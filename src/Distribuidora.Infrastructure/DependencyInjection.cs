using Distribuidora.Application.Users.Abstractions;
using Distribuidora.Infrastructure.Persistence.Repositories;
using Distribuidora.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

            return services;
        }

        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            return services;
        }
    }
}
