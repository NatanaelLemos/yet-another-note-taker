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
            BsonClassMap.RegisterClassMap<Notebook>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<Note>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<Settings>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }

        public IMongoCollection<User> Users => GetCollection<User>("users");

        public IMongoCollection<Notebook> Notebooks => GetCollection<Notebook>("notebooks");

        public IMongoCollection<Note> Notes => GetCollection<Note>("notes");

        public IMongoCollection<Settings> Settings => GetCollection<Settings>("settings");
    }
}
