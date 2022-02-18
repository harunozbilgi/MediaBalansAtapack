using FluentValidation.AspNetCore;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Services;
using MediaBalans.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MediaBalans.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.Configure<DocumentSetting>(configuration.GetSection(nameof(DocumentSetting)));

            services.AddTransient<ILanguageService, LanguageManager>();
            services.AddTransient<ICategoryService, CategoryManager>();
            services.AddTransient<IDocumentService, DocumentManager>(); 
            services.AddTransient<ISliderService, SliderManager>();
            services.AddTransient<IAppSeoService, AppSeoManager>();
            services.AddTransient<IProductService, ProductManager>();  
            services.AddTransient<IPortfolioService, PortfolioManager>();  

            services.AddFluentValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
                options.ImplicitlyValidateChildProperties = true;
                options.RegisterValidatorsFromAssembly(assembly);
            });
        }
    }
}
