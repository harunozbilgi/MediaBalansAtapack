using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;
using MediaBalans.Persistence.Configurations;
using MediaBalans.Persistence.Configurations.LanguagesMap;
using Microsoft.EntityFrameworkCore;

namespace MediaBalans.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppConfigMap());
            modelBuilder.ApplyConfiguration(new AppSeoMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new DocumentMap());
            modelBuilder.ApplyConfiguration(new GalleryMap());
            modelBuilder.ApplyConfiguration(new LanguageMap());
            modelBuilder.ApplyConfiguration(new NewsMap());
            modelBuilder.ApplyConfiguration(new PageMap());
            modelBuilder.ApplyConfiguration(new PagePropertyMap());
            modelBuilder.ApplyConfiguration(new PortfolioMap());
            modelBuilder.ApplyConfiguration(new ProductFileMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ServiceFileMap());
            modelBuilder.ApplyConfiguration(new ServiceMap());
            modelBuilder.ApplyConfiguration(new ServicePropertyMap());
            modelBuilder.ApplyConfiguration(new SliderMap());

            modelBuilder.ApplyConfiguration(new AppSeoLanguageMap());
            modelBuilder.ApplyConfiguration(new CategoryLanguageMap());
            modelBuilder.ApplyConfiguration(new NewsLanguageMap());
            modelBuilder.ApplyConfiguration(new PageLanguageMap());
            modelBuilder.ApplyConfiguration(new PortfolioLanguageMap());
            modelBuilder.ApplyConfiguration(new ProductLanguageMap());
            modelBuilder.ApplyConfiguration(new ServiceLanguageMap());
            modelBuilder.ApplyConfiguration(new ServicePropertyLanguageMap());
            modelBuilder.ApplyConfiguration(new SliderLanguageMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<AppSeo> AppSeos { get; set; }  
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Document> Documents { get; set; }  
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageProperty> PageProperties { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioFile> PortfolioFiles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFile> ProductFiles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceFile> ServiceFiles { get; set; }
        public DbSet<ServiceProperty> ServiceProperties { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        #region Language
        public DbSet<AppSeoLanguage> AppSeoLanguages { get; set; }
        public DbSet<CategoryLanguage> CategoryLanguages { get; set; }
        public DbSet<NewsLanguage> NewsLanguages { get; set; }
        public DbSet<PageLanguage> PageLanguages { get; set; }
        public DbSet<PortfolioLanguage> PortfolioLanguages { get; set; }
        public DbSet<ProductLanguage> ProductLanguages { get; set; }
        public DbSet<ServiceLanguage> ServiceLanguages { get; set; }
        public DbSet<ServicePropertyLanguage> ServicePropertyLanguages { get; set; }
        public DbSet<SliderLanguage> SliderLanguages { get; set; }
        #endregion

    }
}
