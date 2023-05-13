using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;


namespace sendFiles_Client
{
    public partial class Form1 : Form
    {
        public Socket socketSender { get; set; }
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1024];
            int sent;
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            Socket server = new Socket(AddressFamily.InterNetwork,
                            SocketType.Stream, ProtocolType.Tcp);

            try
            {
                server.Connect(ip);
                Console.WriteLine("Server is listening");
            }
            catch (SocketException error)
            {
                Console.WriteLine("Unable to connect to server.");
                Console.WriteLine(error.ToString());
            }


            Bitmap bmp = new Bitmap("C:\\Users\\ירין\\Downloads\\tra-tran-ZvVhWYA-hXA-unsplash.jpg");

            MemoryStream memoryStream = new MemoryStream();
            bmp.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            Console.WriteLine("BMP saved");

            byte[] bmpBytes = memoryStream.ToArray();
            bmp.Dispose();
            memoryStream.Close();
 
            sent = sendData(server, bmpBytes);

            Console.WriteLine("Disconnecting from server...");
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private static int sendData(Socket socket, byte[] data)
        {
            Console.WriteLine("Byts chunks before send image");

            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;

            byte[] datasize = new byte[4];
            datasize = BitConverter.GetBytes(size);
            sent = socket.Send(datasize);

            while (total < size)
            {
                sent = socket.Send(data, total, dataleft, SocketFlags.None);
                total += sent;
                dataleft -= sent;
            }
            Console.WriteLine("I finished and now send the image");

            return total;
        }
    }
}
