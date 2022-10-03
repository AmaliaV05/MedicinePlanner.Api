using System.Text.Json;

namespace MedicinePlanner.Data.Models.Error
{
    public class ModelError
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorStack { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
