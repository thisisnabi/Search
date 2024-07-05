namespace Search.Models;

public class CatalogItemIndex
{
    public const string IndexName = "catalog-item-index";

    public required Id Id { get; set; }

    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string CatalogCategory { get; set; }
    public required string CatalogBrand { get; set; }
    public required string Url { get; set; }
 
}
