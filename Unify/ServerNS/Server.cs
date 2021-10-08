using System;
using System.Net;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Connections.TCP;
using NetworkCommsDotNet.DPSBase;
using Packets;
using Unify.DataStructures;


namespace ServerNS
{
    internal class Server
    {
        private readonly TCPConnectionListener _listener;
        internal string Name { get; }
        internal Server(ServerDefinition definition)
        {
            Name = definition.Name;
            SendReceiveOptions optionsToUse = new SendReceiveOptions<NullSerializer>();
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),definition.Port);
            _listener = new TCPConnectionListener(optionsToUse, ApplicationLayerProtocolStatus.Disabled);
            _listener.AppendIncomingUnmanagedPacketHandler((header, connection, array) =>
            {
                PacketParser.Instance.ParsePacket(connection,array);
            });
            Connection.StartListening(_listener, iPEndPoint);
            Console.Out.WriteLine("Started server: "+Name+ " at port: "+definition.Port);
        }

        ~Server()
        {
            Connection.StopListening(_listener);
        }
    }
}
