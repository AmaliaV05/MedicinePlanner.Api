using System.Collections.Generic;

namespace MedicinePlanner.Data.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int Total { get; set; }
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public List<LoadingStock> LoadingStocks { get; set; }
        public List<UnloadingStock> UnloadingStocks { get; set; }
    }
}
