using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace serwerHTTPforms
{
    class MyServer
    {
        int portNumber;
        TcpListener server = null;
        IPAddress localAddress = IPAddress.Parse("127.0.0.1");
        Byte[] bytes = new Byte[512];

        String siteSource = null;
        String directoryFiles = null;

        Thread workingThread;

        String data = null;
        string request_sub = null;
        String header = null;
        
        public String log { get; set; }

        public MyServer(TextBox port_n, TextBox file_p)
        {
            log = null;
            portNumber = int.Parse(port_n.Text);
            directoryFiles = file_p.Text.ToString();

            server = new TcpListener(localAddress, portNumber);
            server.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }

        public void startOnThread()
        {
            workingThread = new Thread(new ThreadStart(startServer));
            workingThread.Start();
        }

        public void startServer()
        {
            startListening();
        }

        public void startListening()
        {
            server.Start();

            try
            {
                while (true)
                {
                    if(!server.Pending())
                    {
                        //Console.WriteLine("Waiting for a connection... \n\n");
                    }
                    else
                    {
                 
                    TcpClient client = server.AcceptTcpClient();
                    
                    Console.WriteLine("Connected!");
                    NetworkStream stream = client.GetStream();

                    stream.Read(bytes, 0, bytes.Length);

                    data = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                    siteSource = loadSite(directoryFiles);

                    byte[] site = Encoding.UTF8.GetBytes(siteSource);

                    stream.Write(site, 0, site.Length);
                    Console.WriteLine("Sent:\n {0}", ASCIIEncoding.UTF8.GetString(site));
                    
                    Console.WriteLine("Disconnected!\n\n");
                    client.Close();
                }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch(Exception ex)
            {
                Console.WriteLine("\n!!!!!!!!!!!!!!\nError: {0}\n!!!!!!!!!!!!!!!!!\n",ex);
            }
        }

        public void stop_server()
        {
            server.Stop();
            workingThread.Abort();
            workingThread.Join();
            
        }

        private string loadSite(String filesDirectory)
        {
            int start_index = data.IndexOf("GET");
            int end_index = data.IndexOf("HTTP");

            Console.WriteLine("\n\nStart index: {0}\n\n",start_index);
            Console.WriteLine("\n\nEnd index: {0}\n\n", end_index);

            request_sub = data.Substring(start_index,end_index-start_index);

            Console.WriteLine("\n\n"+request_sub+"\n\n");

            String filePath = Path.GetFullPath(filesDirectory);
            
            /*headers*/
            String header_ok = @"HTTP/1.1 200 OK" + "\n" + @"Content-Type: text/html; charset=ISO-8859-2" + "\n\n";
            String header_404 = @"HTTP/1.1 404 Not Found" + "\n" + @"Content-Type: text/html; charset=ISO-8859-2" + "\n\n";
            String header_500 = @"HTTP/1.1 500 Internal server error" + "\n" + @"Content-Type: text/html; charset=ISO-8859-2" + "\n\n";

            /*bodies*/
            String body_404 = @"<h1>404 Not Found</h1><p>The URL you requested was not found.</p>";
            String body_500 = @"<h1>Internal server error (500)</h1><p>The server encountered an internal error or misconfiguration and was unable to complete your request.</p>";

            StringBuilder sb_siteSource = new StringBuilder();

            if (request_sub.EndsWith("file1.html "))
            {
                try
                {
                    sb_siteSource.Append(header_ok);
                    sb_siteSource.Append(siteSource = File.ReadAllText(@filePath + @"\file1.html"));
                }
                catch (FileNotFoundException)
                {
                    sb_siteSource.Append(header_404);
                    sb_siteSource.Append(body_404);
                }
                catch(DirectoryNotFoundException)
                {
                    sb_siteSource.Append(header_404);
                    sb_siteSource.Append(body_404);
                }
                catch (Exception)
                {
                    sb_siteSource.Append(header_500);
                    sb_siteSource.Append(body_500);
                }
            }
            else if (request_sub.EndsWith("file2.html "))
            {
                try
                {
                    sb_siteSource.Append(header_ok);
                    sb_siteSource.Append(siteSource = File.ReadAllText(@filePath + @"\file2.html"));
                }
                catch (FileNotFoundException)
                {
                    sb_siteSource.Append(header_404);
                    sb_siteSource.Append(body_404);
                }
                catch (DirectoryNotFoundException)
                {
                    sb_siteSource.Append(header_404);
                    sb_siteSource.Append(body_404);
                }
                catch (Exception)
                {
                    sb_siteSource.Append(header_500);
                    sb_siteSource.Append(body_500);
                }
            }
            else if (request_sub.EndsWith("file3.html "))
            {
                try
                {
                    sb_siteSource.Append(header_ok);
                    sb_siteSource.Append(siteSource = File.ReadAllText(@filePath + @"\file3.html"));
                }
                catch (FileNotFoundException)
                {
                    sb_siteSource.Append(header_404);
                    sb_siteSource.Append(body_404);
                }
                catch (DirectoryNotFoundException)
                {
                    sb_siteSource.Append(header_404);
                    sb_siteSource.Append(body_404);
                }
                catch (Exception)
                {
                    sb_siteSource.Append(header_500);
                    sb_siteSource.Append(body_500);
                }
            }
            else if (request_sub.EndsWith("file4.html "))
            {
                try
                {
                    throw new System.ArgumentException("Internal server error", "500");
                    //sb_siteSource.Append(header_ok);
                    //sb_siteSource.Append(siteSource = File.ReadAllText(@filePath + @"\file4.html"));
                }
                catch (FileNotFoundException)
                {
                    sb_siteSource.Append(header_404);
                    sb_siteSource.Append(body_404);
                }
                catch (DirectoryNotFoundException)
                {
                    sb_siteSource.Append(header_404);
                    sb_siteSource.Append(body_404);
                }
                catch (Exception)
                {
                    sb_siteSource.Append(header_500);
                    sb_siteSource.Append(body_500);
                }
            }
            else if(request_sub.EndsWith("/ "))
            {
                try
                {
                    sb_siteSource.Append(header_ok);
                    sb_siteSource.Append(siteSource = File.ReadAllText(@filePath + @"\index.html"));
                }
                catch (FileNotFoundException)
                {
                    sb_siteSource.Append(header_404);
                    sb_siteSource.Append(body_404);
                }
                catch (DirectoryNotFoundException)
                {
                    sb_siteSource.Append(header_404);
                    sb_siteSource.Append(body_404);
                }
                catch (Exception)
                {
                    sb_siteSource.Append(header_500);
                    sb_siteSource.Append(body_500);
                }
            }
            else
            {
                sb_siteSource.Append(header_404);
                sb_siteSource.Append(body_404);
            }
            return sb_siteSource.ToString();
        }
    }
}
