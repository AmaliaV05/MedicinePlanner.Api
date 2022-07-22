using System;

namespace MedicinePlanner.BusinessLogic.DTOs
{
    public class LoadingStockDTO
    {
        public int Id { get; set; }
        public int PillsNumber { get; set; }
        public DateTime LoadingDate { get; set; }
    }
}
