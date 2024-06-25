using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using Company.Api.Products;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

// allow any traffic for now
builder.Services.AddCors(policy => policy.AddPolicy("CorsPolicy", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Company Website API", Version = "v1" }));

var sqlLiteConnectionStringBuilder = new SqliteConnectionStringBuilder()
{
    DataSource = "CompanyApi.db",
    Mode = SqliteOpenMode.ReadWriteCreate,
};
builder.Services.AddDbContext<ProductContext>(options => options.UseSqlite(sqlLiteConnectionStringBuilder.ConnectionString));

var serviceName = Assembly.GetExecutingAssembly().GetName().Name.ToString();
var serviceVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
var appResourceBuilder = ResourceBuilder.CreateDefault()
    .AddService(serviceName: serviceName, serviceVersion: serviceVersion);
var otlpExporterEndpoint = builder.Configuration.GetValue<string>("OTLPExporter:Endpoint");
if (Uri.TryCreate(otlpExporterEndpoint, UriKind.Absolute, out var otlpExporterEndpointUri))
{
    var meter = new Meter(serviceName);
    builder.Services.AddOpenTelemetry()
        .WithTracing(tracerProviderBuilder => tracerProviderBuilder
            .AddOtlpExporter(opt => opt.Endpoint = otlpExporterEndpointUri)
            .AddSource(serviceName)
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
            .AddAspNetCoreInstrumentation())
        .WithMetrics(metricProviderBuilder => metricProviderBuilder
            .AddOtlpExporter(opt => opt.Endpoint = otlpExporterEndpointUri)
            .AddMeter(meter.Name)
            .SetResourceBuilder(appResourceBuilder)
            .AddAspNetCoreInstrumentation());
}
else
{
    Log.Logger.Warning("Invalid OTLP URI: {uri}. Open Telemetry will not be configured.", otlpExporterEndpoint);
}

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
    var db = scope.ServiceProvider.GetRequiredService<ProductContext>();
    db.Database.Migrate();
}

await host.RunAsync();
