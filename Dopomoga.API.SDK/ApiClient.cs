using Dopomoga.API.SDK.Resources;
using Refit;

namespace Dopomoga.API.SDK
{
    public class ApiClient
    {

        private readonly ServiceHttpClient httpClient;
        public IAccountResource Account { get; set; }
        public ICategoryResource Categories { get; set; }
        public IPostResource Posts { get; set; }
        public IPageInformationResource PageInformation { get; set; }
        public ISubscribersResource Subscribers { get; set; }



        public ApiClient(ServiceHttpClient httpClient)
        {
            Categories = RestService.For<ICategoryResource>(httpClient);
            Account = RestService.For<IAccountResource>(httpClient);
            Posts = RestService.For<IPostResource>(httpClient);
            PageInformation = RestService.For<IPageInformationResource>(httpClient);
            Subscribers = RestService.For<ISubscribersResource>(httpClient);

            this.httpClient = httpClient;
        }
    }
}