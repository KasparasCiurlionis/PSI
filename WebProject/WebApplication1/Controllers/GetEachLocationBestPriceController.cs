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
            prices = Functions.getEachLocationBestPrice();
            List<Price> result = new List<Price>();
            foreach (Price current in prices)
            {
                Price price = new Price();
                price.PriceID = current.PriceID;
                price.Price1 = current.Price1;
                price.GasTypeID = current.GasTypeID;
                price.LocationID = current.LocationID;
                result.Add(price);
            }

            return result;
        }

    }
}