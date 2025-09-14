using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaSecretaria.Application.Interfaces;
using SistemaSecretaria.Application.Mappings;
using SistemaSecretaria.Application.Services;
using SistemaSecretaria.Data.Context;
using SistemaSecretaria.Data.Repositories;
using SistemaSecretaria.Domain.Interfaces;

namespace SistemaSecretaria.IoC
{
    public static class DependencyInjectinAPI
    {
        public static IServiceCollection AddInfrastructureAPI
            (
                this IServiceCollection services,
                IConfiguration configuration
            )
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LocalDb"), b =>
                b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            // Injeção de Dependências Repositories
            services.AddScoped<IAlunoRepository, AlunoRepository>();

            // Injeção de Dependências Services
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAlunoService, AlunoService>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}
