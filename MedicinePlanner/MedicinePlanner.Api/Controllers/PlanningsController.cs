using MedicinePlanner.BusinessLogic.DTOs;
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

        [HttpGet("{idMedicine}")]
        public async Task<IActionResult> GetPlanning(int idMedicine)
        {
            return Ok(await _planningService.GetPlanning(idMedicine));
        }

        [HttpGet]
        public async Task<IActionResult> GetPlannings()
        {
            return Ok(await _planningService.GetPlannings());
        }

        [HttpPost]
        public async Task<IActionResult> PostPlanning(int idMedicine, PlanningDTO planningDTO)
        {
            return Ok(await _planningService.AddPlanning(idMedicine, planningDTO));
        }

        [HttpPut]
        public async Task<IActionResult> ConsumeMedicine(int idDailyPlanning)
        {
            return Ok(await _planningService.ConsumeMedicine(idDailyPlanning));
        }
    }
}
