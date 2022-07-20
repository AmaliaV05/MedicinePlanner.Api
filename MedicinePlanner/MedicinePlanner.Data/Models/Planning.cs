using System;
using System.Collections.Generic;

namespace MedicinePlanner.Data.Models
{
    public class Planning
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? PauseEndDate { get; set; }
        public Medicine Medicine { get; set; }
        public List<DailyPlanning> DailyPlannings { get; set; }
    }
}
