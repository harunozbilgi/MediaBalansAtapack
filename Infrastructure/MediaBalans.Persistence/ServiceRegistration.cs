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
