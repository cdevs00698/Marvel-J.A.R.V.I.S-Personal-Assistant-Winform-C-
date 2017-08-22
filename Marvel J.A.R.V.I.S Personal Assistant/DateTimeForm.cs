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
    public partial class DateTimeForm : Form
    {
        int Top;
        int MoveX;
        int MoveY;
        public DateTimeForm()
        {
            InitializeComponent();
        }

        private void datetimer_Tick(object sender, EventArgs e)
        {
            datetimer.Start();
            string time = System.DateTime.Now.ToString("hh:mm:ss");
            string am = System.DateTime.Now.ToString("tt");
            labeltime.Text = time;
            labeltime2.Text = am;
        }

        private void DateTime_Load(object sender, EventArgs e)
        {
            System.DateTime nowtime = System.DateTime.Now;
            for (int i = 0; i < 12; i++)
            {
                nowtime = nowtime.AddMonths(1);
                labelmonth.Text = nowtime.ToString("MMMM");
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Top = 0;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Top == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MoveX, MousePosition.Y - MoveY);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Top = 1;
            MoveX = e.X;
            MoveY = e.Y;
        }
    }
}

