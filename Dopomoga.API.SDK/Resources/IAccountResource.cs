using Dopomoga.Models.Requests.Identity;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.API.SDK.Resources
{
    public interface IAccountResource
    {
        [Post("/api/accounts/login")]
        public Task<ApiResponse<LoginResponse>> Login(LoginRequest loginRequest);
        [Post("/api/accounts/register")]
        public Task<ApiResponse<string>> Register(string email, string password);
        [Post("/api/accounts/logout")]
        public Task<ApiResponse<string>> Logout();
    }
}
