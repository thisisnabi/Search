namespace Search.Infrastructure.Consumers;

public class CatalogItemAddedEventConsumer(ElasticsearchClient elasticsearchClient) : IConsumer<CatalogItemAddedEvent>
{
    private readonly ElasticsearchClient _elasticsearchClient = elasticsearchClient;

    public async Task Consume(ConsumeContext<CatalogItemAddedEvent> context)
    {
        var message = context.Message;

        if (message is null) return;

        var itemIndex = new CatalogItemIndex
        {
            CatalogBrand = message.CatalogBrand,
            CatalogCategory = message.CatalogCategory,
            Description = message.Description,
            Url = message.DetailUrl,
            Name = message.Name,
            Slug = message.Slug,
        };

        var result = await _elasticsearchClient.Indices.ExistsAsync(CatalogItemIndex.IndexName);

        if (!result.Exists)
        {
            await _elasticsearchClient.Indices
                .CreateAsync<CatalogItemIndex>(index: CatalogItemIndex.IndexName);
        }

        await _elasticsearchClient.IndexAsync(itemIndex, index: CatalogItemIndex.IndexName);
    }
}
