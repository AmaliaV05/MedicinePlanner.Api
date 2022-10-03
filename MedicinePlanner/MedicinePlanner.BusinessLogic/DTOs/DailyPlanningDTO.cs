using MedicinePlanner.Data.Models.Enum.Utils;
using System;

namespace MedicinePlanner.BusinessLogic.DTOs
{
    public class DailyPlanningDTO
    {
        public int Id { get; set; }
        public DateTime IntakeTime { get; set; }
        public double Dosage { get; set; }
        public bool Consumed { get; set; }
        public PlanningMessage Message { get; set; }
    }
}
