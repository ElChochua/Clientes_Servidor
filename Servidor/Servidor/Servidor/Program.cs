using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace SocketTextoServidor
{
    class Program
    {
        public static string data = null;
        public static void Main(string[] args)
        {
            byte[] bytes = new byte[1024];
            //Creamos el socket
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(localEndPoint); //Asociar ip al puerto
                listener.Listen(10); //Escucha para conexiones
                while (true)
                {
                    Socket handler = listener.Accept();
                    data = null;
                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if(data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                        Console.WriteLine(data);
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.Read();
        }
    }
}