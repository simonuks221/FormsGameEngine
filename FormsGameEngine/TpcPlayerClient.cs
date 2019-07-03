using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;

namespace FormsGameEngine
{
    public class TpcPlayerClient
    {
        private int portNumber;
        private string hostName;

        public TpcPlayerClient(int _portNumber, string _hostName, out bool connectionSuccessful)
        {
            portNumber = _portNumber;
            hostName = _hostName;

            try
            {
                TcpClient client = new TcpClient(hostName, portNumber);

                NetworkStream ns = client.GetStream();

                byte[] bytes = new byte[1024];
                int bytesRead = ns.Read(bytes, 0, bytes.Length);

                Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRead));

                client.Close();

                connectionSuccessful = true;
            }
            catch(Exception e)
            {
                connectionSuccessful = false;
            }


            return;
        }
    }
}
