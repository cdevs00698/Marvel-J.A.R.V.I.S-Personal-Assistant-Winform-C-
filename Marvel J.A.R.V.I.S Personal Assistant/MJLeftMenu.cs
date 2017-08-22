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
    public partial class MJLeftMenu : Form
    {
        int Top;
        int MoveX;
        int MoveY;
        public MJLeftMenu()
        {
            InitializeComponent();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Top = 1;
            MoveX = e.X;
            MoveY = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Top == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MoveX, MousePosition.Y - MoveY);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Top = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Name name = new Name();
            name.Show();
            name.TopMost = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MailReader mr = new MailReader();
            mr.Show();
            mr.TopMost = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MediaPlayer mplayer = new MediaPlayer();
            mplayer.Show();
            mplayer.TopMost = true;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Reminder reminder = new Reminder();
            reminder.Show();
            reminder.TopMost = true;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Websearch ws = new Websearch();
            ws.Show();
            ws.TopMost = true;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            TextReading tr = new TextReading();
            tr.Show();
            tr.TopMost = true;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Commandlist cl = new Commandlist();
            cl.Show();
            cl.TopMost = true;
        }
    }
}
