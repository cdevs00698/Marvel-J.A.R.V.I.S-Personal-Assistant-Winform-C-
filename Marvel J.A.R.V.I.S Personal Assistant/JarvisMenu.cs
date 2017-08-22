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
    public partial class JarvisMenu : Form
    {
        int Top;
        int MoveX;
        int MoveY;
        public JarvisMenu()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Top = 1;
            MoveX = e.X;
            MoveY = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Top == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MoveX, MousePosition.Y - MoveY);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Top = 0;
        }
    }
}

