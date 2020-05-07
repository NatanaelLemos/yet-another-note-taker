using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NLemos.Api.Framework.Data;
using YetAnotherNoteTaker.Server.Entities;

namespace YetAnotherNoteTaker.Server.Data
{
    public class NoteTakerContext : MongoDBContext
    {
        public NoteTakerContext(string connectionString)
            : base(connectionString, "notetaker")
        {
            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }

        public IMongoCollection<User> Users => GetCollection<User>("users");
    }
}
