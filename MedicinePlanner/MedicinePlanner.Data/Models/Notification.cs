using System;

namespace MedicinePlanner.Data.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Medicine Medicine { get; set; }
        public Planning Planning { get; set; }
    }
}
