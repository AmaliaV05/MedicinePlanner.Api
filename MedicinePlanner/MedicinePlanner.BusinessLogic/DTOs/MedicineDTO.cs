using System;
using System.Collections.Generic;

namespace MedicinePlanner.BusinessLogic.DTOs
{
    public class MedicineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public StockDTO Stock { get; set; }
        public IEnumerable<PlanningDTO> Plannings { get; set; }
    }
}
