using API.Entities;
using MongoDB.Driver;

namespace API.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
