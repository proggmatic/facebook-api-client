using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Facebook.Api.Client
{
    public class FacebookClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FacebookClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var httpClient = _httpClientFactory.CreateClient(ServiceRegistrationExtensions.HTTP_CLIENT_NAME);
            var response = await httpClient.GetAsync(url).ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            T result = JsonConvert.DeserializeObject<T>(content);

            return result;
        }

        public async Task<T> PostAsync<T>(string url, object parameters)
        {
            var httpClient = _httpClientFactory.CreateClient(ServiceRegistrationExtensions.HTTP_CLIENT_NAME);
            var response = await httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json")).ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            T result = JsonConvert.DeserializeObject<T>(content);

            return result;
        }

        public async Task<T> DeleteAsync<T>(string url)
        {
            var httpClient = _httpClientFactory.CreateClient(ServiceRegistrationExtensions.HTTP_CLIENT_NAME);
            var response = await httpClient.DeleteAsync(url).ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            T result = JsonConvert.DeserializeObject<T>(content);

            return result;
        }
    }
}
