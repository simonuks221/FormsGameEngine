using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace FormsGameEngine
{
    public class TpcGameServer
    {
        private int portNumber;

        public TpcGameServer(int _portNumber)
        {
            portNumber = _portNumber;

            ManageServer();
        }

        void ManageServer()
        {

            AsyncService service = new AsyncService(portNumber);
            service.Run();

            #region Stuff
            /*
            bool done = false;
            TcpListener listener = new TcpListener(IPAddress.Any, portNumber);

            listener.Start();
            while (!done)
            {
                TcpClient client = listener.AcceptTcpClient();

                NetworkStream ns = client.GetStream();

                byte[] byteTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString());

                ns.Write(byteTime, 0, byteTime.Length);
                ns.Close();

                while (client.Connected) //Read client messages if there is any
                {
                    byte[] msg = new byte[1024];
                    ns.Read(msg, 0, msg.Length);
                    Console.WriteLine(Encoding.Default.GetString(msg).Trim());
                }
                client.Close();

                //done = true;
            }


            listener.Stop();

    */
            #endregion
        }
    }

    public class AsyncService
    {
        private int portNumber;
        public AsyncService(int port)
        {
            portNumber = port;
        }
        public async Task Run()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, portNumber);
            listener.Start();

            while (true)
            {
                try
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    Task t = Process(client);
                    await t;
                }
                catch(Exception c)
                {
                    Console.Out.WriteLine(c.Message);
                }
            }
        }

        private async Task Process(TcpClient tcpClient)
        {
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                StreamReader reader = new StreamReader(networkStream);

                while (true)
                {
                    string request = await reader.ReadLineAsync();
                    if (!string.IsNullOrEmpty(request))
                    {
                        Console.Out.WriteLine("Request received: " + request);
                    }
                    else
                    {
                        break; //Client closed connection
                    }
                }
                tcpClient.Close();
            }
            catch(Exception e)
            {
                Console.Out.WriteLine(e.Message);
                if (tcpClient.Connected)
                {
                    tcpClient.Close();
                }
            }

        }
        //private static string Response(string request) { };
        //private static double Average(double[] vals) {}
        //private static double Minimum(double[] vals) { }
    }
}
