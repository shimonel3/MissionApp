using GoogleMapsApi;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using MissionApp.Service;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

public class LocationService : ILocationService
{
    private readonly string _apiKey;

    public LocationService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public GeoJson2DCoordinates GetCoordinates(string address)
    {
        Task<(double Latitude, double Longitude)?> geoCords = GetCoordinatesAsync(address);
        var coordinates = geoCords.Result;
        if (coordinates == null || !coordinates.HasValue)
        {
            return null;
        }
        var targetLocation = new GeoJson2DCoordinates(coordinates.Value.Longitude, coordinates.Value.Latitude);
        return targetLocation;
    }

    private async Task<(double Latitude, double Longitude)?> GetCoordinatesAsync(string address)
    {
        var geocodingRequest = new GeocodingRequest
        {
            Address = address,
            ApiKey = _apiKey
        };

        var geocodingResponse = await GoogleMaps.Geocode.QueryAsync(geocodingRequest);

        if (geocodingResponse.Status == Status.OK && geocodingResponse.Results.Any())
        {
            var location = geocodingResponse.Results.First().Geometry.Location;
            return (location.Latitude, location.Longitude);
        }
        return null;
    }
}