using MedicinePlanner.BusinessLogic.DTOs;
using MedicinePlanner.BusinessLogic.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicinePlanner.BusinessLogic.Interfaces
{
    public interface IMedicineService
    {
        Task<MedicineDTO> GetMedicine(int id);
        Task<IEnumerable<PlanningMlDTO>> GetMedicines();
        Task<ServiceResponse<MedicineDTO, string>> AddMedicine(MedicineDTO medicineDTO);
        Task DeleteMedicine(int idMedicine);
    }
}
