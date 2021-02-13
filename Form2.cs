using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Plasmic_Client
{
    public partial class Form2 : Form
    {
        public Form2()
        {  
            InitializeComponent();
      
           

        }
        String UserName = Environment.UserName;

        string GetLine(string fileName, string v, int line)
        {
            using (var sr = new StreamReader(fileName))
            {
                for (int i = 1; i < line; i++)
                    sr.ReadLine();
                return sr.ReadLine();
            }
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {

         
        
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        { //
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            SidePanel.Height = Launch.Height;
            SidePanel.Top = Launch.Top;
            System.IO.File.WriteAllText("Workspace/Logs.txt", "----Plasmic Client----");
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void control1_Load_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button3.Top;
            SidePanel.Height = button3.Height;

        }
    }
}
