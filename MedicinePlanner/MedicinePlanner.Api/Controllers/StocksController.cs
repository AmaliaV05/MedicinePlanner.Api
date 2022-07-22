using MedicinePlanner.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MedicinePlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPut("Consumed-Medicine/{idDailyPlanning}")]
        public async Task<IActionResult> PostUnloadingStock(int idDailyPlanning)
        {
            return Ok(await _stockService.ConsumedMedicine(idDailyPlanning));
        }

        [HttpPost("Load-Stock/{idStock}")]
        public async Task<IActionResult> PostLoadingStock(int idStock, int pillsNumber)
        {
            return Ok(await _stockService.LoadStock(idStock, pillsNumber));
        }

        [HttpPost("Unload-Stock/{idStock}")]
        public async Task<IActionResult> PostUnloadingStock(int idStock, int pillsNumber)
        {
            return Ok(await _stockService.UnloadStock(idStock, pillsNumber));
        }        
    }
}
