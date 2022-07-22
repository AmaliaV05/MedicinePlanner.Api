namespace MedicinePlanner.BusinessLogic.Services
{
    public class ServiceResponse<TResponseOk, TResponseError>
    {
        public TResponseOk ResponseOk { get; set; }
        public TResponseError ResponseError { get; set; }
    }
}
