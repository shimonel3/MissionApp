using MissionApp.Entities;
using MongoDB.Driver;

namespace MissionApp.Managers
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Mission> Missions => _database.GetCollection<Mission>("Missions");
    }
}
