using MediaBalans.Application.Dtos;
using MediaBalans.Application.Exceptios;
using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Services
{
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Response<Product>> AddProdcutAsync(Product product)
        {
            await _productRepository.AddAsync(product);
            return Response<Product>.Success(product, "Ekleme işlemi başarılı", 202);
        }

        public async Task<Response<NoContent>> AddProductFileAsync(ProductFile product)
        {
            await _productRepository.AddProductFileAsync(product);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> AddProductLangauageAsync(ProductLanguage product)
        {
            await _productRepository.AddProductLanguageAsync(product);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);
        }

        public async Task<Response<Product>> GetProductAsync(string productId)
        {
            var result = await _productRepository.GetAsync(x => x.Id.ToString() == productId,
                x => x.ProductFiles,
                x => x.ProductLanguages,
                x => x.Category,
                x => x.Category.CategoryLanguages);

            if(result is not null)
            {
                return Response<Product>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<Product>> GetProductCategoriesAsync(string categoryId)
        {
            var result = await _productRepository.GetAsync(x => x.CategoryId.ToString() == categoryId,
               x => x.ProductFiles,
               x => x.ProductLanguages,
               x => x.Category,
               x => x.Category.CategoryLanguages);

            if (result is not null)
            {
                return Response<Product>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<List<Product>>> GetProductsAsync()
        {
            var result=await _productRepository.GetAllAsync(x=>x.OrderBy(x=>x.OrderBy), x => x.ProductFiles,
               x => x.ProductLanguages,
               x => x.Category,
               x => x.Category.CategoryLanguages);
            return Response<List<Product>>.Success(result.ToList());
        }

        public async Task<Response<Product>> GetProductSlugUrl(string slugUrl)
        {
            var result = await _productRepository.GetAsync(x => x.SlugUrl == slugUrl,
               x => x.ProductFiles,
               x => x.ProductLanguages,
               x => x.Category,
               x => x.Category.CategoryLanguages);

            if (result is not null)
            {
                return Response<Product>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<NoContent>> RemoveProdcutAsync(Product product)
        {
            await _productRepository.RemoveAsync(product);
            return Response<NoContent>.Success("Silme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> RemoveProductFileAsync(string productId)
        {
            await _productRepository.DeleteProductFileAsync(productId);
            return Response<NoContent>.Success("Silme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateProdcutAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateProdcutLanguageAsync(ProductLanguage product)
        {
            await _productRepository.UpdateProductLanguageAsync(product);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }
    }
}
