using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    public partial class MJRightMenu : Form
    {
        int Top;
        int MoveX;
        int MoveY;
        public MJRightMenu()
        {
            InitializeComponent();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Top = 0;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Top == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MoveX, MousePosition.Y - MoveY);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Top = 1;
            MoveX = e.X;
            MoveY = e.Y;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WeatherReport weather = new WeatherReport();
            weather.Show();
            weather.TopMost = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            WebReader wr = new WebReader();
            wr.Show();
            wr.TopMost = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            WebReader wr = new WebReader();
            wr.Show();
            wr.TopMost = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
            settings.TopMost = true;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\Public\Desktop\Skype");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Todaynews tdn = new Todaynews();
            tdn.Show();
            tdn.TopMost = true;
        }
    }
}

