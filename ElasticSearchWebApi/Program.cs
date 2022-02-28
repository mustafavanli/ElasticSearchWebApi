using ElasticSearchWebApi.Models;
using Nest;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// you can create cloud server for elasticsearch from this is web site: https://www.elastic.co/
var settings = new ConnectionSettings(new Uri("https://...............es.us-central1.gcp.cloud.es.io:9243"))
.DefaultIndex("customers")
        .PrettyJson()
        .DefaultMappingFor<Customer>(m => m.IdProperty(p => p.Id));

settings.BasicAuthentication("elastic", ".......");

services.AddSingleton<IElasticClient>(new ElasticClient(settings));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

