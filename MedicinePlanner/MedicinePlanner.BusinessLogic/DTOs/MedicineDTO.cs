using MedicinePlanner.Data.Models.Enum.Utils;
using System;

namespace MedicinePlanner.BusinessLogic.DTOs
{
    public class MedicineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MedicineType Type { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public StockDTO Stock { get; set; }
    }
}
