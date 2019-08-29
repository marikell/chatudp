using p01_chatudp.Models;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace p01_chatudp
{
    /// <summary>
    /// 
    /// </summary>
    public class SocketUdp
    {
        private readonly Socket _socket;
        private readonly State _state;
        private EndPoint _endpoint;
        private AsyncCallback _receiver;
        public SocketUdp()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _state = new State(1024 * 8);
            _endpoint = new IPEndPoint(IPAddress.Any, 0);
            _receiver = null;
        }

        /// <summary>
        /// Método que inicia o server para a conexão de outros clients.
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        public void Start(string ip, int port, string username)
        {
            IPEndPoint ipEndpoint = new IPEndPoint(IPAddress.Parse(ip), port);
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(ipEndpoint);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Receive()
        {
            _socket.BeginReceiveFrom(_state.GetBuffer(), 0, _state.BufferSize, SocketFlags.None, ref _endpoint, _receiver = (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _socket.EndReceiveFrom(ar, ref _endpoint);

                _socket.BeginReceiveFrom(so.GetBuffer(), 0, so.BufferSize, SocketFlags.None, ref _endpoint, _receiver, so);

                Console.WriteLine(string.Format("Mensagem recebida: {0}: {1}, {2}", _endpoint.ToString(), bytes, Encoding.ASCII.GetString(so.GetBuffer(), 0, bytes)));
            }, _state);
        }

        public void Connect(string ip, int port, string username)
        {

        }
    }
}
