using System;

namespace MedicinePlanner.Data.Models
{
    public class UnloadingStock : Balance
    {
        public DateTime UnloadingDate { get; set; }
    }
}
