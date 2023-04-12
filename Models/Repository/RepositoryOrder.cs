using MongoDB.Driver; 
namespace TDStore.Models.Repository
{
    public class RepositoryOrder : IRepositoryOder
    {
        TestMongoContext _context;
        public RepositoryOrder(TestMongoContext context)
        {
            _context = context;
        }
        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            return _context.Orders.Find(FilterDefinition<Order>.Empty).ToList();
        }

        public Order GetById(int key)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Order entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
