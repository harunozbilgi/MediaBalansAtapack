using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;
using MediaBalans.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediaBalans.Persistence.Repositories
{
    public class ProductRepository : GenericRepositoryAsync<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddProductFileAsync(ProductFile product)
        {
            await _context.ProductFiles.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task AddProductLanguageAsync(ProductLanguage productLanguage)
        {
            await _context.ProductLanguages.AddAsync(productLanguage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductFileAsync(string productId)
        {
            var product = await _context.ProductFiles.Where(x => x.ProductId.ToString() == productId).ToListAsync();
            _context.ProductFiles.RemoveRange(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductLanguageAsync(ProductLanguage productLanguage)
        {
            _context.ProductLanguages.Update(productLanguage);
            await _context.SaveChangesAsync();
        }
    }
}
