using MedicinePlanner.BusinessLogic.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicinePlanner.BusinessLogic.Interfaces
{
    public interface IMedicineService
    {
        Task<MedicineDTO> GetMedicine(int id);
        Task<IEnumerable<MedicineDTO>> GetMedicines();
        Task<MedicineDTO> UpdateMedicine(int idMedicine, MedicineDTO medicine);
        Task<MedicineDTO> AddMedicine(MedicineDTO medicine);
    }
}
