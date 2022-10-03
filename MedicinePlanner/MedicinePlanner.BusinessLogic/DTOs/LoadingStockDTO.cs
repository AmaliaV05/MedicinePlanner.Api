using System;

namespace MedicinePlanner.BusinessLogic.DTOs
{
    public class LoadingStockDTO
    {
        public int Id { get; set; }
        public double PillsNumber { get; set; }
        public DateTime LoadingDate { get; set; }
        public StockOperationDTO Stock { get; set; }
    }
}
