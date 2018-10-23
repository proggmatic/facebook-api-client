using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Facebook.Api.Client
{
    public class FacebookClient
    {
        private readonly HttpClient _httpClient;

        private string _accessToken;
        public string AccessToken
        {
            get => _accessToken;
            set
            {
                _accessToken = value;
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("access_token", this.AccessToken);
            }
        }

        public string ApiVersion { get; }


        public FacebookClient(string accessToken, string apiVersion = null)
        {
            _httpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })
            {
                BaseAddress = new Uri("https://graph.facebook.com/" + (!string.IsNullOrEmpty(apiVersion) ? "v" + apiVersion + "/" : ""))
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            this.AccessToken = accessToken;
            this.ApiVersion = apiVersion;
        }

        public FacebookClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(ServiceRegistrationExtensions.HTTP_CLIENT_NAME);
        }


        public Task<dynamic> GetAsync(string url, CancellationToken cancellationToken = default) => GetAsync<dynamic>(url, cancellationToken);

        public async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            T result = JsonConvert.DeserializeObject<T>(content);

            return result;
        }

        public async Task<T> PostAsync<T>(string url, object parameters, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json"), cancellationToken).ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            T result = JsonConvert.DeserializeObject<T>(content);

            return result;
        }

        public async Task<T> DeleteAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync(url, cancellationToken).ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            T result = JsonConvert.DeserializeObject<T>(content);

            return result;
        }
    }
}