using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleLoan.Domain.Repositories;
using SimpleLoan.Infrastructure.Persistence;
using SimpleLoan.Infrastructure.Repositories;

namespace SimpleLoan.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPaymentPlanRepository, PaymentPlanRepository>();
        services.AddTransient<IDataSeeder, DataSeeder>();

        string dbConnection = configuration.GetConnectionString("SqliteConnection") ?? throw new InvalidOperationException("Sqlite connection string is missing.");
        services.AddDbContext<PaymentDbContext>(opts => 
        { 
            opts.UseSqlite(dbConnection,
                sqlOpts => sqlOpts.MigrationsAssembly(typeof(PaymentDbContext).GetTypeInfo().Assembly.GetName().Name));
        });
        
        return services;
    }
    
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
        
        return app;
    }
    
    public static async Task SeedDataAsync(this IApplicationBuilder app, CancellationToken cancellationToken = default)
    {
        await using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
        var dbSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();

        await dbSeeder.SeedAsync(cancellationToken);
    }
}