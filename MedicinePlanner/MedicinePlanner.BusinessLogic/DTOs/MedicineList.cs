using System;
using System.Collections.Generic;

namespace MedicinePlanner.BusinessLogic.DTOs
{
    public class PlanningMlDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PauseEndDate { get; set; }
        public MedicineMlDTO Medicine { get; set; }
        public IEnumerable<DailyPlanningDTO> DailyPlannings { get; set; }
    }

    public class MedicineMlDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public StockDTO Stock { get; set; }
    }
}
