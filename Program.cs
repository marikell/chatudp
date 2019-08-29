//using System;
//using UDP;

//namespace p01_chatudp
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            try
//            {
//                Console.WriteLine("Digite a porta host");
//                string hostPort = Console.ReadLine();

//                Console.WriteLine("Digite o IP para enviar");
//                string ip = Console.ReadLine();

//                Console.WriteLine("Digite a porta para enviar");
//                string portaSend = Console.ReadLine();


//                int portaEnviaInt = int.Parse(portaSend);
//                int portaHostInt = int.Parse(hostPort);

//                UDPSocket s = new UDPSocket();
//                s.Server(ip, portaHostInt);

//                UDPSocket c = new UDPSocket();
//                c.Client(ip, portaEnviaInt);

//                while (true)
//                {
//                    Console.WriteLine("Digite uma mensagem:");
//                    c.Send(Console.ReadLine());
//                }
//            }
//            catch (Exception ex)
//            {

//                Console.WriteLine("[Error] : {0}", ex.Message);
//            }
//        }
//    }
//}

using System;

public class UdpSrvrSample
{
    public static void Main()
    {
        Console.WriteLine("Escolha o que deseja ser: server ou client");
        var tipo = Console.ReadLine();

        int valor = Int32.Parse(tipo);

        if (valor == 1)
        {
            UdpServer.Run();
        }
        else if (valor == 2)
        {
            Console.WriteLine("Digite o IP:");
            string ip = Console.ReadLine();
            Console.WriteLine("Digite a porta:");
            string port = Console.ReadLine();

            UdpClient.Run(ip, int.Parse(port));
        }

    }
}
