using System;
using System.Collections.Generic;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Tools;

namespace Managers
{
    // ReSharper disable once HollowTypeName
    internal class ConnectionManager
    {
        private Dictionary<ShortGuid, Connection> _activeConnections;

        public ConnectionManager()
        {
            _activeConnections = new Dictionary<ShortGuid, Connection>();
        }

        public void OnConnect(Connection connection)
        {
            var guid = connection.ConnectionInfo.NetworkIdentifier;
            if (_activeConnections.ContainsKey(guid))
            {
                Console.Out.WriteLine("Duplicate NetworkIdentifier: "+ guid);
                Console.Out.WriteLine("Replacing old Connection");
                _activeConnections.Remove(guid);
            }
            else
            {
                guid = ShortGuid.NewGuid();
                connection.ConnectionInfo.ResetNetworkIdentifer(guid);
            }

            _activeConnections.Add(guid,connection);
        }
    }
}
