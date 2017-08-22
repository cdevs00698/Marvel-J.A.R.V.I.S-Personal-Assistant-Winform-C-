using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.AudioFormat;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Diagnostics;
using System.Web;
using System.Security;
using System.Threading;
using System.Net;
using System.Globalization;
using System.Net.NetworkInformation;
using Marvel_J.A.R.V.I.S_Personal_Assistant;
using System.Runtime.InteropServices;
using System.Xml;
using System.Text.RegularExpressions;

namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    public partial class Todaynews : Form
    {
        int Top;
        int MoveX;
        int MoveY;
        SpeechRecognitionEngine speechRecognitionEngine = null;
        SpeechSynthesizer Marvel = new SpeechSynthesizer();
        bool flag = false;
        public Todaynews()
        {
            InitializeComponent();
            try
            {
                speechRecognitionEngine = createSpeechEngine("en-US");
                // hook to events
                //speechRecognitionEngine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(engine_AudioLevelUpdated);
                speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);

                // load dictionary
                loadGrammarAndCommands();

                // use the system's default microphone
                speechRecognitionEngine.SetInputToDefaultAudioDevice();

                // start listening
                speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                Marvel.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(Marvel_SpeakCompleted);
                if (Marvel.State == SynthesizerState.Speaking)
                    Marvel.SpeakAsyncCancelAll();
                foreach (InstalledVoice voice in Marvel.GetInstalledVoices())
                {
                    cbVoice.Items.Add(voice.VoiceInfo.Name);
                    if (cbVoice.Text != "IVONA 2 Brian OEM")
                    {
                        cbVoice.SelectedItem = "IVONA 2 Brian OEM";
                        Marvel.SelectVoice("IVONA 2 Brian OEM");
                    }
                    else
                    {
                        cbVoice.SelectedItem = "Microsoft David Desktop";
                        Marvel.SelectVoice("Microsoft David Desktop");
                    }
                }
                //stopbtn.Enabled = true;
            }
            catch (Exception)
            {
                Marvel.SpeakAsync("");
            }
        }
        private SpeechRecognitionEngine createSpeechEngine(string preferredCulture)
        {
            foreach (RecognizerInfo config in SpeechRecognitionEngine.InstalledRecognizers())
            {
                if (config.Culture.ToString() == preferredCulture)
                {
                    speechRecognitionEngine = new SpeechRecognitionEngine(config);
                    break;
                }
            }

            // if the desired culture is not found, then load default
            if (speechRecognitionEngine == null)
            {
                MessageBox.Show("The desired culture is not installed on this machine, the speech-engine will continue using "
                    + SpeechRecognitionEngine.InstalledRecognizers()[0].Culture.ToString() + " as the default culture.",
                    "Culture " + preferredCulture + " not found!");
                speechRecognitionEngine = new SpeechRecognitionEngine(SpeechRecognitionEngine.InstalledRecognizers()[0]);
            }

            return speechRecognitionEngine;
        }
        /// <summary>
        /// //////////////////////copy clipbord //////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /// <summary>
        /// //////////////////Speed of speech ///////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void voicespeed_ValueChanged(object sender, EventArgs e)
        {
            int rate = 2 * (voicespeed.Value - 5);
            Marvel.Rate = rate;
            _lblSpeed.Text = string.Format("Speed ({0}):", rate.ToString());
        }
        /// <summary>
        /// //////////////////////////end here ////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Marvel_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            btnPlay.Enabled = true;
            btnPause.Enabled = false;
        }
        private void loadGrammarAndCommands()
        {
            try
            {
                Choices texts = new Choices();
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\jarvisnewscommnds.txt");
                texts.Add(lines);
                Grammar wordsList = new Grammar(new GrammarBuilder(texts));
                speechRecognitionEngine.LoadGrammar(wordsList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region speechEngine events
        /// <summary>
        /// Handles the SpeechRecognized event of the engine control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Speech.Recognition.SpeechRecognizedEventArgs"/> instance containing the event data.</param>
        void engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            string speech = (e.Result.Text);
            switch (speech)
            {
                //GREETINGS
                case "get bing news":
                    getbingnewsbtn.PerformClick();
                    break;
                case "get google news":
                    getgooglenewsbtn.PerformClick();
                    break;
                case "continue":
                    btnPlay.PerformClick();
                    break;
                case "pause":
                    btnPause.PerformClick();
                    break;
                case "resume":
                    btnPause.PerformClick();
                    break;
                case "stop":
                case "stop talking":
                    stopbtn.PerformClick();
                    break;
                case "convert text to audio":
                    btnDownload.PerformClick();
                    break;
                case "close news report":
                    closebtn.PerformClick();
                    break;
                case "minimize":
                case "hide news report":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Minimized;
                    TopMost = false;
                    break;
                case "show news report":
                case "show news report again":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Normal;
                    TopMost = true;
                    break;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // unhook events
            speechRecognitionEngine.RecognizeAsyncStop();
            // clean references
            speechRecognitionEngine.Dispose();

        }
        private void Play(string words)
        {
            words = convertedtxt.Text;
            Marvel.SpeakAsync(words);
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
            stopbtn.PerformClick();
            btnPlay.Enabled = false;
            btnPause.Enabled = true;
            foreach (string s in convertedtxt.Items)
            {
                string name = s;
                Marvel.SpeakAsync(s);
            }

        }
        private void btnPause_Click(object sender, EventArgs e)
        {

            if (Marvel.State == SynthesizerState.Speaking)
            {
                Marvel.Pause();
                btnPause.Text = "Resume";
            }
            else
            {
                if (Marvel.State == SynthesizerState.Paused)
                {
                    Marvel.Resume();
                    btnPause.Text = "Pause";
                }
            }
        }
        private void stopbtn_Click(object sender, EventArgs e)
        {

            if (Marvel.State == SynthesizerState.Paused)
                Marvel.Resume();
            Marvel.SpeakAsyncCancelAll();
            //convertedtxt.Clear();
            stopbtn.Text = "Stop";
            btnPlay.Enabled = true;
        }
        private void btnDownload_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            if (browser.ShowDialog() == DialogResult.OK)
            {
                SpeechSynthesizer CSynthesizer = new SpeechSynthesizer();
                CSynthesizer.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>
                    (CSynthesizer_SpeakCompleted);
                CSynthesizer.SetOutputToWaveFile(string.Concat(browser.SelectedPath,
                    "\\MyTTS.wav"));
                PromptBuilder builder = new PromptBuilder();
                builder.AppendText(convertedtxt.Text);
                CSynthesizer.SpeakAsync(builder);
            }
        }

        void CSynthesizer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            Marvel.Speak("Audio downloaded sucessfully");
        }
        //private void openbtn_Click(object sender, EventArgs e)
        //{
        //    computer.Speak("choose a text file from your, drives");
        //    Stream myStream;
        //    OpenFileDialog openFileDialog1 = new OpenFileDialog();

        //    openFileDialog1.InitialDirectory = "c:\\";
        //    openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
        //    openFileDialog1.FilterIndex = 2;
        //    openFileDialog1.RestoreDirectory = true;

        //    if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        try
        //        {
        //            if ((myStream = openFileDialog1.OpenFile()) != null)
        //            {
        //                using (myStream)
        //                {
        //                    string strfilename = openFileDialog1.FileName;
        //                    string filetext = File.ReadAllText(strfilename);
        //                    convertedtxt.Text = filetext;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
        //        }

        //    }
        //}

        private void closebtn_Click(object sender, EventArgs e)
        {

            this.Close();
        }
        private void Todaynews_Load(object sender, EventArgs e)
        {
            Todaynews wr = new Todaynews();
            wr.TopMost = true;
            MarvelJPA fc = Application.OpenForms["Todaynews "] != null ? (MarvelJPA)Application.OpenForms["Todaynews "] : null;
            if (fc != null)
            {
                fc.Close();
            }
            convertedtxt.Text = string.Empty;
            labelnet.Text = NetworkInterface.GetIsNetworkAvailable().ToString();
            if (labelnet.Text != "True")
            {
                Marvel.SpeakAsync("Please check your internet connection, before the news broadcast panel, work properly");
                this.Close();
            }
            else
            {
                Marvel.SpeakAsync("today latest news is");
                getgooglenewsbtn.PerformClick();
            }
            ////////////////////////commands ///////////////////////////////////
            //////////////here voice search ////////////////////////////

            ////////////////////////end here ///////////////////////////////////

            // Start listening for clipboard changes

        }

        private void getbingnewsbtn_Click(object sender, EventArgs e)
        {
            convertedtxt.Items.Clear();
            WebClient webc = new WebClient();
            string page = webc.DownloadString("http://www.bing.com/news/search?q=&nvaug=%5bNewsVertical+Category%3d%22rt_World%22%5d&FORM=NSBABR");
            Todaynewsbrow.Navigate("http://www.bing.com/news/search?q=&nvaug=%5bNewsVertical+Category%3d%22rt_World%22%5d&FORM=NSBABR");
            //string news = "<span name=\"desc\" class=\"snippet\">(.*?)</span>";
            //string news = "<span class=\"st\">(.*?)</span>";
            //< span class="snippet" data-tag="" name="desc">
            string news = "<span class=\"sn_snip\">(.*?)</span>";
            news = "<div class=\"snippet\">(.*?)</div>";
            foreach (Match match in Regex.Matches(page, news))
                convertedtxt.Items.Add(match.Groups[1].Value);
            btnPlay.PerformClick();
        }

        private void getgooglenewsbtn_Click(object sender, EventArgs e)
        {
            convertedtxt.Items.Clear();
            WebClient webc = new WebClient();
            string page = webc.DownloadString("https://news.google.com/?edchanged=1&ned=us&authuser=0");
            Todaynewsbrow.Navigate("https://news.google.com/?edchanged=1&ned=us&authuser=0");
            //string news = "<span name=\"desc\" class=\"snippet\">(.*?)</span>";
            string news = "<div class=\"esc-lead-snippet-wrapper\">(.*?)</div>";
            foreach (Match match in Regex.Matches(page, news))
                convertedtxt.Items.Add(match.Groups[1].Value);
            btnPlay.PerformClick();
        }

        private void minbtn_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Top = 1;
            MoveX = e.X;
            MoveY = e.Y;
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
    }
}
#endregion
