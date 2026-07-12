using Matrimony.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        //------------------------------------------------------
        // Create
        //------------------------------------------------------

        Task AddAsync(Notification notification);

        //------------------------------------------------------
        // Get By Id
        //------------------------------------------------------

        Task<Notification?> GetByIdAsync(Guid notificationId);

        //------------------------------------------------------
        // Notification List
        //------------------------------------------------------

        Task<List<Notification>> GetByUserIdAsync(Guid userId);

        //------------------------------------------------------
        // Unread Count
        //------------------------------------------------------

        Task<int> GetUnreadCountAsync(Guid userId);

        //------------------------------------------------------
        // Update
        //------------------------------------------------------

        void Update(Notification notification);

        //------------------------------------------------------
        // Save
        //------------------------------------------------------

        Task SaveChangesAsync();
    }
}
