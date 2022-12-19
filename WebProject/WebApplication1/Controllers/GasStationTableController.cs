using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GasStationTableController : ControllerBase
    {

        private readonly ILogger<GasStationTableController> _logger;
        private readonly IGasStationTable _gasStationTable;

        public GasStationTableController(ILogger<GasStationTableController> logger, IGasStationTable gasStationTable)
        {
            _logger = logger;
            _gasStationTable = gasStationTable;
        }

        [HttpGet(Name = "GetGasStationTable")]
        [ProducesResponseType(typeof(List<List<String>>), 200)]
        public IActionResult Get(int id)
        {
            var gasStationTable = _gasStationTable.GetTable(id);

            if (gasStationTable == null)
            {
                return NotFound();
            }

            return Ok(gasStationTable);
        }

        
    }
}