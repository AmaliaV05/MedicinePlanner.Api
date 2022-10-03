namespace MedicinePlanner.BusinessLogic.DTOs
{
    public class StockOperationDTO
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public StockOperationMedicineDTO Medicine { get; set; }
    }
}
