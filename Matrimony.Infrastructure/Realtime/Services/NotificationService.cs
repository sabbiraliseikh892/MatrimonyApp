using Matrimony.Application.Features.Notifications;
using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Application.Interfaces.Services;
using Matrimony.Domain.Entities;
using Matrimony.Infrastructure.Realtime.Hubs;
using Matrimony.Infrastructure.Realtime.Managers;
using Matrimony.Shared.Exceptions;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Realtime.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly UserConnectionManager _connectionManager;

        public NotificationService(
            INotificationRepository notificationRepository,
            IHubContext<NotificationHub> hubContext,
            UserConnectionManager connectionManager)
        {
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
            _connectionManager = connectionManager;
        }

        //-------------------------------------------------------
        // Send Notification
        //-------------------------------------------------------

        public async Task SendAsync(
            Guid userId,
            NotificationResponse notification)
        {
            var entity = new Notification
            {
                UserId = userId,
                Title = notification.Title,
                Message = notification.Message,
                Type = notification.Type,
                Data = notification.Data,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await _notificationRepository.AddAsync(entity);

            await _notificationRepository.SaveChangesAsync();

            notification.Id = entity.Id;
            notification.IsRead = false;

            var connections =
                _connectionManager.GetConnections(userId);

            foreach (var connectionId in connections)
            {
                await _hubContext.Clients
                    .Client(connectionId)
                    .SendAsync(
                        "ReceiveNotification",
                        notification);
            }
        }

        //-------------------------------------------------------
        // Notification History
        //-------------------------------------------------------

        public async Task<List<NotificationListResponse>>
            GetNotificationsAsync(Guid userId)
        {
            var notifications =
                await _notificationRepository
                    .GetByUserIdAsync(userId);

            return notifications.Select(x =>
                new NotificationListResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    Message = x.Message,
                    Type = x.Type,
                    IsRead = x.IsRead,
                    CreatedAt = x.CreatedAt
                }).ToList();
        }

        //-------------------------------------------------------
        // Unread Count
        //-------------------------------------------------------

        public async Task<int> GetUnreadCountAsync(
            Guid userId)
        {
            return await _notificationRepository
                .GetUnreadCountAsync(userId);
        }

        //-------------------------------------------------------
        // Mark As Read
        //-------------------------------------------------------

        public async Task MarkAsReadAsync(
            Guid notificationId,
            Guid userId)
        {
            var notification =
                await _notificationRepository
                    .GetByIdAsync(notificationId);

            if (notification == null)
                throw new NotFoundException("Notification not found.");

            if (notification.UserId != userId)
                throw new UnauthorizedException("Unauthorized.");

            notification.IsRead = true;
            notification.ReadAt = DateTime.UtcNow;

            _notificationRepository.Update(notification);

            await _notificationRepository.SaveChangesAsync();
        }
    }
}
