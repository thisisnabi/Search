using Search;
using Search.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.BrokerConfigure();
builder.ElasticSearchConfigure();

builder.Services.Configure<AppSettings>(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
 
app.UseHttpsRedirection();
 
app.Run();

