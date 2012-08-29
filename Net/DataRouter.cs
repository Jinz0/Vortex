using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;



namespace Vortex.Net
{

    /// <summary>
    /// Routes data over a network of clients
    /// </summary>
    /// <remarks>
    /// Listener Socket
    ///       |
    ///       |
    ///       |
    ///     Router ----
    ///                 | Connection1
    ///                 | Connection2
    ///                 | Connection3
    /// </remarks>
    class DataRouter
    {

        private static readonly int MAXIMUM_BYTES = 1024;

        /// <summary>
        /// Allows us to invoke a client and handle data accordingly.
        /// </summary>
        /// <param name="Data">The data to route.</param>
        public void Route(byte[] Data)
        {
            //Here, we handle the users data.

            if (Data.Length >= MAXIMUM_BYTES)
            {
                throw new ArgumentOutOfRangeException("Data received is too great a length!");


            }
            else
            {
                //this.Invoke(Data);
            }
        }

        
    }
}
