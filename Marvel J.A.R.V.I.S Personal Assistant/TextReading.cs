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

namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    public partial class TextReading : Form
    {
        int Top;
        int MoveX;
        int MoveY;
        SpeechRecognitionEngine speechRecognitionEngine = null;
        SpeechSynthesizer Marvel = new SpeechSynthesizer();
        bool flag = false;
        public TextReading()
        {
            InitializeComponent();
            try
            {
                // create the engine
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
                if (Marvel.State == SynthesizerState.Speaking)
                    Marvel.SpeakAsyncCancelAll();
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
                foreach (InstalledVoice voice in Marvel.GetInstalledVoices())
                {
                    cbVoice.Items.Add(voice.VoiceInfo.Name);

                }
            }
            catch (Exception)
            {
                Marvel.SpeakAsync("");
            }
        }
        void computer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            btnPlay.Enabled = true;
            btnPause.Enabled = false;
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
        private void loadGrammarAndCommands()
        {
            try
            {
                Choices texts = new Choices();
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\textreader.txt");
                // add the text to the known choices of speechengine
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
            //scvText.ScrollToEnd();
            string speech = (e.Result.Text);
            switch (speech)
            {
                //GREETINGS
                case "start reading":
                    btnPlay.PerformClick();
                    break;
                case "pause":
                    btnPause.PerformClick();
                    break;
                case "resume":
                    btnPause.PerformClick();
                    break;
                case "stop":
                    stopbtn.PerformClick();
                    break;
                case "open text file":
                    openbtn.PerformClick();
                    break;
                case "convert text to audio":
                    btnDownload.PerformClick();
                    break;
                case "close text reader":
                    closebtn.PerformClick();
                    break;
                case "change voice to microsoft devid":
                    //cbVoice.SelectedIndex = 0;
                    cbVoice.SelectedItem = "Microsoft David Desktop";
                    Marvel.SelectVoice("Microsoft David Desktop");
                    break;
                case "change voice to brian":
                    if (cbVoice.Text != "IVONA 2 Brian OEM")
                    {
                        cbVoice.SelectedItem = "IVONA 2 Brian OEM";
                        Marvel.SelectVoice("IVONA 2 Brian OEM");

                    }
                    else
                    {
                        cbVoice.SelectedItem = "Microsoft David Desktop";
                        Marvel.SelectVoice("Microsoft David Desktop");
                        Marvel.Speak("Ivona Brian, is not installed, here is microsoft david desktop, at your service");
                    }
                    break;
                case "change voice to salli":
                    if (cbVoice.Text != "IVONA 2 Salli OEM")
                    {
                        cbVoice.SelectedItem = "IVONA 2 Salli OEM";
                        Marvel.SelectVoice("IVONA 2 Salli OEM");

                    }
                    else
                    {
                        cbVoice.SelectedItem = "Microsoft Zira Desktop";
                        Marvel.SelectVoice("Microsoft Zira Desktop");
                        Marvel.Speak("Ivona Salli, is not installed, here is microsoft zira desktop, at your service");
                    }
                    break;
                case "change voice to amy":
                    if (cbVoice.Text != "IVONA 2 Amy OEM")
                    {
                        cbVoice.SelectedItem = "IVONA 2 Amy OEM";
                        Marvel.SelectVoice("IVONA 2 Amy OEM");

                    }
                    else
                    {
                        cbVoice.SelectedItem = "Microsoft Zira Desktop";
                        Marvel.SelectVoice("Microsoft Zira Desktop");
                        Marvel.Speak("Ivona Salli, is not installed, here is microsoft zira desktop, at your service");
                    }
                    break;
                case "change voice to microsoft zira":
                    //cbVoice.SelectedIndex = 2;
                    //computer.SelectVoice(cbVoice.Text);
                    cbVoice.SelectedItem = "Microsoft Zira Desktop";
                    Marvel.SelectVoice("Microsoft Zira Desktop");
                    break;
                case "minimize":
                case "hide text reader":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Minimized;
                    TopMost = false;
                    break;
                case "show text reader":
                case "show text reader again":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Normal;
                    TopMost = true;
                    break;
                case "change voice speed to minis two":
                    Marvel.Rate = -1;
                    lblSpeed.Text = "Current Voice Speed: -2";
                    break;
                case "change voice speed to minis four":
                    Marvel.Rate = -4;
                    lblSpeed.Text = "Current Voice Speed: -4";
                    break;
                case "change voice speed to minis six":
                    Marvel.Rate = -6;
                    lblSpeed.Text = "Current Voice Speed: -6";
                    break;
                case "change voice speed to minis eight":
                    Marvel.Rate = -8;
                    lblSpeed.Text = "Current Voice Speed: -8";
                    break;
                case "change voice speed to minis ten":
                    Marvel.Rate = -10;
                    lblSpeed.Text = "Current Voice Speed: -10";
                    break;
                case "change voice speed to two":
                    Marvel.Rate = 2;
                    lblSpeed.Text = "Current Voice Speed: 2";
                    break;
                case "change voice speed to four":
                    Marvel.Rate = 4;
                    lblSpeed.Text = "Current Voice Speed: 4";
                    break;
                case "change voice speed to six":
                    Marvel.Rate = 6;
                    lblSpeed.Text = "Current Voice Speed: 6";
                    break;
                case "change voice speed to eight":
                    Marvel.Rate = 8;
                    lblSpeed.Text = "Current Voice Speed: 8";
                    break;
                case "change voice speed to ten":
                    Marvel.Rate = 10;
                    lblSpeed.Text = "Current Voice Speed: 10";
                    break;
                case "change voice speed back to normal":
                    Marvel.Rate = 0;
                    lblSpeed.Text = "Current Voice Speed: 0";
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
        private void btnPlay_Click(object sender, EventArgs e)
        {
            stopbtn.PerformClick();
            btnPlay.Enabled = false;
            btnPause.Enabled = true;
            Marvel.SpeakAsync(txtWords.Text);
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
                builder.AppendText(txtWords.Text);
                CSynthesizer.SpeakAsync(builder);

            }

        }

        void CSynthesizer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            Marvel.Speak("Audio downloaded sucessfully");
        }
        private void openbtn_Click(object sender, EventArgs e)
        {
            if (Marvel.State == SynthesizerState.Paused)
                Marvel.SpeakAsyncCancelAll();
            txtWords.Clear();
            Marvel.SpeakAsync(txtWords.Text);
            Marvel.Resume();
            Marvel.Speak("choose a text file from your, drives");
            Stream myStream;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|rtf files (*.rtf)|*.rtf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string strfilename = openFileDialog1.FileName;
                            string filetext = File.ReadAllText(strfilename);
                            txtWords.Text = filetext;

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }

            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            // unhook events
            speechRecognitionEngine.RecognizeAsyncStop();
            // clean references
            speechRecognitionEngine.Dispose();
            this.Close();
        }

        private void reading_Load(object sender, EventArgs e)
        {

        }
        private void voicespeed_ValueChanged(object sender, EventArgs e)
        {
            int rate = 2 * (voicespeed.Value - 5);
            Marvel.Rate = rate;
            lblSpeed.Text = string.Format("Current Voice Speed: {0}", rate.ToString());
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
    }
}
#endregion
