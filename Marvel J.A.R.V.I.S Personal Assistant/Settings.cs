using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Marvel_J.A.R.V.I.S_Personal_Assistant.Properties;
using System.Speech.Synthesis;

namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    public partial class Settings : Form
    {
        int Top;
        int MoveX;
        int MoveY;
        SpeechSynthesizer computer = new SpeechSynthesizer();
        StreamReader sr;
        public Settings()
        {
            InitializeComponent();
            try
            {
                sr = new StreamReader(MarvelJPA.shellcpath); shellcommandstxt.Text = sr.ReadToEnd(); sr.Close();
                sr = new StreamReader(MarvelJPA.shellrespath); jarvisshellresponsetxt.Text = sr.ReadToEnd(); sr.Close();
                sr = new StreamReader(MarvelJPA.shellocpath); jarvisshelllocationtxt.Text = sr.ReadToEnd(); sr.Close();
                sr = new StreamReader(MarvelJPA.webcpath); webcmdtxt.Text = sr.ReadToEnd(); sr.Close();
                sr = new StreamReader(MarvelJPA.webrespath); webkeywordstxt.Text = sr.ReadToEnd(); sr.Close();
                sr = new StreamReader(MarvelJPA.socmdpath); socialcmdtxt.Text = sr.ReadToEnd(); sr.Close();
                sr = new StreamReader(MarvelJPA.sorespath); responsesocialtxt.Text = sr.ReadToEnd(); sr.Close();
                sr = new StreamReader(MarvelJPA.websearchpath); websearchtxt.Text = sr.ReadToEnd(); sr.Close();
                sr = new StreamReader(MarvelJPA.websearchkeypath); searchkeywordstxt.Text = sr.ReadToEnd(); sr.Close();
                sr = new StreamReader(MarvelJPA.weatherchpath); weathercmd.Text = sr.ReadToEnd(); sr.Close();
                sr = new StreamReader(MarvelJPA.weathercitypath); weathercity.Text = sr.ReadToEnd(); sr.Close();
            }
            catch (Exception)
            {
                computer.SpeakAsync("Please Plug in Your Microphone Before Settings Get Work");
                MessageBox.Show("Please Plugin Your Microphone Before Settings Get Work");
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addshellcombtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void socialcmdbtn_Click(object sender, EventArgs e)
        {


            tabControl1.SelectedIndex = 1;
        }

        private void webcmdbtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }
        private void websearchbtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }
        private void weatherset_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
        }
        private void YTbtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 5;
        }
        private void helpcmdbtn_Click(object sender, EventArgs e)
        {
            Commandlist coml = new Commandlist();
            coml.Show();
            coml.TopMost = true;
        }

        private void savecmdbtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(MarvelJPA.shellcpath))
                { sw.Write(shellcommandstxt.Text); sw.Close(); }
                using (StreamWriter sw = File.CreateText(MarvelJPA.shellrespath))
                { sw.Write(jarvisshellresponsetxt.Text); sw.Close(); }
                using (StreamWriter sw = File.CreateText(MarvelJPA.shellocpath))
                { sw.Write(jarvisshelllocationtxt.Text); sw.Close(); }
                using (StreamWriter sw = File.CreateText(MarvelJPA.webcpath))
                { sw.Write(webcmdtxt.Text); sw.Close(); }
                using (StreamWriter sw = File.CreateText(MarvelJPA.webrespath))
                { sw.Write(webkeywordstxt.Text); sw.Close(); }
                using (StreamWriter sw = File.CreateText(MarvelJPA.socmdpath))
                { sw.Write(socialcmdtxt.Text); sw.Close(); }
                using (StreamWriter sw = File.CreateText(MarvelJPA.sorespath))
                { sw.Write(responsesocialtxt.Text); sw.Close(); }
                using (StreamWriter sw = File.CreateText(MarvelJPA.websearchpath))
                { sw.Write(websearchtxt.Text); sw.Close(); }
                using (StreamWriter sw = File.CreateText(MarvelJPA.websearchkeypath))
                { sw.Write(searchkeywordstxt.Text); sw.Close(); }
                using (StreamWriter sw = File.CreateText(MarvelJPA.weatherchpath))
                { sw.Write(weathercmd.Text); sw.Close(); }
                using (StreamWriter sw = File.CreateText(MarvelJPA.weathercitypath))
                { sw.Write(weathercity.Text); sw.Close(); }
            }
            catch (Exception)
            {
                computer.SpeakAsync("Commands Are Not Saved Please Plug in Your Microphone Before This Get Work Properly");
                MessageBox.Show("Commands Are Not Saved Please Plugin Your Microphone Before This Get Work Properly");
            }
        }

        private void browsesbtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = File.CreateText(MarvelJPA.shellocpath))
                {
                    sw.Write(jarvisshelllocationtxt.Text);
                    sw.Write(openFileDialog1.FileName);
                    sw.Close();
                }
                sr = new StreamReader(MarvelJPA.shellocpath);
                jarvisshelllocationtxt.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void minbtn_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Minimized;
        }

        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            Top = 0;
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (Top == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MoveX, MousePosition.Y - MoveY);
            }
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            Top = 1;
            MoveX = e.X;
            MoveY = e.Y;
        }
    }
}

