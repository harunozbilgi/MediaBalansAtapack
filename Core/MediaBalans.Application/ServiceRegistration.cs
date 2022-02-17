using FluentValidation.AspNetCore;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MediaBalans.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationRegistration(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddTransient<ILanguageService, LanguageManager>();
            services.AddTransient<ICategoryService, CategoryManager>();
            services.AddFluentValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
                options.ImplicitlyValidateChildProperties = true;
                options.RegisterValidatorsFromAssembly(assembly);
            });
        }
    }
}
