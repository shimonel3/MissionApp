using MissionApp.Entities;
using MissionApp.Exceptions;

namespace MissionApp.Service
{
    public class MissionService
    {
        private ILocationService _locationService;
        private IDatabaseService _dataBaseService;

        public MissionService(ILocationService locationService, IDatabaseService databaseService)
        {
            _locationService = locationService;
            _dataBaseService = databaseService;
        }

        public Mission? FindClosestMission(LocationDTO locationDTO)
        {
            if (locationDTO == null)
            {
                throw new MissionException("Find closest mission request contains no parameters.");
            }
            Mission? mission = _dataBaseService.FindMissonByAddress(locationDTO);
            if (mission == null)
            {
                throw new MissionException("Failed to Search for closest mission. Please provide a location of a previously existing mission address.");
            }
            var coordinates = mission.Location;
            return _dataBaseService.FindClosestMissionInDB(coordinates);
        }

        public Mission ConvertMissionDtoToMission(MissionDTO missionDto)
        {
            Mission mission = new Mission();
            if (missionDto == null)
            {
                throw new Exception("Missing mission data in mission creation request. Please contact support");
            }
            mission.Address = missionDto.Address;
            mission.Date = missionDto.Date;
            mission.CodeName = missionDto.CodeName;
            mission.Country = missionDto.Country;
            if (missionDto.Address != null)
            {
                var coords = _locationService.GetCoordinates(missionDto.Address);
                mission.Location = coords;
            }
            return mission;
        }
    }
}
