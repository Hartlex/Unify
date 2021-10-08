using System;
using NetworkCommsDotNet.Connections;
using Unify.Helpers;

namespace Unify.DataStructures
{
    public class PacketDefinition
    {
        public int ID1 { get; }
        public int ID2 { get; }
        public Action<Connection, ByteBuffer> Action { get; }
        public PacketDefinition(int id1, int id2, Action<Connection, ByteBuffer> action)
        {
            ID1 = id1;
            ID2 = id2;
            Action = action;
        }
    }
}
