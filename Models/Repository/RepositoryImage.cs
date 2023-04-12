using MongoDB.Driver;
namespace TDStore.Models.Repository
{
    public class RepositoryImage : IRepositoryImage
    {
        TestMongoContext _context;
        public RepositoryImage(TestMongoContext context)
        {
            _context = context;
        }
        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        public List<ImageData> GetAll()
        {
            return _context.ImagesDatas.Find(FilterDefinition<ImageData>.Empty).ToList();
        }

        public ImageData GetById(int key)
        {
            throw new NotImplementedException();
        }

        public bool Insert(ImageData entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(ImageData entity)
        {
            throw new NotImplementedException();
        }
    }
}
