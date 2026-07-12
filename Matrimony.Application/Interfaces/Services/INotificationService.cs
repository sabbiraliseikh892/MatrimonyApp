using Matrimony.Application.Features.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendAsync(
            Guid userId,
            NotificationResponse notification);

        Task<List<NotificationListResponse>> GetNotificationsAsync(
            Guid userId);

        Task<int> GetUnreadCountAsync(
            Guid userId);

        Task MarkAsReadAsync(
            Guid notificationId,
            Guid userId);
    }
}
