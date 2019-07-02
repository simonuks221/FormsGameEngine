using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;

namespace FormsGameEngine
{
    public class TpcGameServer
    {
        private const int portNumber = 9999;

        public TpcGameServer()
        {
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

                done = true;
            }

            listener.Stop();
        }
    }
}
