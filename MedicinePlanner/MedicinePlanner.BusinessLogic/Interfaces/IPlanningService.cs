﻿using MedicinePlanner.BusinessLogic.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicinePlanner.BusinessLogic.Interfaces
{
    public interface IPlanningService
    {
        Task<PlanningDTO> GetPlanning(int idMedicine);
        Task<IEnumerable<PlanningDTO>> GetPlannings();
        Task<PlanningDTO> AddPlanning(int idMedicine, PlanningDTO planningDTO);
        Task<PlanningDTO> ApproveNewPlanning(int idMedicine);
        Task<IEnumerable<DailyPlanningDTO>> AddDailyPlanning(int idPlanning, IEnumerable<DailyPlanningDTO> dailyPlanningDTO);
    }
}
