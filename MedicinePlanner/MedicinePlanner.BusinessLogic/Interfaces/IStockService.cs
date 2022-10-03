using MedicinePlanner.BusinessLogic.DTOs;
using MedicinePlanner.BusinessLogic.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicinePlanner.BusinessLogic.Interfaces
{
    public interface IStockService
    {
        Task<IEnumerable<UnloadingStockDTO>> UnloadingStockList();
        Task<IEnumerable<LoadingStockDTO>> LoadingStockList();
        Task<ServiceResponse<DailyPlanningDTO, string>> ConsumedMedicine(ConsumedMedicineDTO medicine);
        Task<LoadingStockDTO> LoadStock(int idStock, int pillsNumber);
        Task<ServiceResponse<UnloadingStockDTO, string>> UnloadStock(int idStock, int pillsNumber); 
    }
}
