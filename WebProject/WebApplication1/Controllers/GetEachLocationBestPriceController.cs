using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WebApplication1.Data.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetEachLocationBestPriceController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<GetEachLocationBestPriceController> _logger;

        public GetEachLocationBestPriceController(ILogger<GetEachLocationBestPriceController> logger)
        {
            _logger = logger;
        }
        

        [HttpGet(Name = "GetEachLocationBestPrice")]
        public List<Price> Get()
        {
            List<Price> prices = new List<Price>();
            // make a list but with maximum amount of 10
            prices = Functions.getEachLocationBestPrice();
            return prices;
        }

    }
}