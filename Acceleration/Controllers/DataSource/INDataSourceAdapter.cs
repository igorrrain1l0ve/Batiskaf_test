using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

using Batiskaf.Models.State.Angular;
using Batiskaf.Models.State.Linear;

namespace Batiskaf.Controllers.DataSource
{
    delegate void addNewValue(INLinearAcceleration acceleration);
    class INDataSourceAdapter : INDataSourceInterface
    {
        public bool isListen;

        private int port = 5001;
        private INLinearAcceleration _linearAcceleration;
        private addNewValue add;

        //Constructor
        public INDataSourceAdapter(int port, addNewValue add)
        {
            this.port = port;
            this.isListen = true;
            this.add = add;


            Thread listenThread = new Thread(new ThreadStart(this.Resiever));
            listenThread.Start();
        }


        //Setters and Getters
        public INLinearAcceleration linearAcceleration
        {
            get
            {
                if (_linearAcceleration != null)
                {
                    _linearAcceleration = new INLinearAcceleration(0, 0, 0);
                }
                return _linearAcceleration;
            }
        }

        //INDataSourceInterface implementation
        public INAngularVelocity getAngularVelocity()
        {
            INAngularVelocity angularVelocity = new INAngularVelocity();
            return angularVelocity;
        }

        public INLinearAcceleration getLinearAcceleration()
        {
            return _linearAcceleration;
        }

        //UDPClient
        private void Resiever()
        {
            try
            {
                UdpClient client = new UdpClient(port);
                IPEndPoint ipEndPoint = null;

                while (this.isListen)
                {
                    byte[] tmp = client.Receive(ref ipEndPoint);
                    _linearAcceleration = toLinearAcceleration(tmp);

                    this.add(_linearAcceleration);
                }

                client.Close();
            }
            catch(Exception exeption)
            {
                Console.WriteLine("Resiever has some troubles...\n" + exeption.ToString());
            }
        }

        //Converter
        private INLinearAcceleration toLinearAcceleration(byte[] bytes)
        {
            int size = sizeof(double);
            double aX = BitConverter.ToDouble(bytes, 0);
            double aY = BitConverter.ToDouble(bytes, size);
            double aZ = BitConverter.ToDouble(bytes, 2 * size);
            INLinearAcceleration acceleration = new INLinearAcceleration(aX, aY, aZ);

            return acceleration;
        }
    }
}
