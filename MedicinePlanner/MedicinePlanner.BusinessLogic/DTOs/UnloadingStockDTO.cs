using System;

namespace MedicinePlanner.BusinessLogic.DTOs
{
    public class UnloadingStockDTO
    {
        public int Id { get; set; }
        public double PillsNumber { get; set; }
        public DateTime UnloadingDate { get; set; }
        public StockOperationDTO Stock { get; set; }
    }
}
