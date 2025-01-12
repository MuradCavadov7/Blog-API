using BlogApp.BL.ExternalServices.Implements;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.BL.Helper;
using BlogApp.BL.Services.Implements;
using BlogApp.BL.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BL
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtHandler, JwtHandler>();
            services.AddMemoryCache();
            return services;
        }
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(typeof(ServiceRegistration));
            return services;
        }
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceRegistration));
            return services;
        }
    }
}
