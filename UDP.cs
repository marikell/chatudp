using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP
{
    public class UDPSocket
    {
        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private const int bufSize = 8 * 1024;
        private State state = new State();
        private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;
        private List<UDPSocket> KnowHosts = new List<UDPSocket>();
        private string name = "nameless";
        private string ipClient;

        public class State
        {
            public byte[] buffer = new byte[bufSize];
        }

        public UDPSocket(string name = "nameless")
        {
            this.name = name;
        }

        public void AddKnowHost(UDPSocket c)
        {   
            this.KnowHosts.Add(c);
        }

        public void SendHeartBeat()
        {
            foreach(var x in this.KnowHosts)
            {
                x.Send(String.Format("HEART BEAT FROM " + this.name));
            }
        }

        public void Server(int port)
        {
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(new IPEndPoint(IPAddress.Any, port));
            Receive();
        }

        public void Client(string address, int port)
        {
            this.ipClient = address;
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Connect(IPAddress.Parse(address), port);
            //Receive();
        }

        public void Send(string text)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);
            _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _socket.EndSend(ar);
                Console.WriteLine("Mensagem Enviada: {0} ", text);
            }, state);
        }

        public void SendResposeToIP(string ip)
        {

            string onlyIp = ip.Split(':')[0];

            foreach (var x in this.KnowHosts)
            { 

                if (x.ipClient == onlyIp)
                {
                    x.Send("Heart Beat OK!!!");
                }
            }
        }

        public void Receive()
        {
            _socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _socket.EndReceiveFrom(ar, ref epFrom);
                _socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);
                string msg = Encoding.ASCII.GetString(so.buffer, 0, bytes);
                Console.WriteLine(String.Format("Mensagem recebida de {0} : {1}", epFrom.ToString(), msg));

                if (msg.Contains("HEART BEAT FROM"))
                {
                    this.SendResposeToIP(epFrom.ToString());
                }

                
            }, state);
        }
    }
}