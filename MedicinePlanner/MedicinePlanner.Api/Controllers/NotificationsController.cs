using MedicinePlanner.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MedicinePlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController: ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            return Ok(await _notificationService.GetNotifications());
        }
    }
}
