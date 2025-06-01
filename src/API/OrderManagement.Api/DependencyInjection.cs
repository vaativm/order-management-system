using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderManagement.Api.Application.Abstractions;
using OrderManagement.Api.Application.Services;
using OrderManagement.Api.Infrastructure.Data;
using OrderManagement.Api.Infrastructure.Data.Repositories;

namespace OrderManagement.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .ConfigureCors()
            .AddDomainServices()
            .AddDatabase(configuration);

        return services;
    }
    private static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IDiscountService, DiscountService>();
        services.AddScoped<IPromotionRepository, PromotionRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPromotionRepository, PromotionRepository>();
        services.AddScoped<IDiscountStrategyFactory, DiscountStrategyFactory>();
        services.AddScoped<IOrderStatusService, OrderStatusService>();
        services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();


        return services;
    }
    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<IOrderManagementDbContext>(sp => sp.GetRequiredService<OrderManagementDbContext>());
        services.AddDbContextPool<OrderManagementDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
    private static IServiceCollection ConfigureCors(this IServiceCollection services)
        => services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            ));
    public static void ConfigSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(s => s.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Customer Orders Api",
            Version = "v1",
            Description = "An api for managing customer orders"
        }));
    }
}
