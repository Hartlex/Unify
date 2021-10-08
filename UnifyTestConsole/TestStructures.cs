using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unify.DataStructures;
using Unify.Helpers;

namespace UnifyTestConsole
{
    public class BasicInfo : PacketInfo
    {
        public string Name { get; set; } = null!;
        public float Health { get; set; }
        public float Mana { get; set; }
        public BasicInfo() { }
        public BasicInfo(string name, float health, float mana)
        {
            Name = name;
            Health = health;
            Mana = mana;
        }
        
    }

    public class BasicInfo2 :PacketInfo
    {
        public string Name { get; set; } = null!;
        public float Health { get; set; }
        public float Mana { get; set; }

        public BasicInfo2(ref ByteBuffer buffer)
        {
            Name = buffer.ReadString();
            Health = buffer.ReadFloat();
            Mana = buffer.ReadFloat();
        }
        public BasicInfo2() { }
        public BasicInfo2(string name, float health, float mana)
        {
            Name = name;
            Health = health;
            Mana = mana;
        }

        public override void Deserialize<T>(ref ByteBuffer buffer)
        {
            Name = buffer.ReadString();
            Health = buffer.ReadFloat();
            Mana = buffer.ReadFloat();
        }

        public override void Serialize<T>(ref ByteBuffer buffer)
        {
            buffer.WriteString(Name);
            buffer.WriteFloat(Health);
            buffer.WriteFloat(Mana);
        }
    }
}
