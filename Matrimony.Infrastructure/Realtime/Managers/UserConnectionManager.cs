using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Realtime.Managers
{
    public class UserConnectionManager
    {
        private readonly ConcurrentDictionary<Guid, HashSet<string>>
           _connections = new();

        private readonly object _lock = new();

        //----------------------------------------------------
        // Add Connection
        //----------------------------------------------------

        public void AddConnection(
            Guid userId,
            string connectionId)
        {
            lock (_lock)
            {
                if (!_connections.ContainsKey(userId))
                {
                    _connections[userId] = new HashSet<string>();
                }

                _connections[userId].Add(connectionId);
            }
        }
        //----------------------------------------------------
        // Remove Connection
        //----------------------------------------------------

        public void RemoveConnection(
            Guid userId,
            string connectionId)
        {
            lock (_lock)
            {
                if (!_connections.TryGetValue(userId, out var connections))
                    return;

                connections.Remove(connectionId);

                if (connections.Count == 0)
                {
                    _connections.TryRemove(userId, out _);
                }
            }
        }

        //----------------------------------------------------
        // Get Connections
        //----------------------------------------------------

        public IReadOnlyCollection<string> GetConnections(Guid userId)
        {
            if (_connections.TryGetValue(userId, out var connections))
            {
                return connections;
            }

            return Array.Empty<string>();
        }
        //----------------------------------------------------
        // Is Online
        //----------------------------------------------------

        public bool IsOnline(Guid userId)
        {
            return _connections.ContainsKey(userId);
        }
    }
}
