using CoffeeVendingMachine.Application.Common.Interfaces;
using CoffeeVendingMachine.Application.Interfaces.Repositories;
using CoffeeVendingMachine.Application.Interfaces.Services;
using CoffeeVendingMachine.Application.Interfaces.UseCases;
using CoffeeVendingMachine.Application.UseCases.ExternalCoffees;
using CoffeeVendingMachine.Application.UseCases.Orders;
using CoffeeVendingMachine.Domain.Constants;
using CoffeeVendingMachine.Infrastructure.Data;
using CoffeeVendingMachine.Infrastructure.Data.Interceptors;
using CoffeeVendingMachine.Infrastructure.Data.Repositories;
using CoffeeVendingMachine.Infrastructure.Identity;
using CoffeeVendingMachine.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<CoffeeDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        services.AddScoped((Func<IServiceProvider, ICoffeeDbContext>)(provider => provider.GetRequiredService<CoffeeDbContext>()));

        services.AddScoped<CoffeeDbContextInitialiser>();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<CoffeeDbContext>();

        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IIdentityService, IdentityService>();

        services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

        services.RegisterRepositories();
        services.RegisterServices();
        services.RegisterUseCases();

        return services;
    }

    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICoffeeRepository, CoffeeRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomizationRepository, CustomizationRepository>();

        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        //services.AddScoped<IExternalCoffeeService, ExternalCoffeeService>();
        services.AddHttpClient<IExternalCoffeeService, ExternalCoffeeService>();

        services.AddScoped<ICoffeeService, CoffeeService>();
        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<ICustomizationService, CustomizationService>();

        return services;
    }

    public static IServiceCollection RegisterUseCases(this IServiceCollection services)
    {
        services.AddScoped<IFetchExternalCoffeesUseCase, FetchExternalCoffeesUseCase>();
        services.AddScoped<IFetchSingleExternalCoffeeUseCase, FetchSingleExternalCoffeeUseCase>();

        services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
        services.AddScoped<IUpdateOrderUseCase, UpdateOrderUseCase>();

        return services;
    }


}
