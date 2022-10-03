using AutoMapper;
using MedicinePlanner.BusinessLogic.DTOs;
using MedicinePlanner.BusinessLogic.Exceptions;
using MedicinePlanner.BusinessLogic.Interfaces;
using MedicinePlanner.Data.Data;
using MedicinePlanner.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MedicinePlanner.BusinessLogic.Services
{
    public class PlanningService : IPlanningService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PlanningService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlanningMlDTO> GetPlanning(int idPlanning)
        {
            var planning = await _context.Plannings
                .Where(p => p.Id == idPlanning)
                .OrderBy(m => m.Id)
                .Include(p => p.DailyPlannings)
                .Include(p => p.Medicine)
                .ThenInclude(m => m.Stock)                
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            if (planning == null)
            {
                throw new IdNotFoundException(nameof(Planning), idPlanning);
            }
            return _mapper.Map<PlanningMlDTO>(planning);
        }        

        public async Task<PlanningDTO> ApproveNewPlanning(int idMedicine)
        {
            var medicine = await _context.Medicines
                .Where(m => m.Id == idMedicine)
                .Include(p => p.Plannings)
                .FirstOrDefaultAsync();
            var newPlanning = new Planning
            {
                StartDate = medicine.Plannings.Last().PauseEndDate.Value.AddDays(1),
                EndDate = medicine.Plannings.Last().PauseEndDate.Value.AddDays(1) + ((DateTime)medicine.Plannings.Last().EndDate).Subtract(medicine.Plannings.Last().StartDate),
                PauseEndDate = medicine.Plannings.Last().PauseEndDate.Value.AddDays(1) + medicine.Plannings.Last().PauseEndDate.Value.Subtract((DateTime)medicine.Plannings.Last().EndDate),
                Medicine = medicine
            };
            _context.Plannings.Add(newPlanning);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlanningDTO>(medicine.Plannings.Last());
        }        
    }
}
