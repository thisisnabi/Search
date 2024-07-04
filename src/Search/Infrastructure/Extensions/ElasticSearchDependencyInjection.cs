using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Options;

namespace Search.Infrastructure.Extensions;

public static class ElasticSearchDependencyInjection
{
    public static void ElasticSearchConfigure(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped(sp =>
        {
            var elasticSettings = sp.GetRequiredService<IOptions<AppSettings>>().Value.ElasticSearchOptions;
            var settings = new ElasticsearchClientSettings(new Uri(elasticSettings.Host))
                                        .CertificateFingerprint(elasticSettings.Fingerprint)
                                        .Authentication(new BasicAuthentication(elasticSettings.UserName, elasticSettings.Password));

            return new ElasticsearchClient(settings);
        });
    }
}
