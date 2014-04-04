using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace serwerHTTPforms
{
    class Server
    {
        TcpListener server = null;
        Int32 portNumber = 8010;
        IPAddress localAddress = IPAddress.Parse("127.0.0.1");

        Byte[] bytes = new Byte[1024];
        
        String siteSource = null;

        public Server()
        {
            server = new TcpListener(localAddress, portNumber);
            server.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }

        public void startListening()
        {
            server.Start();

            try
            {
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
       
                    NetworkStream stream = client.GetStream();

                    siteSource = randomSite(@"sites\");
                       
                    String header = @"HTTP/1.1200 OK" + "\n" + @"Content-Type: text/html; charset=ISO-8859-2" + "\n\n";

                    byte[] site = Encoding.UTF8.GetBytes(header + siteSource);

                    stream.Write(site, 0, site.Length);
                    Console.WriteLine("Sent:\n {0}", ASCIIEncoding.UTF8.GetString(site));
                 
                    Console.WriteLine("Disconnected!\n\n");
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }

        }

        private string randomSite(String filesDirectory)
        {
            String filePath = Path.GetFullPath(filesDirectory);
            int siteNumber = 0;
            System.Random rand = new Random();
            siteNumber = rand.Next(1, 5);

            if (siteNumber == 1)
            {
                try
                {
                    siteSource = File.ReadAllText(@filePath+"table2x2.html");
                }
                catch(FileNotFoundException)
                {
                    Console.WriteLine("\n\n"+filePath+"\n\n");
                    Console.WriteLine("\n\ntable2x2.html: File Not Found.\n\n");
                }
                catch(FileLoadException lex)
                {
                    Console.WriteLine("\n\nFile not load correctly.\n\n");
                    Console.WriteLine("Error: "+lex.ToString());
                }
            }
            else if (siteNumber == 2)
            {
                try
                {
                    siteSource = File.ReadAllText(@filePath + "table2x3.html");
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("\n\n" + filePath + "\n\n");
                    Console.WriteLine("\n\ntable2x3.html: File Not Found.\n\n");
                }
                catch (FileLoadException lex)
                {
                    Console.WriteLine("\n\nFile not load correctly.\n\n");
                    Console.WriteLine("Error: " + lex.ToString());
                }
            }
            else if (siteNumber == 3)
            {
                try
                {
                    siteSource = File.ReadAllText(@filePath + "table2x5.html");
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("\n\n" + filePath + "\n\n");
                    Console.WriteLine("\n\ntable2x5.html: File Not Found.\n\n");
                }
                catch (FileLoadException lex)
                {
                    Console.WriteLine("\n\nFile not load correctly.\n\n");
                    Console.WriteLine("Error: " + lex.ToString());
                }
            }
            else
            {
                try
                {
                    siteSource = File.ReadAllText(@filePath + "wynik.html");
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("\n\n" + filePath + "\n\n");
                    Console.WriteLine("\n\nwynik.html: File Not Found.\n\n");
                }
                catch (FileLoadException lex)
                {
                    Console.WriteLine("\n\nFile not load correctly.\n\n");
                    Console.WriteLine("Error: " + lex.ToString());
                }
            }
            return siteSource;
        }
    }
}
