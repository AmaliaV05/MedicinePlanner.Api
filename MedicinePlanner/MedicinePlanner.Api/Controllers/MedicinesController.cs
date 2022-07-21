using MedicinePlanner.BusinessLogic.DTOs;
using MedicinePlanner.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MedicinePlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicinesController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpGet("{idMedicine}")]
        public async Task<IActionResult> GetMedicine(int idMedicine)
        {
            return Ok(await _medicineService.GetMedicine(idMedicine));
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicines()
        {
            return Ok(await _medicineService.GetMedicines());
        }

        [HttpPut]
        public async Task<IActionResult> PutMedicine(int idMedicine, MedicineDTO medicineDTO)
        {
            return Ok(await _medicineService.UpdateMedicine(idMedicine, medicineDTO));
        }

        [HttpPost]
        public async Task<IActionResult> PostMedicine(MedicineDTO medicineDTO)
        {
            return Ok(await _medicineService.AddMedicine(medicineDTO));
        }
    }
}
