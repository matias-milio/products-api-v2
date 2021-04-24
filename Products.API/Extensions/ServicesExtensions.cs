using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Products.Application.Interfaces;
using Products.Domain;
using Products.Helpers.ConfigModels;
using Products.Infrastructure.AppDbContext;
using Products.Infrastructure.Implementations;
using Products.Infrastructure.Intefaces;
using Products.Security.Tokens;
using System.Linq;
using System.Text;

namespace Products.API.Extensions
{
    public static class ServicesExtensions
    {     

        public static IServiceCollection AddIdentityCoreCustom(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<MyStoreDbContext>();
            identityBuilder.AddSignInManager<SignInManager<User>>();
            services.TryAddSingleton<ISystemClock, SystemClock>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfigurationSection tokenSettingsSection)
        {
            services.Configure<TokenSettings>(tokenSettingsSection);
            var tokenSettings = tokenSettingsSection.Get<TokenSettings>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateAudience = false,
                ValidateIssuer = false
            });
            services.AddScoped<IJwtFactory, JwtFactory>();

            return services;
        }

        public static IServiceCollection AddControllersCustom(this IServiceCollection services)
        {
            services.AddControllers(options => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Application.Products.RequestModels.Create>());

            return services;
        }

        public static IServiceCollection AddSwaggerGenCustom(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });
            });

            return services;
        }

        public static IServiceCollection AddRedis(this IServiceCollection services, IConfigurationSection cacheSettingSection)
        {
            services.AddStackExchangeRedisCache(opt => opt.Configuration = cacheSettingSection["ConnectionString"]);
            services.Configure<CacheSettings>(cacheSettingSection);
            services.AddScoped<ICacheManager, CacheManager>();
            return services;
        }
    }
}
