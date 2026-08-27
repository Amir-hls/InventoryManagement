using Application.IRepository;
using Application.IServices;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Web.Extentions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();

            });

            services.AddSingleton<ISqlConnectionFactory>(provider
                => new SqlConnectionFactory(connectionString));
            services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
            services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
            services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
            services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStockQuantityRepository, StockQuantityRepository>();
            services.AddScoped<IStockQuantityService, StockQuantityService>();

            return services;

        }
    }
}
