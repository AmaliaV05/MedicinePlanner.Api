using MedicinePlanner.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicinePlanner.BusinessLogic.Interfaces
{
    public interface IMedicineService
    {
        Task<Medicine> GetMedicine(int id);
        Task<IEnumerable<Medicine>> GetMedicines();
        Task<Medicine> UpdateMedicine(int idMedicine, Medicine medicine);
        Task<Medicine> AddMedicine(Medicine medicine);
    }
}
