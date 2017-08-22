using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Media;


namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    public partial class Name : Form
    {
        SpeechSynthesizer Marvel = new SpeechSynthesizer();
        string userName = Environment.UserName;
        public Name()
        {
            InitializeComponent();
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            //computer.SpeakAsyncCancelAll();
            this.Close();
        }
        private void Domain_Load(object sender, EventArgs e)
        {
            Name dm = new Name();
            dm.TopMost = true;
        }

        private void namebtn_Click(object sender, EventArgs e)
        {
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(@"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\name.txt");
                //Write a line of text
                namebox.Text = nametxt.Text;
                sw.WriteLine(nametxt.Text);
                //Write a second line of text
                //sw.WriteLine("From the StreamWriter class");
                namebox.Update();
                //Close the file
                sw.Close();
                Marvel.SpeakAsync("Your name is added, successfully");
                Thread.Sleep(800);
                this.Close();
            }

            catch (Exception)
            {

                Marvel.SpeakAsync("Error try again");
            }

        }
    }
}
