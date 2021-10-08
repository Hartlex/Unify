using System.Collections.Generic;
using Unify;
using Unify.DataStructures;
using Unify.Helpers;

namespace UnifyTestConsole
{
    internal class UnifyServer : Singleton<UnifyServer>
    {
        private List<ServerDefinition> _serverDefinitions = new List<ServerDefinition >();
        private List<PacketDefinition> _packetDefinitions = new List<PacketDefinition>();
        public void Define()
        {
            _serverDefinitions = new List<ServerDefinition>()
            {
                //Insert more Servers here if you want
                new ServerDefinition("Test 1", 50550)
            };

            _packetDefinitions = new List<PacketDefinition>()
            {
                //Inser more PacketDefinitions here!
                new PacketDefinition(0, 0, Actions.Action1),
                new PacketDefinition(0, 1, Actions.Action2)
            };
        }

        public void Start()
        {
            Network.Initialize(_serverDefinitions, _packetDefinitions);
            
        }

        public void Stop()
        {

        }
    }
}
