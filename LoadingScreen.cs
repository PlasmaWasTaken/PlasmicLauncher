using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plasmic_Client
{
    public partial class LoadingScreen : Form
    {
        public LoadingScreen()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            bunifuGradientPanel1.Width = bunifuGradientPanel1.Width + 1;
            if (bunifuGradientPanel1.Width == 707)
            {
                timer1.Enabled = false;
                bunifuGradientPanel1.Width = 0;
                this.Hide();
                Form2 main = new Form2();
                main.Show();
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void LoadingScreen_Load(object sender, EventArgs e)
        {

            label1.Text = "Loading";
            await Task.Delay(1000);
            label1.Text = "Loading.";
            await Task.Delay(1000);
            label1.Text = "Loading..";
            await Task.Delay(1000);
            label1.Text = "Loading...";
            

          
           
          
        }
    }
}
