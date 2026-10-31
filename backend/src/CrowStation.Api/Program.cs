using CrowStation.Api.Endpoints;
using CrowStation.Api.Infrastructure;
using CrowStation.Application;
using CrowStation.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors(ApiServices.CorsPolicyName);

app.MapHealthEndpoints();
app.MapApiV1();

await app.RunAsync().ConfigureAwait(false);

/// <summary>
/// Exposed for WebApplicationFactory in the integration tests.
/// </summary>
public partial class Program;
