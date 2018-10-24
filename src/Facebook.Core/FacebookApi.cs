using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Facebook
{
    public class FacebookApi
    {
        private readonly HttpClient _httpClient;

        public string AccessToken { get; set; }

        public string ApiVersion { get; }


        public FacebookApi(string accessToken, string apiVersion = null)
        {
            _httpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            this.AccessToken = accessToken;
            this.ApiVersion = apiVersion;
        }

        public FacebookApi(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(ServiceRegistrationExtensions.HTTP_CLIENT_NAME);
        }


        public Task<dynamic> GetAsync(string url, CancellationToken cancellationToken = default) => GetAsync<dynamic>(url, cancellationToken);


        public async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            ProcessUrl(ref url);

            var response = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            EnsureSuccessResponse(response);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<T> PostAsync<T>(string url, object body, CancellationToken cancellationToken = default)
        {
            ProcessUrl(ref url);

            var response = await _httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"), cancellationToken).ConfigureAwait(false);
            EnsureSuccessResponse(response);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<T> DeleteAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            ProcessUrl(ref url);

            var response = await _httpClient.DeleteAsync(url, cancellationToken).ConfigureAwait(false);
            EnsureSuccessResponse(response);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }


        protected void ProcessUrl(ref string url)
        {
            var sBuilder = new StringBuilder(35 + (url?.Length ?? 0) + (this.AccessToken?.Length ?? 0));
            if (url?.StartsWith("http", StringComparison.OrdinalIgnoreCase) != true)
            {
                sBuilder.Append("https://graph.facebook.com/");
                if (!string.IsNullOrEmpty(this.ApiVersion))
                    sBuilder.Append("v").Append(this.ApiVersion).Append("/");
            }

            sBuilder.Append(url?.StartsWith("/") == true ? url.Substring(1) : url);
            sBuilder.Append(url?.Contains("?") == true ? "&" : "?");
            sBuilder.Append("access_token=").Append(this.AccessToken);

            url = sBuilder.ToString();
        }

        protected void EnsureSuccessResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
                return;

            var content = response.Content.ReadAsStringAsync().Result;

            var error = JsonConvert.DeserializeObject<FacebookGenericError>(content).Error;
            if (string.Equals(error.Type, "OAuthException", StringComparison.OrdinalIgnoreCase))
                throw new FacebookOAuthException(error);

            if (error.Code == 4 || error.Code == 17 || error.Code == 32 || error.Code == 613)
                throw new FacebookApiLimitException(error);

            throw new FacebookApiException(error);
        }
    }
}