using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Persistence.Context;
using MediaBalans.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediaBalans.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceRegistration(this IServiceCollection service, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("SqlConnection");
            service.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                 configure =>
                 {
                     configure.MigrationsAssembly("MediaBalans.Persistence");
                 })              
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .ConfigureWarnings(warnings =>
                {
                    warnings.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning);
                }));

            service.AddTransient<ILanguageRepository, LanguageRepository>();
            service.AddTransient<ICategoryRepository, CategoryRepository>();
            service.AddTransient<IDocumentRepository, DocumentRepository>();
            service.AddTransient<ISliderRepository, SliderRepository>();
            service.AddTransient<IAppSeoRepository, AppSeoRepository>();
            service.AddTransient<IProductRepository, ProductRepository>();  
            service.AddTransient<IPortfolioRepository, PortfolioRepository>();
            service.AddTransient<INewsRepository, NewsRepository>();   
            service.AddTransient<IServiceRepository, ServiceRepository>();
            service.AddTransient<IServicePropertyRepository, ServicePropertyRepository>();
            service.AddTransient<IPageRepository, PageRepository>();
            service.AddTransient<IPagePropertyRepository, PagePropertyRepository>();
            service.AddTransient<IAppConfigRepository, AppConfigRepository>();
            service.AddTransient<IAppUserRepository, AppUserRepository>();
            service.AddTransient<IGalleryRepository, GalleryRepository>();
        }
        public static  void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            applicationDbContext.Database.Migrate();


        }
    }
}
