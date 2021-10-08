using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerNS;


namespace Managers
{
    // ReSharper disable once HollowTypeName
    internal class ServerManager
    {
        private readonly Dictionary<int, Server> _activeServers;

        public ServerManager()
        {
            _activeServers =new Dictionary<int, Server>();
        }

        public void AddServer(Server server)
        {
            var hash = server.Name.GetHashCode();
            if (_activeServers.ContainsKey(hash))
            {
                Console.Out.WriteLine("Duplicate Server: "+server.Name );
                Console.Out.WriteLine("Existing server will be overwritten!");
                _activeServers.Remove(hash);
            }
            _activeServers.Add(hash,server);
        }
    }
}
