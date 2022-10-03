using System;

namespace MedicinePlanner.BusinessLogic.Exceptions
{
    public class MedicineNotFoundException : Exception
    {
        public MedicineNotFoundException() { }
        public MedicineNotFoundException(string message) : base(message) { }
        public MedicineNotFoundException(string model, string name) : base(string.Format("There is no {0} with name={1}", model, name)) { }
    }
}
