using MedicinePlanner.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MedicinePlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanningsController : ControllerBase
    {
        private readonly IPlanningService _planningService;

        public PlanningsController(IPlanningService planningService)
        {
            _planningService = planningService;
        }        

        [HttpGet("{idPlanning}")]
        public async Task<IActionResult> GetPlanning(int idPlanning)
        {
            return Ok(await _planningService.GetPlanning(idPlanning));
        }

        [HttpPost("Approve-Planning")]
        public async Task<IActionResult> ApproveNewPlanning(int idMedicine)
        {
            return Ok(await _planningService.ApproveNewPlanning(idMedicine));
        }
    }
}
