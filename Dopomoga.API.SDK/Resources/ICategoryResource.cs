using Dopomoga.Data.Entities.Categories;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.API.SDK.Resources
{
    public interface ICategoryResource
    {
        [Get("/api/categories")]
        public Task<ApiResponse<List<CategoryEntity>>> Get();

        [Get("/api/categories/MainCategories")]
        public Task<ApiResponse<List<MainCategoryEntity>>> GetMainCategories();

        [Get("/api/categories/{id}")]
        public Task<ApiResponse<CategoryEntity>> GetById(int id);

        [Post("/api/categories")]
        public Task<ApiResponse<CategoryEntity>> Create(CategoryEntity request);

        [Put("/api/categories/{id}")]
        public Task<ApiResponse<CategoryEntity>> Update(int id, CategoryEntity request);
        [Delete("/api/categories/{id}")]
        public Task<int> Delete(int id);
    }
}
