using MongoDB.Driver.GeoJsonObjectModel;

namespace MissionApp.Service
{
    public interface ILocationService
    {
        /// <summary>
        /// Gets the geographic coordinates for the provided address.
        /// </summary>
        /// <param name="address">The address to get the coordinates for.</param>
        /// <returns>A GeoJson2DCoordinates object containing the latitude and longitude of the address.</returns>
        public GeoJson2DCoordinates GetCoordinates(string address);
    }
}
