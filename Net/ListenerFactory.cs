#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

#endregion


namespace Vortex.Net
{

    /// <summary>
    /// The main server listener for Vortex.Net
    /// </summary>

  public class ListenerFactory
    {
        #region Fields & Properties
        /// <summary>
        /// The main listener instance, held in a socket.
        /// </summary>
        private Socket ListenerSocket;

        /// <summary>
        /// The callback handler for a new connection
        /// </summary>
        private AsyncCallback OnConnectionCallback;

        /// <summary>
        /// Holds the IP address for the socket.
        /// </summary>
        private IPAddress IpAddress;

        /// <summary>
        /// Port for listening.
        /// </summary>
        private int Port { get; set; }

        /// <summary>
        /// Boolean to determine whether the factory is listening.
        /// </summary>
        private bool IsListening;


        /// <summary>
        /// The connection queue as an integer.
        /// </summary>
        private int QueuedConnections;
        #endregion


        /// <summary>
        /// Construct a new listener factory object
        /// </summary>
        /// <param name="port">The port for the server to listen on</param>
        /// <param name="ipAddress">The ip address</param>
        /// <param name="QueuedConnections">The backlog for the listening instance</param>

        #region Constructor
        public ListenerFactory(int port, IPAddress ipAddress, int QueuedConnections)
        {
            this.QueuedConnections = QueuedConnections;
            this.Port = port;
            this.IpAddress = ipAddress;


            this.ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Console.Read();
            this.BeginListening(QueuedConnections);
        }

        #endregion

        #region Private Methods

        private void BeginListening(int QueuedConnections)
        {
            this.OnConnectionCallback = new AsyncCallback(OnConnectionReceived);
            
            IPEndPoint endpoint = new IPEndPoint(this.IpAddress,this.Port);
            this.ListenerSocket.Bind(endpoint);
            
            this.IsListening = true;
            this.Listen();
            

            Console.WriteLine("Initialised Listener Factory [port:{0}, ip:{1}]", this.Port, this.IpAddress);
            StartAccepting();
            
        }

        private void OnConnectionReceived(IAsyncResult IAr)
        {
           
                int i = 0;
                Socket UserSocket = this.ListenerSocket.EndAccept(IAr);
                Console.WriteLine("Accepted client {0} with IP of {1}", i, UserSocket.RemoteEndPoint);
                Connection Client = new Connection(UserSocket, i++);
                
            
        }


        private void Listen()
        {
           
                this.ListenerSocket.Listen(this.QueuedConnections);
            
        }

        private void StartAccepting()
        {
                this.ListenerSocket.BeginAccept(OnConnectionCallback, null);
            
        }

        private void EndSocketAccept()
        {
            Console.WriteLine("Shutting down socket");
            if (IsListening)
            {
                
                //this.ListenerSocket.Shutdown(SocketShutdown.Both);
                //added safety?
                this.ListenerSocket = null;
                Console.Read();
                Environment.Exit(1);
            }
            else
            {
                Console.WriteLine("You cannot terminate a socket that has not been initialized!");
            }
        }
        #endregion
    }
}
