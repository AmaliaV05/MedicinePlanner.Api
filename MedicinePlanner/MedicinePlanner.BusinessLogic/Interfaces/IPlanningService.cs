using MedicinePlanner.BusinessLogic.DTOs;
using System.Threading.Tasks;

namespace MedicinePlanner.BusinessLogic.Interfaces
{
    public interface IPlanningService
    {
        Task<PlanningMlDTO> GetPlanning(int idPlanning);
        Task<PlanningDTO> ApproveNewPlanning(int idMedicine);
    }
}
