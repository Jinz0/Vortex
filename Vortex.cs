using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vortex.Net;
using System.Net;

namespace Vortex
{
    class Vortex
    {
        public static void Main(string[] args)
        {
            
            ListenerFactory factory = new ListenerFactory(90, IPAddress.Any, 5);
            
            Console.ReadLine();
        }
    }
}
