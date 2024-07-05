namespace Catalog.Infrastructure.IntegrationEvents;

public record CatalogItemAddedEvent(
    string Name,
    string Description,
    string CatalogCategory,
    string CatalogBrand,
    string Slug,
    string DetailUrl);
