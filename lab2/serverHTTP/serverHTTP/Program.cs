using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace serwerHTTPforms
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.startListening();
        }
    }
}
