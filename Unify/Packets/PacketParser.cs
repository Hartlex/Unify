using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;
using NetworkCommsDotNet.Connections;
using Unify.DataStructures;
using Unify.Helpers;

namespace Packets
{
    internal class PacketParser : Singleton<PacketParser>
    {
        private readonly Dictionary<int, Dictionary<int, Action<Connection, ByteBuffer>>> _actions =
            new Dictionary<int, Dictionary<int, Action<Connection, ByteBuffer>>>();
        internal void ParsePacket(Connection connection,byte[] bytes)
        {
            var buffer = new ByteBuffer(bytes);
            var id1 = buffer.ReadInt32();
            var id2 = buffer.ReadInt32();
            if (TryFindAction(id1, id2, out var action))
            {
                //var actionType = action.Method.GetParameters()[1].ParameterType;
                //PacketInfo packet = (PacketInfo) Activator.CreateInstance(actionType)!;
                action(connection, buffer);
            }

            Console.Out.WriteLine($"Unknown Packet :{id1}|{id2}");
        }

        internal void AddPacket(PacketDefinition packet)
        {
            var id1 = packet.ID1;
            var id2 = packet.ID2;
            var action = packet.Action;
            if (!_actions.ContainsKey(id1))
                _actions.Add(id1,new Dictionary<int, Action<Connection, ByteBuffer>>());

            if (_actions[id1].ContainsKey(id2))
                _actions[id1].Remove(id2);

            _actions[id1].Add(id2,action);
        }
        private bool TryFindAction(int ID1, int ID2, out Action<Connection, ByteBuffer> action)
        {
            action = null;
            if (!_actions.ContainsKey(ID1)) return false;
            if (!_actions[ID1].ContainsKey(ID2)) return false;
            action = _actions[ID1][ID2];
            return true;
        }
    }
}
