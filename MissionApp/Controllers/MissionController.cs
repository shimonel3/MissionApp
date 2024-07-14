using Microsoft.AspNetCore.Mvc;
using MissionApp.Entities;
using MissionApp.Service;
using MissionApp.Validations;
namespace MissionApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MissionController : ControllerBase
    {
        private IDatabaseService _dbService;
        private ILocationService _locationService;
        private MissionService _missionService;
        public MissionController(IDatabaseService dbService, ILocationService locationService, MissionService missionService)
        {
            _dbService = dbService;
            _locationService = locationService;
            _missionService = missionService;
        }

        [HttpPost("/find-closest")]
        public ActionResult<Mission> FindClosestMission([FromBody] LocationDTO address)
        {
            try
            {
                Mission? mission = _missionService.FindClosestMission(address);
                if (mission == null)
                {
                    return BadRequest("Unable to find any close mission.");
                }
                return Ok(mission);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/countries-by-isolation")]
        public ActionResult<string> GetMostIsolatedCountry()
        {
            string? country = _dbService.GetMostIsolatedCountry();
            if (string.IsNullOrEmpty(country))
            {
                return Ok("Unable to find any country with isolated agents");
            }
            return Ok(country);
        }

        [HttpPost]
        public ActionResult<Mission> CreateMission([FromBody] MissionDTO missionDto)
        {
            try
            {
                var mission = _missionService.ConvertMissionDtoToMission(missionDto);
                MissionCreationValidator.validate(mission);
                _dbService.SaveNewMission(mission);
                return Ok("Mission added successfully.");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
