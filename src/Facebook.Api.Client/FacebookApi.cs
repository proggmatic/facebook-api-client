using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using Microsoft.Extensions.Options;


namespace Facebook;

public class FacebookApi
{
    private readonly IOptionsSnapshot<FacebookApiConfig>? _facebookApiConfig;
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new(JsonSerializerDefaults.Web);

    public string? AccessToken { get; set; }

    public string? ApiVersion { get; }


    public FacebookApi(string accessToken, string? apiVersion = null)
    {
        _httpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        this.AccessToken = accessToken;
        this.ApiVersion = apiVersion;
    }

    public FacebookApi(IHttpClientFactory httpClientFactory, IOptionsSnapshot<FacebookApiConfig> facebookApiConfig)
    {
        _facebookApiConfig = facebookApiConfig;
        _httpClient = httpClientFactory.CreateClient(ServiceRegistrationExtensions.HTTP_CLIENT_NAME);
    }


    public Task<dynamic?> GetAsync(string url, CancellationToken cancellationToken = default) => GetAsync<dynamic>(url, cancellationToken);


    public async Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken = default)
    {
        ProcessUrl(ref url);

        var response = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
        EnsureSuccessResponse(response);

        return await response.Content.ReadFromJsonAsync<T>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);
    }

    public async Task<T?> PostAsync<T>(string url, object body, CancellationToken cancellationToken = default)
    {
        ProcessUrl(ref url);

        var response = await _httpClient.PostAsJsonAsync(url, body, cancellationToken).ConfigureAwait(false);
        EnsureSuccessResponse(response);

        return await response.Content.ReadFromJsonAsync<T>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);
    }

    public async Task<T?> DeleteAsync<T>(string url, CancellationToken cancellationToken = default)
    {
        ProcessUrl(ref url);

        var response = await _httpClient.DeleteAsync(url, cancellationToken).ConfigureAwait(false);
        EnsureSuccessResponse(response);

        return await response.Content.ReadFromJsonAsync<T>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);
    }


    protected void ProcessUrl(ref string url)
    {
        var sBuilder = new StringBuilder(35 + url.Length + (this.AccessToken?.Length ?? 0));
        if (url.StartsWith("http", StringComparison.OrdinalIgnoreCase) != true)
        {
            sBuilder.Append("https://graph.facebook.com/");

            if (!string.IsNullOrEmpty(this.ApiVersion))
                sBuilder.Append('v').Append(this.ApiVersion).Append('/');
            else if (!string.IsNullOrEmpty(_facebookApiConfig?.Value.ApiVersion))
                sBuilder.Append('v').Append(_facebookApiConfig?.Value.ApiVersion).Append('/');
        }

        sBuilder.Append(url.StartsWith('/') ? url[1..] : url);
        sBuilder.Append(url.Contains('?') ? "&" : "?");
        sBuilder.Append("access_token=").Append(this.AccessToken);

        url = sBuilder.ToString();
    }

    protected void EnsureSuccessResponse(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.OK)
            return;

        var error = response.Content.ReadFromJsonAsync<FacebookGenericError>().Result?.Error!;

        if (string.Equals(error.Type, "OAuthException", StringComparison.OrdinalIgnoreCase))
            throw new FacebookOAuthException(error);

        if (error.Code is 4 or 17 or 32 or 613)
            throw new FacebookApiLimitException(error);

        throw new FacebookApiException(error);
    }
}