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

                Task<string> response = SendRequest(client);

                client.Close();

                connectionSuccessful = true;
            }
            catch(Exception e)
            {
                Console.Out.WriteLine("Connection aint succesfull because: " + e.Message);
                connectionSuccessful = false;
            }


            return;
        }

        private async Task<string> SendRequest(TcpClient client)
        {
            try
            {
                NetworkStream networkStream = client.GetStream();
                StreamWriter writer = new StreamWriter(networkStream);
                writer.AutoFlush = true;

                string newData = "Kakas";

                await writer.WriteLineAsync(newData);

                client.Close();
            }
            catch (Exception c)
            {
                Console.Out.WriteLine(c.Message);
            }

            return "";
        }
    }
}
