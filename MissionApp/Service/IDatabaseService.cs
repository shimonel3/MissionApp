using MissionApp.Entities;
using MongoDB.Driver.GeoJsonObjectModel;

namespace MissionApp.Service
{
    public interface IDatabaseService
    {
        /// <summary>
        /// Saves a new mission to the database.
        /// </summary>
        /// <param name="mission">The mission object to be saved.</param>
        public void SaveNewMission(Mission mission);

        /// <summary>
        /// Finds the most isolated country. An isolated agent is defined as an agent that participated in a single mission.
        /// </summary>
        /// <returns>The name of the country with the highest number of isolated agents, or null if no isolated country is found.</returns>
        public string? GetMostIsolatedCountry();

        /// <summary>
        /// Finds the closest mission in the database to the provided coordinates.
        /// </summary>
        /// <param name="coordinates">The GeoJson2DCoordinates to search for the nearest mission.</param>
        /// <returns>The closest mission to the provided coordinates, or null if no mission is found.</returns>
        public Mission? FindClosestMissionInDB(GeoJson2DCoordinates coordinates);

        /// <summary>
        /// Finds a mission in the database by the provided address or coordinates.
        /// </summary>
        /// <param name="locationDTO">The LocationDTO containing the address or coordinates to search for the mission.</param>
        /// <returns>The mission matching the provided address or coordinates, or null if no mission is found.</returns>
        public Mission? FindMissonByAddress(LocationDTO locationDTO);
    }
}
