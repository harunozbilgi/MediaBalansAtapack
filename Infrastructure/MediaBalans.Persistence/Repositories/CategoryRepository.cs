using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;
using MediaBalans.Persistence.Context;

namespace MediaBalans.Persistence.Repositories
{
    public class CategoryRepository : GenericRepositoryAsync<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddCategoryLanguageAsync(CategoryLanguage categoryLanguage)
        {
            await _context.CategoryLanguages.AddAsync(categoryLanguage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryLanguageAsync(CategoryLanguage categoryLanguage)
        {
             _context.CategoryLanguages.Update(categoryLanguage);
            await _context.SaveChangesAsync();
        }
    }
}
