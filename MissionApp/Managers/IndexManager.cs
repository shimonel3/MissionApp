using MissionApp.Entities;
using MongoDB.Driver;
namespace MissionApp.Managers
{
    public class IndexManager
    {
        private readonly IMongoCollection<Mission> _missionsCollection;

        public IndexManager(MongoDbContext dbContext)
        {
            _missionsCollection = dbContext.Missions;
        }

        public void CreateIndexes()
        {
            var indexKeys = Builders<Mission>.IndexKeys;
            var agentIndex = new CreateIndexModel<Mission>(indexKeys.Ascending(m => m.CodeName));
            var countryIndex = new CreateIndexModel<Mission>(indexKeys.Ascending(m => m.Country));
            var locationIndex = new CreateIndexModel<Mission>(indexKeys.Geo2DSphere(m => m.Location));

            _missionsCollection.Indexes.CreateMany(new[] { agentIndex, countryIndex, locationIndex });
        }
    }
}
