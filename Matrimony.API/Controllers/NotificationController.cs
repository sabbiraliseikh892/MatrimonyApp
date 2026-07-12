using Matrimony.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matrimony.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private readonly INotificationService _notificationService;
        private readonly ICurrentUserService _currentUserService;
        public NotificationController(
            INotificationService notificationService,
            ICurrentUserService currentUserService)
        {
            _notificationService = notificationService;
            _currentUserService = currentUserService;
        }
        //-------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var userId =
                _currentUserService.GetUserId(User);

            var result =
                await _notificationService
                    .GetNotificationsAsync(userId);

            return Success(
                result,
                "Notifications retrieved successfully.");
        }

        //-------------------------------------------------------
        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
            var userId =
                _currentUserService.GetUserId(User);

            var count =
                await _notificationService
                    .GetUnreadCountAsync(userId);

            return Success(
                count,
                "Unread count retrieved.");
        }

        //-------------------------------------------------------

        [HttpPut("{notificationId:guid}/read")]
        public async Task<IActionResult> MarkAsRead(
            Guid notificationId)
        {
            var userId =
                _currentUserService.GetUserId(User);

            await _notificationService.MarkAsReadAsync(
                notificationId,
                userId);

            return Success(
                true,
                "Notification marked as read.");
        }
    }
}
