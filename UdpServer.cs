/*
C# Network Programming 
by Richard Blum

Publisher: Sybex 
ISBN: 0782141765
*/
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public static class UdpServer
{
    public static void Run()
    {
        int recv;
        byte[] data = new byte[1024];
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 10009);

        Socket newsock = new Socket(AddressFamily.InterNetwork,
                        SocketType.Dgram, ProtocolType.Udp);

        newsock.Bind(ipep);
        Console.WriteLine("Waiting for a client...");

        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        EndPoint Remote = (EndPoint)(sender);

        recv = newsock.ReceiveFrom(data, ref Remote);

        Console.WriteLine("Message received from {0}:", Remote.ToString());
        Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

        string welcome = "Welcome to my test server";
        data = Encoding.ASCII.GetBytes(welcome);
        newsock.SendTo(data, data.Length, SocketFlags.None, Remote);
        while (true)
        {
            data = new byte[1024];
            recv = newsock.ReceiveFrom(data, ref Remote);

            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            Console.WriteLine("Mensagem a ser enviada");
            
            var teste = Console.ReadLine();
            //newsock.SendTo(data, recv, SocketFlags.None, Remote);
            newsock.SendTo(Encoding.ASCII.GetBytes(teste), Remote);

        }
    }
}
