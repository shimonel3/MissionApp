using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MissionApp.Service;
using MissionApp.Managers;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var origLocationService = services.SingleOrDefault(
                d => d.ServiceType == typeof(ILocationService));

            if (origLocationService != null)
            {
                services.Remove(origLocationService);
            }

            var origMissionService = services.SingleOrDefault(
            d => d.ServiceType == typeof(MissionService));
            if (origMissionService != null)
            {
                services.Remove(origMissionService);
            }

            var origDatabaseService = services.SingleOrDefault(
            d => d.ServiceType == typeof(IDatabaseService));
            if (origDatabaseService != null)
            {
                services.Remove(origDatabaseService);
            }

            ILocationService locationService = new MockLocationService();
            var dbContextMock = new MongoDbContext("mongodb://localhost:27017/", "Mission_Test");
            IDatabaseService dbService = new MongoDBService(dbContextMock);
            services.AddSingleton(dbService);
            var missionService = new MissionService(locationService, dbService);
            var indexManager = new IndexManager(dbContextMock);
            indexManager.CreateIndexes();
            services.AddSingleton(missionService);
            services.AddSingleton(locationService);

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
            }
        });
    }
}