using MedicinePlanner.Data.Models.Enum.Utils;
using System;

namespace MedicinePlanner.Data.Models
{
    public class DailyPlanning
    {
        public int Id { get; set; }
        public DateTime IntakeTime { get; set; }
        public double Dosage { get; set; }
        public bool Consumed { get; set; }
        public PlanningMessage Message { get; set; }
        public Planning Planning { get; set; }
    }
}
