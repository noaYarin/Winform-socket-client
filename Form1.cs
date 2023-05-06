using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sendFiles_Client
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            byte[] bytes = new byte[1024];

            try
            {

                IPHostEntry hostAddr = Dns.GetHostEntry(textBox2.Text);
                IPAddress ipAddress = hostAddr.AddressList[0]; 
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, Int32.Parse(textBox1.Text));

                Socket socketSender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    socketSender.Connect(remoteEP);
                    Console.WriteLine("Socket connected to {0}",
                        socketSender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                    // Send the data through the socket.
                    int bytesSent = socketSender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = socketSender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    socketSender.Shutdown(SocketShutdown.Both);
                    socketSender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception error)
                {
                    Console.WriteLine("Unexpected exception : {0}", error.ToString());
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
        }
    }
}
