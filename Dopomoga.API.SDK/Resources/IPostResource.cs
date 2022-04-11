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
        public Task<ApiResponse<List<PostEntity>>> Get(string searchWord, int? page, int limit = 9, int? category = null);

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
