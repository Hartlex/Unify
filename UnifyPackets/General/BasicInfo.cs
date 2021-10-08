using Unify.DataStructures;
using Unify.Helpers;

namespace UnifyPackets.General
{
    public class BasicInfo : PacketInfo
    {
        public readonly int x1;
		public readonly int x2;
		public readonly float health;
		

        public BasicInfo(ref ByteBuffer buffer)
        {
            x1 = buffer.ReadInt32();
			x2 = buffer.ReadInt32();
			health = buffer.ReadFloat();
			
        }
        public void GetBytes(ref ByteBuffer buffer)
        {
            buffer.WriteInt32(x1);
			buffer.WriteInt32(x2);
			buffer.WriteFloat(health);
			
        }

    }
}