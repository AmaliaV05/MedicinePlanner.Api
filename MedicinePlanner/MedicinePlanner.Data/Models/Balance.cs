﻿namespace MedicinePlanner.Data.Models
{
    public abstract class Balance
    {
        public int Id { get; set; }
        public int PillsNumber { get; set; }
        public Stock Stock { get; set; }
    }
}
