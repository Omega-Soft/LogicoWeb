using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Logico.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<LogicoWebContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureSPA(this IServiceCollection services)
        {
            services.AddSpaStaticFiles(options => options.RootPath = "clientapp/dist");
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
        {
		    services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
            };
        }); 
	}
    }
}
