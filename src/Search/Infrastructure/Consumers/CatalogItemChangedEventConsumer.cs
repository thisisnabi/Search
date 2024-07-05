namespace Search.Infrastructure.Consumers;

public class CatalogItemChangedEventConsumer(ElasticsearchClient elasticsearchClient) : IConsumer<CatalogItemChangedEvent>
{
    private readonly ElasticsearchClient _elasticsearchClient = elasticsearchClient;

    public async Task Consume(ConsumeContext<CatalogItemChangedEvent> context)
    {
        var message = context.Message;

        if (message is null) return;
         
        var result = await _elasticsearchClient
            .UpdateAsync<CatalogItemIndex, CatalogItemIndex>(message.Slug, CatalogItemIndex.IndexName, 
            u=> u.Doc(new CatalogItemIndex { 
                CatalogBrand = message.CatalogBrand,
                CatalogCategory = message.CatalogCategory,
                Description = message.Description,
                Id = message.Slug,
                Name = message.Name,    
                Url = message.DetialUrl,
            }));
    }
}
