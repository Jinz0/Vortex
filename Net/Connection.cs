using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Vortex.Net
{
   public class Connection
    {
        private int id { get; set; }
        private Socket ClientSocket;
        private AsyncCallback DataReceivedCallback;
        private AsyncCallback DataSentCallback;
        private byte[] DataBuffer = new byte[1024];

        public Connection(Socket socket, int id)
        {
            this.ClientSocket = socket;
            this.id = id;
            DataReceivedCallback = new AsyncCallback(DataReceived);
            StartDataReceive();
        }

        public void StartDataReceive()
        {
            if (this.ClientSocket.Connected)
            {
                this.ClientSocket.BeginReceive(this.DataBuffer, 0, this.DataBuffer.Length, SocketFlags.None, DataReceivedCallback, null);
               // Console.WriteLine(Encoding.ASCII.GetChars(DataBuffer));
            }
            else
            {
                
                Console.WriteLine("Client {0} is not connected!", this.id);
            }
        }

        private void DataReceived(IAsyncResult IAr)
        {
            int DataLen = this.ClientSocket.EndReceive(IAr);

            if (DataLen <= 0)
            {
                throw new ArgumentOutOfRangeException("Data received cannot be of a negative length!");
            }
            else
            {
                //ClientMessage message = new ClientMessage(DataBuffer,2bytes,DataLen);
                //DataRouter.Route(message);
            }
            Wait();

            //Handle data here somehow.
        }

        private void Wait()
        {
            this.ClientSocket.BeginReceive(this.DataBuffer, 0, this.DataBuffer.Length, SocketFlags.None, DataReceivedCallback, null);
            
        }
    }
}
