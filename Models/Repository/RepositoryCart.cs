using MongoDB.Driver;

namespace TDStore.Models.Repository
{
    public class RepositoryCart : IRepositoryCart
    {
        TestMongoContext _context;
        public RepositoryCart(TestMongoContext context)
        {
            _context = context;
        }
        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        public List<CartItem> GetAll()
        {
            return _context.CartItems.Find(FilterDefinition<CartItem>.Empty).ToList();
        }

        public CartItem GetById(int key)
        {
            throw new NotImplementedException();
        }

        public bool Insert(CartItem entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(CartItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
