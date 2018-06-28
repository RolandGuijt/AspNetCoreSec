using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetSecurity_m4_Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace AspNetSecurity_m4.Api
{
    public class ConferenceApiService
    {
        private readonly HttpClient client;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ConferenceApiService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
        }
        private async Task EnsureBearerToken()
        {
            client.SetBearerToken(await httpContextAccessor.HttpContext.
                GetTokenAsync("access_token"));
        }

        public async Task<IEnumerable<ConferenceModel>> GetAll()
        {
            List<ConferenceModel> result;
            await EnsureBearerToken();
            var response = await client.GetAsync("/Conference/GetAll");
            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadAsAsync<List<ConferenceModel>>();
            else
                throw new HttpRequestException(response.ReasonPhrase);

            return result;
        }

        public async Task<ConferenceModel> GetById(int id)
        {
            var result = new ConferenceModel();
            await EnsureBearerToken();
            var response = await client.GetAsync($"/Conference/GetById/{id}");
            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadAsAsync<ConferenceModel>();
 
            return result;
        }

        public async Task Add(ConferenceModel model)
        {
            await EnsureBearerToken();
            await client.PostAsJsonAsync("/Conference/Add/", model);
        }
    }
}
