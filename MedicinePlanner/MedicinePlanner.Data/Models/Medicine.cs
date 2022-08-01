using MedicinePlanner.Data.Models.Enum.Utils;
using System;
using System.Collections.Generic;

namespace MedicinePlanner.Data.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MedicineType Type { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public Stock Stock { get; set; }
        public List<Planning> Plannings { get; set; }
    }
}
