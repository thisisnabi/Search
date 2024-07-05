namespace Catalog.Infrastructure.IntegrationEvents;

public record CatalogItemChangedEvent(
    string Name,
    string Description,
    string CatalogCategory,
    string CatalogBrand,
    string Slug,
    string DetialUrl);
