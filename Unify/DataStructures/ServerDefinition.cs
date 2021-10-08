using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unify.DataStructures
{
    public class ServerDefinition
    {
        public string Name { get; }
        public int Port { get; }

        public ServerDefinition(string name, int port)
        {
            Name = name;
            Port = port;
        }


    }
}
