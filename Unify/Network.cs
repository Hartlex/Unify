using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Managers;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Tools;
using Packets;
using ServerNS;
using Unify.DataStructures;

namespace Unify
{
    public static class Network
    {
        private static ConnectionManager? _connectionManager;
        private static ServerManager? _serverManager;
        public static bool Initialize(List<ServerDefinition> serverDefinitions,List<PacketDefinition> packetDefinitions)
        {
            _connectionManager = new ConnectionManager();
            _serverManager = new ServerManager();

            InitPacketDefinitions(packetDefinitions);
            InitServers(serverDefinitions);
            NetworkComms.AppendGlobalConnectionEstablishHandler(_connectionManager.OnConnect);
            
            return true;
        }

        private static void InitServers(List<ServerDefinition> definitions)
        {
            foreach (var serverDefinition in definitions)
            {
                var server = new Server(serverDefinition);
                _serverManager?.AddServer(server);
            }
        }
        private static void InitPacketDefinitions(List<PacketDefinition> definitions)
        {
            foreach (var packetDefinition in definitions)
            {
                PacketParser.Instance.AddPacket(packetDefinition);
            }
        }
        
    }
}
