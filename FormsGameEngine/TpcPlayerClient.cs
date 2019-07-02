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
        private const int portNumber = 9999;
        private const string hostName = "localhost";

        public TpcPlayerClient()
        {
            TcpClient client = new TcpClient(hostName, portNumber);

            NetworkStream ns = client.GetStream();

            byte[] bytes = new byte[1024];
            int bytesRead = ns.Read(bytes, 0, bytes.Length);

            Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRead));

            client.Close();
        }
    }
}
