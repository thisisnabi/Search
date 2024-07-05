var builder = WebApplication.CreateBuilder(args);

builder.BrokerConfigure();
builder.ElasticSearchConfigure();
builder.BindAppSettings();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/", SearchItems);
 
app.Run();
 
static async Task<Results<Ok<IReadOnlyCollection<CatalogItemIndex>>, NotFound>> SearchItems(string qr, ElasticsearchClient elasticsearch)
{
    var response = await elasticsearch.SearchAsync<CatalogItemIndex>(s => s
        .Index(CatalogItemIndex.IndexName)
        .From(0)
        .Size(10)
        .Query(q =>
             q.Fuzzy(t => t.Field(x => x.Description).Value(qr)))
    );

    if (response.IsValidResponse)
        return TypedResults.Ok(response.Documents);

    return TypedResults.NotFound();

}