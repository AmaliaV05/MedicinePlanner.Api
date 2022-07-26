using System;
using System.Collections.Generic;

namespace MedicinePlanner.BusinessLogic.DTOs
{
    public class PlanningDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? PauseEndDate { get; set; }
        public IEnumerable<DailyPlanningDTO> DailyPlannings { get; set; }
    }
}
