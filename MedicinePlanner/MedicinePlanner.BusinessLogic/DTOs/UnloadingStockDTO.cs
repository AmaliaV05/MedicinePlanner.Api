using System;

namespace MedicinePlanner.BusinessLogic.DTOs
{
    public class UnloadingStockDTO
    {
        public int Id { get; set; }
        public int PillsNumber { get; set; }
        public DateTime UnloadingDate { get; set; }
    }
}
