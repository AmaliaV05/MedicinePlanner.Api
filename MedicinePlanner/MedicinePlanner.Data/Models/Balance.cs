namespace MedicinePlanner.Data.Models
{
    public abstract class Balance
    {
        public int Id { get; set; }
        public double PillsNumber { get; set; }
        public Stock Stock { get; set; }
    }
}
