using System;

namespace MedicinePlanner.BusinessLogic.Exceptions
{
    public class IdNotFoundException : Exception
    {
        public IdNotFoundException() { }
        public IdNotFoundException(string message) : base(message) { }
        public IdNotFoundException(string model, int id) : base(string.Format("There is no {0} with id={1}", model, id.ToString())) { }
    }
}
