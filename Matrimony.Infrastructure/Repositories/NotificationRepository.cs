using Matrimony.Application.Interfaces.Repositories;
using Matrimony.Domain.Entities;
using Matrimony.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Repositories
{
    public class NotificationRepository
        : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        //------------------------------------------------------
        // Add
        //------------------------------------------------------

        public async Task AddAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
        }

        //------------------------------------------------------
        // Get By Id
        //------------------------------------------------------

        public async Task<Notification?> GetByIdAsync(
            Guid notificationId)
        {
            return await _context.Notifications
                .FirstOrDefaultAsync(x =>
                    x.Id == notificationId &&
                    !x.IsDeleted);
        }

        //------------------------------------------------------
        // Get User Notifications
        //------------------------------------------------------

        public async Task<List<Notification>> GetByUserIdAsync(
            Guid userId)
        {
            return await _context.Notifications
                .AsNoTracking()
                .Where(x =>
                    x.UserId == userId &&
                    !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        //------------------------------------------------------
        // Unread Count
        //------------------------------------------------------

        public async Task<int> GetUnreadCountAsync(
            Guid userId)
        {
            return await _context.Notifications
                .CountAsync(x =>
                    x.UserId == userId &&
                    !x.IsDeleted &&
                    !x.IsRead);
        }

        //------------------------------------------------------
        // Update
        //------------------------------------------------------

        public void Update(Notification notification)
        {
            _context.Notifications.Update(notification);
        }

        //------------------------------------------------------
        // Save
        //------------------------------------------------------

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
