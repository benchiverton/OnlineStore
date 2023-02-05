using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Online Store API", Version = "v1" }));

var host = builder.Build();

host.UseSwagger();
// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
host.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Store API v1"));

if (host.Environment.IsDevelopment())
{
    host.UseDeveloperExceptionPage();
}
host.UseHttpsRedirection();
host.UseCors("CorsPolicy");
host.UseRouting();
host.UseAuthorization();
host.UseEndpoints(endpoints => endpoints.MapControllers());

await host.RunAsync();
