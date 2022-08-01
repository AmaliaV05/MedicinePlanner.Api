using AutoMapper;
using MedicinePlanner.BusinessLogic.DTOs;
using MedicinePlanner.BusinessLogic.Exceptions;
using MedicinePlanner.BusinessLogic.Interfaces;
using MedicinePlanner.Data.Data;
using MedicinePlanner.Data.Models;
using MedicinePlanner.Data.Models.Enum.Utils;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<ServiceResponse<DailyPlanningDTO, string>> ConsumedMedicine(int idDailyPlanning)
        {
            var serviceResponse = new ServiceResponse<DailyPlanningDTO, string>();
            var dailyPlanning = await _context.DailyPlannings
                .Where(d => d.Id == idDailyPlanning)
                .Include(d => d.Planning)
                .ThenInclude(d => d.Medicine)
                .ThenInclude(m => m.Stock)
                .ThenInclude(s => s.UnloadingStocks)
                .FirstOrDefaultAsync();
            if (dailyPlanning == null)
            {
                throw new IdNotFoundException(nameof(DailyPlanning), idDailyPlanning);
            }
            if (dailyPlanning.Planning.Medicine.Stock.Total < dailyPlanning.Dosage)
            {
                serviceResponse.ResponseError = string.Format("Cannot use more than {0} pills", dailyPlanning.Planning.Medicine.Stock.Total);
                return serviceResponse;
            }
            var unloadingStock = new UnloadingStock
            {
                PillsNumber = dailyPlanning.Dosage,
                UnloadingDate = DateTime.Now
            };
            dailyPlanning.Planning.Medicine.Stock.UnloadingStocks.Add(unloadingStock);
            dailyPlanning.Planning.Medicine.Stock.Total -= dailyPlanning.Dosage;
            dailyPlanning.Consumed = true;
            dailyPlanning.Message = PlanningMessage.Consumed;
            _context.Entry(dailyPlanning).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var dailyPlanningDTO = _mapper.Map<DailyPlanningDTO>(dailyPlanning);
            serviceResponse.ResponseOk = dailyPlanningDTO;
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
                LoadingDate = DateTime.Now
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
                UnloadingDate = DateTime.Now
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
