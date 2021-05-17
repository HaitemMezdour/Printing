using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;


namespace Printing
{
    public class Printer
    {
        private static string _IpAdress  { get; set; }
        private static Int32 _Port { get; set; }
        static readonly IPAddress IpAddress = IPAddress.Parse(_IpAdress);
        static readonly IPEndPoint endpoint = new(IpAddress, _Port);
        static readonly Socket socket = new(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        
        
        
        

        public Printer()
        {
            socket.Connect(endpoint);


        }
        public PrintStatus DoPrntiJob(string  Path)
        {
             ESC CMD = new ESC();
             PrintStatus status=0;
             var buffer = new ArrayList();
             NetworkStream networkStream = new NetworkStream(socket, true);
            try
            {
                //Créez une instance de StreamReader pour lire à partir d'un fichier
                using (StreamReader sr = new StreamReader(Path))
                {
                    string line;
                    // Lire les lignes du fichier jusqu'à la fin.
                    int i = 0;

                    while ((line = sr.ReadLine()) != null)
                    {

                        i++;
                        Console.WriteLine(line + '\n');
                        buffer.Add(System.Text.Encoding.UTF8.GetBytes((line + '\n')));
                        byte[] dataTobePrinted = (byte[])buffer[i - 1];
                        networkStream.Write(dataTobePrinted, 0, dataTobePrinted.Length);
                        if (line.Contains(";"))
                        {
                            networkStream.Write(System.Text.Encoding.UTF8.GetBytes(CMD.GotoStartPosition), 0, CMD.GotoStartPosition.Length);

                        }

                    }
                    networkStream.Write(System.Text.Encoding.UTF8.GetBytes(CMD.GotoStoCutPosition), 0, CMD.GotoStartPosition.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être lu.");
                Console.WriteLine(e.Message);
            }
            return status;
        }

        
    }
}
