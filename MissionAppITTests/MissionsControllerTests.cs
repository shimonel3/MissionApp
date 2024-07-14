using System.Text;
using Newtonsoft.Json;
using MissionApp.Entities;

public class MissionsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public MissionsControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task AddMission_ReturnsOk()
    {
        var missionRequest = new MissionDTO
        {
            CodeName = "099",
            Country = "Brazil",
            Address = "Avenida Vieira Souto 168 Ipanema, Rio de Janeiro",
            Date = "Dec 17, 1995, 9:45:17 PM"
        };

        var content = new StringContent(JsonConvert.SerializeObject(missionRequest), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/Mission", content);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Equal("Mission added successfully.", responseString);
    }

    [Fact]
    public async Task FindClosest_ReturnsMission()
    {
        var locationRequest = new LocationDTO
        {
            Address = "Avenida Vieira Souto 168 Ipanema, Rio de Janeiro"
        };

        var content = new StringContent(JsonConvert.SerializeObject(locationRequest), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/find-closest", content);
        response.EnsureSuccessStatusCode();
        var mission = JsonConvert.DeserializeObject<Mission>(await response.Content.ReadAsStringAsync());
        Assert.NotNull(mission);
        Assert.Equal("Brazil", mission.Country);
    }

    [Fact]
    public async Task FindIsolatedAgents()
    {

        var response = await _client.GetAsync("/countries-by-isolation");
        response.EnsureSuccessStatusCode();
        var country = await response.Content.ReadAsStringAsync();
        Assert.NotNull(country);
        Assert.Equal("Morocco", country);
    }
}
