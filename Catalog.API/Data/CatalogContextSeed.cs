using API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
                productCollection.InsertManyAsync(GetMyProducts());
        }

        private static IEnumerable<Product> GetMyProducts() 
        {
            return new List<Product>()
{
                new Product
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Smartphone",
                    Category = "Electronics",
                    Description = "Latest model with 128GB storage",
                    Image = "smartphone.jpg",
                    Price = 699.99M
                },
                new Product
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Gaming Laptop",
                    Category = "Computers",
                    Description = "High-performance laptop for gaming",
                    Image = "gaming_laptop.jpg",
                    Price = 1299.99M
                },
                new Product
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Wireless Headphones",
                    Category = "Accessories",
                    Description = "Noise-cancelling over-ear headphones",
                    Image = "headphones.jpg",
                    Price = 199.99M
                },
                new Product
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Electric Toothbrush",
                    Category = "Health & Beauty",
                    Description = "Rechargeable toothbrush with multiple modes",
                    Image = "toothbrush.jpg",
                    Price = 49.99M
                },
                new Product
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "LED Desk Lamp",
                    Category = "Home & Office",
                    Description = "Adjustable desk lamp with touch controls",
                    Image = "desk_lamp.jpg",
                    Price = 29.99M
                }
            };
        }
    }
}
