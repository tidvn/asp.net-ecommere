using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using static NuGet.Packaging.PackagingConstants;

namespace TDStore.Models
{
    public class TestMongoContext
    {
        IConfiguration _configuration;
        public TestMongoContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IMongoDatabase Connection 
        {
            get 
            { 
                var client = new MongoClient(_configuration.GetConnectionString("MongoConnection"));
                var database = client.GetDatabase(_configuration.GetConnectionString("database"));
                return database;
            }       
        }
        public IMongoCollection<Product> Products => Connection.GetCollection<Product>("products");
        public IMongoCollection<Order> Orders => Connection.GetCollection<Order>("orders");
        public IMongoCollection<ImageData> ImagesDatas => Connection.GetCollection<ImageData>("imagesdatas");
        public IMongoCollection<CartItem> CartItems => Connection.GetCollection<CartItem>("cartitems");
       
    }
}
