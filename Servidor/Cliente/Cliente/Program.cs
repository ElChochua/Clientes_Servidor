using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Cliente
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(remoteEP);
                byte[] msg = Encoding.ASCII.GetBytes("Mensaje de texto del ciente<EOF>");
                int bytesSent = sender.Send(msg);
                Console.WriteLine("Mensaje enviado");
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
                Console.Read();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}