using System;
using System.Reflection;
using Company.Api.PetRocks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeFormattedMessage = true;
    logging.IncludeScopes = true;
});

var serviceName = Assembly.GetExecutingAssembly().GetName().Name.ToString();
var serviceVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
var appResourceBuilder = ResourceBuilder.CreateDefault()
    .AddService(serviceName: serviceName, serviceVersion: serviceVersion);
builder.Services
    .AddMetrics()
    .AddOpenTelemetry()
    .ConfigureResource(c => c.AddService(serviceName))
    .WithMetrics(metricProviderBuilder => metricProviderBuilder
        .AddMeter(
            "Microsoft.AspNetCore.Hosting",
            "Microsoft.AspNetCore.Server.Kestrel",
            "System.Net.Http",
            serviceName)
        .SetResourceBuilder(appResourceBuilder)
        .AddAspNetCoreInstrumentation()
        .AddRuntimeInstrumentation())
    .WithTracing(tracerProviderBuilder => tracerProviderBuilder
        .AddSource(serviceName, nameof(PetRocksController))
        .SetResourceBuilder(appResourceBuilder)
        .AddAspNetCoreInstrumentation()
        .AddEntityFrameworkCoreInstrumentation());

var otlpExporterEndpoint = builder.Configuration.GetValue<string>("OTEL_EXPORTER_OTLP_ENDPOINT");
if (Uri.TryCreate(otlpExporterEndpoint, UriKind.Absolute, out _))
{
    builder.Services.Configure<OpenTelemetryLoggerOptions>(options => options.AddOtlpExporter())
        .ConfigureOpenTelemetryMeterProvider(metrics => metrics.AddOtlpExporter())
        .ConfigureOpenTelemetryTracerProvider(tracing => tracing.AddOtlpExporter());
}

// allow any traffic for now
builder.Services.AddCors(policy => policy.AddPolicy("CorsPolicy", opt => opt
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Company Website API", Version = "v1" }));

var sqlLiteConnectionStringBuilder = new SqliteConnectionStringBuilder()
{
    DataSource = "CompanyApi.db", Mode = SqliteOpenMode.ReadWriteCreate,
};
builder.Services.AddDbContext<PetRockContext>(options =>
    options.UseSqlite(sqlLiteConnectionStringBuilder.ConnectionString));

var host = builder.Build();

host.UseSwagger();
// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
host.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company Website API v1"));

if (host.Environment.IsDevelopment())
{
    host.UseDeveloperExceptionPage();
}

host.UseHttpsRedirection();
host.UseCors("CorsPolicy");
host.UseRouting();
host.UseAuthorization();
host.MapControllers();

using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PetRockContext>();
    db.Database.Migrate();
}

await host.RunAsync();
