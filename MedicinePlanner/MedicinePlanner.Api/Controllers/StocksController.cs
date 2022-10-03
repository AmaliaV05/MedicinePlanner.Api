using MedicinePlanner.BusinessLogic.DTOs;
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

        [HttpGet("Unloading-Stock-List")]
        public async Task<IActionResult> GetUnloadingStockList()
        {
            return Ok(await _stockService.UnloadingStockList());
        }

        [HttpGet("Loading-Stock-List")]
        public async Task<IActionResult> GetLoadingStockList()
        {
            return Ok(await _stockService.LoadingStockList());
        }

        [HttpPut("Consumed-Medicine")]
        public async Task<IActionResult> PostUnloadingStock(ConsumedMedicineDTO medicine)
        {
            return Ok(await _stockService.ConsumedMedicine(medicine));
        }

        [HttpPost("Load-Stock/{idStock}")]
        public async Task<IActionResult> PostLoadingStock(int idStock, [FromBody] int pillsNumber)
        {
            return Ok(await _stockService.LoadStock(idStock, pillsNumber));
        }

        [HttpPost("Unload-Stock/{idStock}")]
        public async Task<IActionResult> PostUnloadingStock(int idStock, [FromBody] int pillsNumber)
        {
            return Ok(await _stockService.UnloadStock(idStock, pillsNumber));
        }
    }
}
