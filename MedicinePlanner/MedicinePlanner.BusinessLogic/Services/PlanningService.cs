using AutoMapper;
using MedicinePlanner.BusinessLogic.DTOs;
using MedicinePlanner.BusinessLogic.Exceptions;
using MedicinePlanner.BusinessLogic.Interfaces;
using MedicinePlanner.Data.Data;
using MedicinePlanner.Data.Models;
using MedicinePlanner.Data.Models.Enum.Utils;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<PlanningDTO> GetPlanning(int idMedicine)
        {
            var planning = await _context.Medicines
                .Where(m => m.Id == idMedicine)
                .Include(m => m.Plannings)
                .ThenInclude(p => p.DailyPlannings)
                .LastOrDefaultAsync();
            if (planning == null)
            {
                throw new IdNotFoundException(nameof(Medicine), idMedicine);
            }
            return _mapper.Map<PlanningDTO>(planning);
        }

        public async Task<IEnumerable<PlanningDTO>> GetPlannings()
        {
            return await _context.Plannings
                .Include(p => p.DailyPlannings)
                .Select(p => _mapper.Map<PlanningDTO>(p))
                .ToListAsync();
        }

        public async Task<PlanningDTO> AddPlanning(int idMedicine, PlanningDTO planningDTO)
        {
            var medicine = await _context.Medicines
                .Where(m => m.Id == idMedicine)
                .Include(p => p.Plannings)
                .FirstOrDefaultAsync();
            var planning = _mapper.Map<Planning>(planningDTO);
            medicine.Plannings.Add(planning);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlanningDTO>(planning);
        }

        public async Task<DailyPlanningDTO> ConsumeMedicine(int idDailyPlanning)
        {
            var dailyPlanning = await _context.DailyPlannings
                .Where(d => d.Id == idDailyPlanning)
                .FirstOrDefaultAsync();
            if (dailyPlanning == null)
            {
                throw new IdNotFoundException(nameof(DailyPlanning), idDailyPlanning);
            }
            dailyPlanning.Consumed = true;
            dailyPlanning.Message = PlanningMessage.Consumed;
            _context.Entry(dailyPlanning).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<DailyPlanningDTO>(dailyPlanning);
        }

        public async Task<IEnumerable<DailyPlanningDTO>> AddDailyPlanning(int idPlanning, IEnumerable<DailyPlanningDTO> dailyPlanningDTO)
        {
            var planning = await _context.Plannings
                .Where(p => p.Id == idPlanning)
                .Include(p => p.DailyPlannings)
                .FirstOrDefaultAsync();
            if (planning == null)
            {
                throw new IdNotFoundException(nameof(Planning), idPlanning);
            }
            var dailyPlannings = _mapper.Map<IEnumerable<DailyPlanning>>(dailyPlanningDTO);
            foreach (DailyPlanning dailyPlanning in dailyPlannings)
            {
                dailyPlanning.Consumed = false;
                dailyPlanning.Message = PlanningMessage.NotConsumed;
                planning.DailyPlannings.Add(dailyPlanning);
            }
            
            await _context.SaveChangesAsync();
            return _mapper.Map<IEnumerable<DailyPlanningDTO>>(dailyPlannings);
        }
    }
}
