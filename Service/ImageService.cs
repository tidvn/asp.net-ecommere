using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TDStore.Models;

namespace TDStore.Service
{
    public class ImageService
    {
        private readonly IMongoCollection<ImageData> _imageCollection;
        public ImageService(
        IOptions<MongoDbSettings> MongoDbSettings)
        {
            var mongoClient = new MongoClient(
                MongoDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                MongoDbSettings.Value.DatabaseName);

            _imageCollection = mongoDatabase.GetCollection<ImageData>("Image");
        }
        public async Task<List<ImageData>> GetAllAsync() =>
        await _imageCollection.Find(_ => true).ToListAsync();

        public async Task<ImageData?> GetByIdAsync(string id) =>
            await _imageCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ImageData newImage) =>
            await _imageCollection.InsertOneAsync(newImage);

        public async Task UpdateAsync(string id, ImageData updatedImage) =>
            await _imageCollection.ReplaceOneAsync(x => x.Id == id, updatedImage);

        public async Task RemoveAsync(string id) =>
            await _imageCollection.DeleteOneAsync(x => x.Id == id);

    }
}
