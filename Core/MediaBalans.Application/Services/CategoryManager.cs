using MediaBalans.Application.Dtos;
using MediaBalans.Application.Exceptios;
using MediaBalans.Application.Interfaces.Repositories;
using MediaBalans.Application.Interfaces.Services;
using MediaBalans.Application.Wrappers;
using MediaBalans.Domain.Entities;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRespository;

        public CategoryManager(ICategoryRepository categoryRespository) => _categoryRespository = categoryRespository;

        public async Task<Response<Category>> AddCategoryAsync(Category category)
        {
            await _categoryRespository.AddAsync(category);
            return Response<Category>.Success(category, "Ekleme işlemi başarılı", 202);
        }

        public async Task<Response<NoContent>> AddCategoryLanguageAsync(CategoryLanguage category)
        {
            await _categoryRespository.AddCategoryLanguageAsync(category);
            return Response<NoContent>.Success("Ekleme işlemi başarılı", 204);
        }

        public async Task<Response<List<Category>>> GetCategoriesAsync()
        {
            var result = await _categoryRespository.GetAllAsync(x => x.OrderBy(x => x.OrderBy), x => x.CategoryLanguages);
            return Response<List<Category>>.Success(result.ToList());
        }

        public async Task<Response<Category>> GetCategoryAsync(string categoryId)
        {
            var result = await _categoryRespository.GetAsync(x => x.Id.ToString() == categoryId, x => x.CategoryLanguages);
            if (result is not null)
            {
                return Response<Category>.Success(result);
            }
            throw new NotFoundException();
        }

        public async Task<Response<NoContent>> RemoveCategoryAsync(Category category)
        {
            await _categoryRespository.RemoveAsync(category);
            return Response<NoContent>.Success("Silme işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateCategoryAsync(Category category)
        {
            await _categoryRespository.UpdateAsync(category);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }

        public async Task<Response<NoContent>> UpdateCategoryLanguageAsync(CategoryLanguage category)
        {
            await _categoryRespository.UpdateCategoryLanguageAsync(category);
            return Response<NoContent>.Success("Güncellem işlemi başarılı", 204);
        }
    }
}
