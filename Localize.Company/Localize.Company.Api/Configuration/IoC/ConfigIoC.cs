using Localize.Company.Application.Contracts;
using Localize.Company.Application.Mappers;
using Localize.Company.Application.Services;
using Localize.Company.Domain.Contracts.Repositories;
using Localize.Company.Domain.Contracts.Services;
using Localize.Company.Domain.Notifications;
using Localize.Company.Domain.Services;
using Localize.Company.Infrastructure.Contexts;
using Localize.Company.Infrastructure.External.ReceitaWSApi.Contracts;
using Localize.Company.Infrastructure.External.ReceitaWSApi.Repositories;
using Localize.Company.Infrastructure.External.ReceitaWSApi.Services;
using Localize.Company.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Localize.Company.Configuration.IoC
{
    public static class ConfigIoC
    {
        public static void AddIndependencyInjection(IServiceCollection services, IConfiguration config)
        {
            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IReceitaWSService, ReceitaWSService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IOrganizationService, OrganizationService>();


            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IReceitaWSRepository, ReceitaWSRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            //Contexts
            services.AddDbContext<LocalizeCompanyContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);


            //AutoMapper
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            //NotificationPattern
            services.AddScoped<NotificationContext>();
        }

        public static void JWTConfiguration(IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };
            });
        }
    }
}
