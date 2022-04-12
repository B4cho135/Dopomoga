using Dopomoga.Data.Entities.Posts;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.API.SDK.Resources
{
    public interface IPostResource
    {
        [Get("/api/Posts")]
        public Task<ApiResponse<List<PostEntity>>> Get(string searchWord, int? page, int limit = 12, int? category = null);

        [Get("/api/Posts/Main")]
        public Task<ApiResponse<List<PostEntity>>> GetMainPosts();

        [Get("/api/Posts/All")]
        public Task<ApiResponse<List<PostEntity>>> GetAll();

        [Get("/api/Posts/Quantity")]
        public Task<ApiResponse<int>> GetQuantity(string searchWord = null, int? categoryId = null);

        [Get("/api/Posts/{id}")]
        public Task<ApiResponse<PostEntity>> GetById(int id);

        [Post("/api/Posts")]
        public Task<ApiResponse<PostEntity>> Create(PostEntity request);

        [Put("/api/Posts/{id}")]
        public Task<ApiResponse<PostEntity>> Update(int id, PostEntity request);
        [Delete("/api/Posts/{id}")]
        public Task<int> Delete(int id);
    }
}
