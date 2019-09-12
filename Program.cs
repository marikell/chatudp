using System;
using System.Collections.Generic;
using System.Threading;
using UDP;

namespace p01_chatudp
{
    class Program
    {
        static void Main(string[] args)
        {
                
            try
            {
                Console.WriteLine("150875-150716 (Chat)");
                Console.WriteLine("Digite a porta host");
                string hostPort = Console.ReadLine();

                Console.WriteLine("Digite o nome do host:");
                string hostName = Console.ReadLine();

                int portaHostInt = int.Parse(hostPort);
                UDPSocket s = new UDPSocket(hostName);
                
                int resposta = 1;

                List<UDPSocket> knowHosts = new List<UDPSocket>();

                while (resposta == 1)
                {

                    Console.WriteLine("Digite 0 para sair ou 1 para digitar outro IP/PORTA");
                    resposta = int.Parse(Console.ReadLine());

                    if(resposta == 1)
                    {

                        Console.WriteLine("Digite o ip: ");
                        string newIP = Console.ReadLine();

                        Console.WriteLine("Digite a porta: ");
                        int newPort = int.Parse(Console.ReadLine());

                        UDPSocket udpS = new UDPSocket();
                        udpS.Client(newIP, newPort);
                        s.AddKnowHost(udpS);
                    }

                }

                s.Server(portaHostInt);

                while (true)
                {
                    Thread.Sleep(2000);
                    s.SendHeartBeat();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("[Error] : {0}", ex.Message);
            }
        }
    }
}

//using System;

//public class UdpSrvrSample
//{
//    public static void Main()
//    {
//        Console.WriteLine("Escolha o que deseja ser: server ou client");
//        var tipo = Console.ReadLine();

//        int valor = Int32.Parse(tipo);

//        if (valor == 1)
//        {
//            UdpServer.Run();
//        }
//        else if (valor == 2)
//        {
//            Console.WriteLine("Digite o IP:");
//            string ip = Console.ReadLine();
//            Console.WriteLine("Digite a porta:");
//            string port = Console.ReadLine();

//            UdpClient.Run(ip, int.Parse(port));
//        }

//    }
//}
