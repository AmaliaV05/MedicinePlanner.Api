using MedicinePlanner.BusinessLogic.DTOs;
using MedicinePlanner.BusinessLogic.Services;
using System.Threading.Tasks;

namespace MedicinePlanner.BusinessLogic.Interfaces
{
    public interface IStockService
    {
        Task<LoadingStockDTO> LoadStock(int idStock, int pillsNumber);
        Task<ServiceResponse<UnloadingStockDTO, string>> UnloadStock(int idStock, int pillsNumber);
        Task<ServiceResponse<DailyPlanningDTO, string>> ConsumedMedicine(int idDailyPlanning);
    }
}
