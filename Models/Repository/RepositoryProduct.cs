using MongoDB.Bson;
using MongoDB.Driver;
using TDStore.Areas.Store.Models;

namespace TDStore.Models.Repository
{
    public class RepositoryProduct : IRepositoryProduct
    {
        TestMongoContext _context;
        public RepositoryProduct(TestMongoContext context)
        {
            _context = context;
        }
        public bool Delete(string key)
        {
            _context.Products.DeleteOne(x=> x.Id == key );
            return true;
        }

        public List<Product> GetAll()
        {
            return _context.Products.Find(FilterDefinition<Product>.Empty).ToList();
        }

        public Product GetById(string key)
        {
            return _context.Products.Find(x => x.Id == key).FirstOrDefault();
        }
        public bool Insert(Product entity)
        {
            entity.Details = entity.Details ?? "";
            
            _context.Products.InsertOne(entity);
            return true;
        }

        public List<Product> Paging(int page, int pageSize, out long totalrows)
        {
            throw new NotImplementedException();
        }

        public List<Product> SearchPaging(int page, int pageSize, out long totalrows)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product entity)
        {
            var p = Builders<Product>.Update.Set("Name", entity.Name)
                .Set("Category", entity.Category)
                .Set("Details", entity.Details)
                .Set("Specifications", entity.Specifications)
                .Set("Feature", entity.Features)
                .Set("Inventory", entity.Inventory)
                .Set("Images", entity.Images);
            _context.Products.UpdateOne(x => x.Id == entity.Id, p);
            return true;
        }
        
    }
}
