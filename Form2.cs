using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sendFiles_Client
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 formOne = new Form1();
            this.Hide();
            formOne.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "*.jpg|*.png";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox2.ImageLocation= dialog.FileName;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
