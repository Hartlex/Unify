using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unify.Helpers;

namespace Unify.DataStructures
{
    public abstract class PacketInfo
    {
        public virtual void Serialize<T>(ref ByteBuffer buffer)
        {
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                buffer.Write(Type.GetTypeCode(prop.PropertyType), prop.GetValue(this, null)!);
            }
        }
        public virtual void Deserialize<T>(ref ByteBuffer buffer)
        {
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                prop.SetValue(this,buffer.Read(Type.GetTypeCode(prop.PropertyType)),null);
            }

        }
    }
}
