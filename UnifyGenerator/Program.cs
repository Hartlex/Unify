using System;
using System.Collections.Generic;

namespace UnifyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Press enter to create a new PackageInfo"); 
            Console.ReadLine();
            var packetGenerator = new PacketInfoGenerator();
        }
    }
}
