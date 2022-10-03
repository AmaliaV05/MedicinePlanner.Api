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

namespace MedicinePlanner.BusinessLogic.Services
{
    public class StockService : IStockService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StockService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UnloadingStockDTO>> UnloadingStockList()
        {
            return await _context.UnloadingStocks
                .OrderByDescending(u => u.UnloadingDate)
                .Include(u => u.Stock)
                .ThenInclude(s => s.Medicine)
                .Select(u => _mapper.Map<UnloadingStockDTO>(u))
                .ToListAsync();
        }

        public async Task<IEnumerable<LoadingStockDTO>> LoadingStockList()
        {
            return await _context.LoadingStocks
                .OrderByDescending(l => l.LoadingDate)
                .Include(l => l.Stock)
                .ThenInclude(s => s.Medicine)
                .Select(l => _mapper.Map<LoadingStockDTO>(l))
                .ToListAsync();
        }        

        public async Task<ServiceResponse<DailyPlanningDTO, string>> ConsumedMedicine(ConsumedMedicineDTO consumedMedicine)
        {
            var serviceResponse = new ServiceResponse<DailyPlanningDTO, string>();
            var medicines = await _context.Medicines
                .Where(m => m.Name == consumedMedicine.MedicineName)
                .OrderBy(m => m.Id)
                .Include(m => m.Plannings)
                .ThenInclude(p => p.DailyPlannings.Where(d => d.IntakeTime == consumedMedicine.IntakeTime))
                .Include(m => m.Stock)
                .ThenInclude(s => s.UnloadingStocks)
                .AsSplitQuery()
                .ToListAsync();
            foreach (Medicine medicine in medicines)
            {
                foreach (Planning planning in medicine.Plannings)
                {
                    if (planning.DailyPlannings.Count > 0)
                    {
                        if (medicine.Stock.Total < planning.DailyPlannings.First().Dosage)
                        {
                            serviceResponse.ResponseError = string.Format("Cannot use more than {0} pills", medicine.Stock.Total);
                            return serviceResponse;
                        }
                        else
                        {
                            var unloadingStock = new UnloadingStock
                            {
                                PillsNumber = planning.DailyPlannings.First().Dosage,
                                UnloadingDate = DateTime.UtcNow
                            };
                            medicine.Stock.UnloadingStocks.Add(unloadingStock);
                            medicine.Stock.Total -= planning.DailyPlannings.First().Dosage;
                            _context.Entry(medicine.Stock).State = EntityState.Modified;
                            planning.DailyPlannings.First().Consumed = true;
                            planning.DailyPlannings.First().Message = PlanningMessage.Consumed;
                            _context.Entry(planning.DailyPlannings.First()).State = EntityState.Modified;                            
                            await _context.SaveChangesAsync();
                            var dailyPlanningDTO = _mapper.Map<DailyPlanningDTO>(planning.DailyPlannings.First());
                            serviceResponse.ResponseOk = dailyPlanningDTO;                            
                        }
                    }
                }
            }
            return serviceResponse;
        }

        public async Task<LoadingStockDTO> LoadStock(int idStock, int pillsNumber)
        {
            var actualStock = await _context.Stocks
                .Where(s => s.Id == idStock)
                .Include(s => s.LoadingStocks)
                .FirstOrDefaultAsync();
            if (actualStock == null)
            {
                throw new IdNotFoundException(nameof(Stock), idStock);
            }
            var loadingStock = new LoadingStock
            {
                PillsNumber = pillsNumber,
                LoadingDate = DateTime.UtcNow
            };
            actualStock.LoadingStocks.Add(loadingStock);
            actualStock.Total += pillsNumber;
            await _context.SaveChangesAsync();
            var loadingStockDTO = _mapper.Map<LoadingStockDTO>(loadingStock);
            return loadingStockDTO;
        }

        public async Task<ServiceResponse<UnloadingStockDTO, string>> UnloadStock(int idStock, int pillsNumber)
        {
            var serviceResponse = new ServiceResponse<UnloadingStockDTO, string>();
            var actualStock = await _context.Stocks
                .Where(s => s.Id == idStock)
                .Include(s => s.UnloadingStocks)
                .FirstOrDefaultAsync();
            if (actualStock == null)
            {
                throw new IdNotFoundException(nameof(Stock), idStock);
            }
            if (actualStock.Total < pillsNumber)
            {
                serviceResponse.ResponseError = string.Format("Cannot unload more than {0} pills", actualStock.Total);
                return serviceResponse;
            }
            var unloadingStock = new UnloadingStock 
            {
                PillsNumber = pillsNumber,
                UnloadingDate = DateTime.UtcNow
            };
            actualStock.UnloadingStocks.Add(unloadingStock);
            actualStock.Total -= pillsNumber;
            await _context.SaveChangesAsync();
            var unloadingStockDTO = _mapper.Map<UnloadingStockDTO>(unloadingStock);
            serviceResponse.ResponseOk = unloadingStockDTO;
            return serviceResponse;
        }        
    }
}
