using FastEndpoints;
using FastEndpoints.Swagger;
using SimpleLoan.Application;
using SimpleLoan.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddFastEndpoints()
    .SwaggerDocument();

var app = builder.Build();

app.UseFastEndpoints()
   .UseSwaggerGen()
   .UseInfrastructure();

await app.SeedDataAsync();
await app.RunAsync();