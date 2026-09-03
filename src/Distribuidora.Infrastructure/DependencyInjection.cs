using Distribuidora.Application.Users.Abstractions;
using Distribuidora.Infrastructure.Persistence.Repositories;
using Distribuidora.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;
using Distribuidora.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using Distribuidora.Application.Customers.Abstractions;



namespace Distribuidora.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

            services.AddScoped<IJwtProvider, JwtProvider>();

            return services;
        }

    }
}
