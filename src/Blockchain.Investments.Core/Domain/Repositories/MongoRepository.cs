using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using Blockchain.Investments.Core.Model;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Blockchain.Investments.Core.Repositories
{
    public class MongoRepository<T> : IRepository<T>
        where T : BaseEntity, new()
    {
        private readonly AppConfig _optionsAccessor;
        MongoClient _client;
        IMongoDatabase _db;
        string _collection;
        public MongoRepository(IOptions<AppConfig> optionsAccessor) 
        {
            _optionsAccessor = optionsAccessor.Value;
            _client = new MongoClient(_optionsAccessor.MONGOLAB_URI);
            _db = _client.GetDatabase(Constants.DatabaseName);
            _collection = typeof(T).Name;
        }
        public virtual IEnumerable<T> FindAll()
        {
            return _db.GetCollection<T>(_collection).Find(r => true).ToList();
        }
        
        public virtual T FindById(string objectId)
        {
            
            var filter = Builders<T>.Filter.Eq(r => r.ObjectId, new ObjectId(objectId));
            return _db.GetCollection<T>(_collection).Find(filter).First();
        }
 
        public virtual T Create(T p)
        {
            _db.GetCollection<T>(_collection).InsertOne(p);
            return p;
        }
        
        public virtual void Update(string objectId, T p)
        {
            var currentObjectId = new ObjectId(objectId);
            p.ObjectId = currentObjectId;
            var filter = Builders<T>.Filter.Eq(r => r.ObjectId, currentObjectId);
            var operation = _db.GetCollection<T>(_collection).FindOneAndReplace(filter, p);
        }
        public virtual void Remove(string objectId)
        {
            var filter = Builders<T>.Filter.Eq(r => r.ObjectId, new ObjectId(objectId));
            var operation = _db.GetCollection<T>(_collection).FindOneAndDelete(filter);
        }
    }
}