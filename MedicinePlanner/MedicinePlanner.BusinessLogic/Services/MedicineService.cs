using AutoMapper;
using MedicinePlanner.BusinessLogic.DTOs;
using MedicinePlanner.BusinessLogic.Exceptions;
using MedicinePlanner.BusinessLogic.Interfaces;
using MedicinePlanner.Data.Data;
using MedicinePlanner.Data.Models;
using MedicinePlanner.Data.Models.Enum.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace MedicinePlanner.BusinessLogic.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MedicineService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MedicineDTO> GetMedicine(int idMedicine)
        {
            var medicine = await _context.Medicines
                .Where(m => m.Id == idMedicine)
                .OrderBy(m => m.Id)
                .Include(m => m.Stock)
                .Include(m => m.Plannings.Where(p => p.EndDate >= DateTime.Now))
                .ThenInclude(p => p.DailyPlannings)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            if(medicine == null)
            {
                throw new IdNotFoundException(nameof(Medicine), idMedicine);
            }
            return _mapper.Map<MedicineDTO>(medicine);
        }

        public async Task<IEnumerable<PlanningMlDTO>> GetMedicines()
        {
            return await _context.Plannings
                .Where(p => p.PauseEndDate >= DateTime.Now)
                .OrderBy(p => p.Id)
                .Include(p => p.Medicine)
                .ThenInclude(m => m.Stock)
                .Include(p => p.DailyPlannings)                
                .AsSplitQuery()
                .Select(p => _mapper.Map<PlanningMlDTO>(p))
                .ToListAsync();
        }       

        public async Task<ServiceResponse<MedicineDTO, string>> AddMedicine(MedicineDTO medicineDTO)
        {
            var serviceResponse = new ServiceResponse<MedicineDTO, string>();
            if (await CheckPlanningExists(medicineDTO))
            {
                serviceResponse.ResponseError = string.Format("A planning already exists for {0}", medicineDTO.Name);
                return serviceResponse;
            }
            using (TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled))
            {
                var medicine = new Medicine
                {
                    Name = medicineDTO.Name,
                    ExpirationDate = medicineDTO.ExpirationDate
                };
                _context.Medicines.Add(medicine);
                await _context.SaveChangesAsync();
                await AddStock(medicine, medicineDTO);
                var planning = await AddPlanning(medicine, medicineDTO);
                await AddDailyPlanning(planning, medicineDTO);
                serviceResponse.ResponseOk = _mapper.Map<MedicineDTO>(medicine);
                scope.Complete();
            }
            return serviceResponse;
        }

        public async Task DeleteMedicine(int idMedicine)
        {
            var medicine = await _context.Medicines
                .Where(m => m.Id == idMedicine)
                .OrderBy(m => m.Id)
                .Include(m => m.Plannings)
                .ThenInclude(p => p.DailyPlannings)
                .Include(m => m.Stock)
                .ThenInclude(s => s.LoadingStocks)
                .Include(m => m.Stock)
                .ThenInclude(s => s.UnloadingStocks)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            _context.Remove(medicine);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> CheckPlanningExists(MedicineDTO medicineDTO)
        {
            var startDate = medicineDTO.Plannings.First().StartDate;
            var planning = await _context.Plannings
                .Where(p => p.StartDate <= startDate && p.Medicine.Name == medicineDTO.Name)
                .FirstOrDefaultAsync();
            if (planning == null) 
            {
                return false;
            }
            return true;
        }

        private async Task AddStock(Medicine medicine, MedicineDTO medicineDTO)
        {
            await _context.Stocks.AddAsync(new Stock
            {
                Total = medicineDTO.Stock.Total,
                MedicineId = medicine.Id
            });
        }

        private async Task<Planning> AddPlanning(Medicine medicine, MedicineDTO medicineDTO)
        {
            var plannings = _mapper.Map<IEnumerable<Planning>>(medicineDTO.Plannings);
            if (medicine.Plannings == null)
            {
                var newPlanning = plannings.ElementAt(0);
                newPlanning.Medicine = medicine;
                await _context.Plannings.AddAsync(newPlanning);
            }
            else
            {
                medicine.Plannings.Add(plannings.ElementAt(0));
            }
            await _context.SaveChangesAsync();
            return plannings.ElementAt(0);
        }
        
        private async Task<IEnumerable<DailyPlanningDTO>> AddDailyPlanning(Planning planning, MedicineDTO medicineDTO)
        {
            var dailyPlannings = _mapper.Map<IEnumerable<DailyPlanning>>(medicineDTO.Plannings.ElementAt(0).DailyPlannings);
            if (!planning.EndDate.HasValue && !planning.PauseEndDate.HasValue)
            {
                for (DateTime date = planning.StartDate; date <= new DateTime().AddYears(100); date = date.AddDays(1))
                {
                    AddDailyTreatmentPlanning(planning, date, dailyPlannings);
                }
            }
            else if (planning.EndDate.HasValue)
            {
                for (DateTime date = planning.StartDate; date <= planning.EndDate; date = date.AddDays(1))
                {
                    AddDailyTreatmentPlanning(planning, date, dailyPlannings);
                }
                if (planning.PauseEndDate.HasValue)
                {
                    for (DateTime date = ((DateTime)planning.EndDate).AddDays(1); date <= planning.PauseEndDate; date = date.AddDays(1))
                    {
                        AddDailyPausePlanning(planning, date, dailyPlannings);
                    }
                }
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<IEnumerable<DailyPlanningDTO>>(planning.DailyPlannings);
        }

        private static void AddDailyTreatmentPlanning(Planning planning, DateTime date, IEnumerable<DailyPlanning> dailyPlannings)
        {
            foreach (DailyPlanning dailyPlanning in dailyPlannings)
            {
                planning.DailyPlannings.Add(new DailyPlanning
                {
                    IntakeTime = new DateTime(date.Year, date.Month, date.Day, dailyPlanning.IntakeTime.Hour, dailyPlanning.IntakeTime.Minute, 0),
                    Dosage = dailyPlanning.Dosage,
                    Consumed = false,
                    Message = PlanningMessage.NotConsumed,
                    Planning = planning
                });
            }
        }

        private static void AddDailyPausePlanning(Planning planning, DateTime date, IEnumerable<DailyPlanning> dailyPlannings)
        {
            foreach (DailyPlanning dailyPlanning in dailyPlannings)
            {
                planning.DailyPlannings.Add(new DailyPlanning
                {
                    IntakeTime = new DateTime(date.Year, date.Month, date.Day, dailyPlanning.IntakeTime.Hour, dailyPlanning.IntakeTime.Minute, 0),
                    Dosage = dailyPlanning.Dosage,
                    Consumed = false,
                    Message = PlanningMessage.Pause,
                    Planning = planning
                });
            }
        }        
    }
}
