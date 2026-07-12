using Matrimony.Infrastructure.Realtime.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Realtime.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private readonly UserConnectionManager _connectionManager;

        public NotificationHub(
            UserConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;

            if (Guid.TryParse(userId, out var id))
            {
                _connectionManager.AddConnection(
                    id,
                    Context.ConnectionId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(
            Exception? exception)
        {
            var userId = Context.UserIdentifier;

            if (Guid.TryParse(userId, out var id))
            {
                _connectionManager.RemoveConnection(
                    id,
                    Context.ConnectionId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
