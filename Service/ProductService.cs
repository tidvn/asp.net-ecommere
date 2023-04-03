using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TDStore.Models;

namespace TDStore.Service
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Product_Inventory> _inventoryCollection;
        private readonly IMongoCollection<Product_Category> _categoryCollection;

        public ProductService(
        IOptions<MongoDbSettings> MongoDbSettings)
        {
            var mongoClient = new MongoClient(
                MongoDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                MongoDbSettings.Value.DatabaseName);

            _productCollection = mongoDatabase.GetCollection<Product>("Products");
            _inventoryCollection=mongoDatabase.GetCollection<Product_Inventory>("Inventorys");
            _categoryCollection=mongoDatabase.GetCollection<Product_Category>("Categorys");
        }
        // Product
        public async Task<List<Product>> GetAllAsync() =>
        await _productCollection.Find(_ => true).ToListAsync();

        public async Task<Product?> GetByIdAsync(string id) =>
            await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product n) =>
            await _productCollection.InsertOneAsync(n);

        public async Task UpdateAsync(string id, Product u) =>
            await _productCollection.ReplaceOneAsync(x => x.Id == id, u);

        public async Task RemoveAsync(string id) =>
            await _productCollection.DeleteOneAsync(x => x.Id == id);
        // Inventory
        public async Task<List<Product_Inventory>> GetAllInventoryAsync() =>
        await _inventoryCollection.Find(_ => true).ToListAsync();

        public async Task<List<Product_Inventory>> GetAllInventoryOfProduct(string idP)
        {
            Product p = await GetByIdAsync(idP);
            List<Product_Inventory> lst = new List<Product_Inventory>();
            foreach (var inv in p.Inventory){
                lst.Add( await _inventoryCollection.Find(x => x.Id == inv).FirstOrDefaultAsync());
            }
        return lst ;
        }
       
        public async Task<Product_Inventory?> GetInventoryByIdAsync(string id) =>
            await _inventoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateInventoryAsync(Product_Inventory n) =>
            await _inventoryCollection.InsertOneAsync(n);

        public async Task UpdateInventoryAsync(string id, Product_Inventory u) =>
            await _inventoryCollection.ReplaceOneAsync(x => x.Id == id, u);

        public async Task RemoveInventoryAsync(string id) =>
            await _inventoryCollection.DeleteOneAsync(x => x.Id == id);
        // Category
        public async Task<List<Product_Category>> GetAllCategoryAsync() =>
        await _categoryCollection.Find(_ => true).ToListAsync();

        public async Task<List<Product_Category>> GetAllCategoryOfProduct(string idP)
        {
            Product p = await GetByIdAsync(idP);
            List<Product_Category> lst = new List<Product_Category>();
            foreach (var cate in p.Category){
                lst.Add( await _categoryCollection.Find(x => x.Id == cate).FirstOrDefaultAsync());
            }
        return lst ;
        }
       

        public async Task<Product_Category?> GetCategoryByIdAsync(string id) =>
            await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateCategoryAsync(Product_Category n) =>
            await _categoryCollection.InsertOneAsync(n);

        public async Task UpdateCategoryAsync(string id, Product_Category u) =>
            await _categoryCollection.ReplaceOneAsync(x => x.Id == id, u);

        public async Task RemoveCategoryAsync(string id) =>
            await _categoryCollection.DeleteOneAsync(x => x.Id == id);
    }
}
