using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WebApplication1.Data.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetGasStationListController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<GetGasStationListController> _logger;

        public GetGasStationListController(ILogger<GetGasStationListController> logger)
        {
            _logger = logger;
        }


        [HttpGet(Name = "GetGasStationList")]
        public List<WebApplication1.Data.Repositories.GasStation> Get()
        {
            List<WebApplication1.Data.Repositories.GasStation> gasStations = new List<WebApplication1.Data.Repositories.GasStation>();
            List<WebApplication1.Data.Repositories.GasStation> gasStations2 = new List<WebApplication1.Data.Repositories.GasStation>();

            gasStations2 = Functions.getGasStationList();
            // now gas Stations2 contains a lot of data
            // and our gas stations list is empty
            // we need to get in some data from gas stations 2
            // we need to get the gas station name and the gas station id
            foreach (WebApplication1.Data.Repositories.GasStation current in gasStations2)
            {
                WebApplication1.Data.Repositories.GasStation gasStation = new WebApplication1.Data.Repositories.GasStation();
                gasStation.GasStationID = current.GasStationID;
                gasStation.GasStationName = current.GasStationName;
                gasStations.Add(gasStation);
            }
            return gasStations;

        }

    }
}