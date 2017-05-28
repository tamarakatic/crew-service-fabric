﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using Crew.Tracker.Interfaces;

namespace Crew.WebService
{
    public class TrackerController : ApiController
    {
        [HttpGet]
        [Route("")]
        public string Index()
        {
            return "Welcome to Crew application!";
        }

        [HttpPost]
        [Route("locations")]
        public async Task<bool> Log(Location location)
        {
            var reporter = TrackerConncetionFactory.CreateLocationReporter();
            await reporter.ReportLocation(location);
            return true;
        }

        [HttpGet]
        [Route("crew/{crewId}/lastseen")]
        public async Task<DateTime?> LastSeen(Guid crewId)
        {
            var viewer = TrackerConncetionFactory.CreateLocationViewer();
            return await viewer.GetLastReportTime(crewId);
        }

        [HttpGet]
        [Route("crew/{crewId}/lastlocation")]
        public async Task<object> LastLocation(Guid crewId)
        {
            var viewer = TrackerConncetionFactory.CreateLocationViewer();
            var location = await viewer.GetLastCrewLocation(crewId);
            return location == null ? null : new
            {
                Latitude = location.Value.Key,
                Longitude = location.Value.Value
            };
        }
    }
}