using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Data.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetGasStationLocationController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<GetGasStationLocationController> _logger;

        public GetGasStationLocationController(ILogger<GetGasStationLocationController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetGasStationLocationList")]
        public List<WebApplication1.Data.Repositories.Location> Get(int pkey)
        {
            List<WebApplication1.Data.Repositories.Location> temp = new List<WebApplication1.Data.Repositories.Location>();
            List<WebApplication1.Data.Repositories.Location> temp2 = new List<WebApplication1.Data.Repositories.Location>();

            temp2 = Functions.getGasStationLocationList(pkey);
            foreach (WebApplication1.Data.Repositories.Location current in temp2)
            {
                WebApplication1.Data.Repositories.Location location = new WebApplication1.Data.Repositories.Location();
                location.LocationID = current.LocationID;
                location.LocationName = current.LocationName;
                location.GasStationID = current.GasStationID;
                temp.Add(location);

            }
            var asd = temp; 
            
            return temp;
        }

    }
}