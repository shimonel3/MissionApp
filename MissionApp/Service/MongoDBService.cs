using MissionApp.Entities;
using MissionApp.Managers;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using MissionApp.Exceptions;

namespace MissionApp.Service
{
    public class MongoDBService : IDatabaseService
    {
        private readonly IMongoCollection<Mission> _missionsCollection;

        public MongoDBService(MongoDbContext dbContext)
        {
            _missionsCollection = dbContext.Missions;
        }

        public void SaveNewMission(Mission mission)
        {
            _missionsCollection.InsertOne(mission);
        }

        public string? GetMostIsolatedCountry()
        {
            Task<string> country = findIsolatedCountryAsync();
            return country.Result;
        }

        public Mission? FindClosestMissionInDB(GeoJson2DCoordinates coordinates)
        {
            var mission = QueryClosestMission(coordinates);
            return mission?.Result;
        }

        public Mission? FindMissonByAddress(LocationDTO locationDTO)
        {
            return QueryFindMissionByAdress(locationDTO).Result;
        }

        private async Task<Mission> QueryFindMissionByAdress(LocationDTO locationDTO)
        {
            FilterDefinition<Mission> filter;

            if (!string.IsNullOrEmpty(locationDTO.Address))
            {
                filter = Builders<Mission>.Filter.Eq(m => m.Address, locationDTO.Address);
            }
            else if (locationDTO.Latitude.HasValue && locationDTO.Longitude.HasValue)
            {
                var targetLocation = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                    new GeoJson2DGeographicCoordinates(locationDTO.Longitude.Value, locationDTO.Latitude.Value));
                filter = Builders<Mission>.Filter.NearSphere(x => x.Location, targetLocation, maxDistance: 1);
            }
            else
            {
                throw new MissionException("Missing location in request");
            }

            var mission = await _missionsCollection.Find(filter).FirstOrDefaultAsync();
            return mission;
        }

        private async Task<Mission> QueryClosestMission(GeoJson2DCoordinates coordinates)
        {
            var targetPoint = new GeoJsonPoint<GeoJson2DCoordinates>(coordinates);
            var filter = Builders<Mission>.Filter.NearSphere(x => x.Location, targetPoint, minDistance: 1000);
            var closestMission = await _missionsCollection.Find(filter).FirstOrDefaultAsync();

            if (closestMission == null)
            {
                return null;
            }
            return closestMission;
        }

        private async Task<string> findIsolatedCountryAsync()
        {
            var agentMissionCount = _missionsCollection.Aggregate()
            .Group(m => m.CodeName, g => new { Agent = g.Key, MissionCount = g.Count() })
            .ToEnumerable();

            var isolatedAgents = agentMissionCount
                .Where(a => a.MissionCount == 1)
                .Select(a => a.Agent)
                .ToHashSet();

            var countryIsolationCount = await _missionsCollection.Aggregate()
                .Match(m => isolatedAgents.Contains(m.CodeName))
                .Group(m => m.Country, g => new { Country = g.Key, IsolatedAgentCount = g.Count() })
                .SortByDescending(g => g.IsolatedAgentCount)
                .FirstOrDefaultAsync();

            if (countryIsolationCount == null)
            {
                return "Unable to find any country with isolated agents";
            }
            return countryIsolationCount.Country;
        }
    }
}


