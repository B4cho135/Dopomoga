using Dopomoga.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Dopomoga.API.SDK
{
    public class ServiceHttpClient : HttpClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ServiceHttpClient(IHttpClientSettings settings, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            this.BaseAddress = new Uri(settings.ServiceUrl);
            this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Request.Cookies["access_token"]);
        }
    }
}