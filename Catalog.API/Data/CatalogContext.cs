using API.Entities;
using MongoDB.Driver;

namespace API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"] ?? configuration.GetValue<string>
                ("DatabaseSettings:ConnectionString"));

            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"] ?? configuration.GetValue<string>
                ("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration["DatabaseSettings:CollectionName"] ?? configuration.GetValue<string>
                ("Database:CollectionName"));

            CatalogContextSeed.SeedData(Products);
            
        }
        public IMongoCollection<Product> Products { get; }
    }
}
