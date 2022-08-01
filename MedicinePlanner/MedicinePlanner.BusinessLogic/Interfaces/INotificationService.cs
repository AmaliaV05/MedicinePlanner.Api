using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicinePlanner.BusinessLogic.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<string>> GetNotifications();
    }
}
