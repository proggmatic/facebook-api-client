using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Facebook.Api.Client
{
    public static class ServiceRegistrationExtensions
    {
        internal const string HTTP_CLIENT_NAME = "FacebookApiHttpClient";

        public static IServiceCollection AddBeelineAPI(this IServiceCollection services)
        {
            //if (configuration != null && !string.IsNullOrEmpty(configurationSection))
            //    services.Configure<BeelineApiConfig>(configuration.GetSection(configurationSection));

            services.AddHttpClient(HTTP_CLIENT_NAME, (serviceProvider, client) =>
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders
                        .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                })
                .ConfigureHttpMessageHandlerBuilder(config => config.PrimaryHandler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });

            //services.AddHttpClient<BeelineApiHttpClient>()
            //    .AddHttpMessageHandler(c => new  HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });

            return services;
        }
    }
}