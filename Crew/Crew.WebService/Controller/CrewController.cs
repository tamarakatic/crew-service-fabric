using System;
using System.Threading.Tasks;
using System.Web.Http;
using Crew.Tracker.Interfaces;
using Crew.CrewMemberActor.Interfaces.Models;

namespace Crew.WebService.Controller
{
    public class CrewController : ApiController
    {
        [HttpGet]
        [Route("")]
        public string Index()
        {
            return "Welcome to Crew application!";
        }

        [HttpPost]
        [Route("crew/add")]
        public async Task<bool> Add(CrewMemberActor.Interfaces.Models.Crew crew)
        {
            var newCrew = TrackerConncetionFactory.CreateCrew();
            await newCrew.CreateCrew(crew);
            return true;
        }
        [HttpGet]
        [Route("crew/{crewName}/show")]
        public async Task<object> Show(String crewName)
        {
            var crew = TrackerConncetionFactory.CreateCrew();
            return await crew.GetCrewByName(crewName);
        }

        [HttpPost]
        [Route("crew/{crewName}/location")]
        public async Task<bool> Location(String crewName, CrewLocation location)
        {
            var reporter = TrackerConncetionFactory.UpdateCrewLocation();
            await reporter.UpdateCrewLocation(crewName, location);
            return true;
        }

        [HttpGet]
        [Route("crew/{crewName}/lastlocation")]
        public async Task<object> LastLocation(String crewName)
        {
            var viewer = TrackerConncetionFactory.GetLastCrewLocation();
            var location = await viewer.GetLastCrewLocation(crewName);
            return location == null ? null : new
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
        }

        [HttpGet]
        [Route("crew/{crewName}/lastseen")]
        public async Task<DateTime?> LastSeen(String crewName)
        {
            var viewer = TrackerConncetionFactory.GetLastCrewLocation();
            var location = await viewer.GetLastCrewLocation(crewName);
            return location.Timestamp;
        }

        [HttpPost]
        [Route("crew/{crewName}/members")]
        public async Task<bool> AddMember(String crewName, Member member)
        {
            var crewMember = TrackerConncetionFactory.CreateMember();
            await crewMember.CreateMember(crewName, member);
            return true;
        }

        [HttpPost]
        [Route("crew/{crewName}/plannedassignment")]
        public async Task<bool> AddPlannedAssignment(String crewName, Planned assignment)
        {
            var crewAssignment = TrackerConncetionFactory.CreateAssignment();
            await crewAssignment.AddPlannedAssignment(crewName, assignment);
            return true;
        }

        [HttpPost]
        [Route("crew/{crewName}/unplannedassignment")]
        public async Task<bool> AddUnplannedAssignment(String crewName, Unplanned assignment)
        {
            var crewAssignment = TrackerConncetionFactory.CreateAssignment();
            await crewAssignment.AddUnplannedAssignment(crewName, assignment);
            return true;
        }
    }
}
