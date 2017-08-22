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
    public partial class Commandlist : Form
    {
        int Top;
        int MoveX;
        int MoveY;
        SpeechRecognitionEngine speechRecognitionEngine = null;
        SpeechSynthesizer Marvel = new SpeechSynthesizer();
        public Commandlist()
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
                if (cbVoice.Text != "IVONA 2 Brian OEM")
                {
                    cbVoice.SelectedItem = "IVONA 2 Brian OEM";
                    Marvel.SelectVoice("IVONA 2 Brian OEM");
                }
                else
                {
                    cbVoice.SelectedItem = "Microsoft David Desktop";
                }
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
        private void loadGrammarAndCommands()
        {
            try
            {
                Choices texts = new Choices();
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\Commandslist.txt");
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
                case "show voice commands list":
                case "show voice commands list again":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Normal;
                    TopMost = true;
                    break;
                case "close voice command list panel":
                    closebtn.PerformClick();
                    break;
                case "minimize":
                case "hide voice commands list":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Minimized;
                    TopMost = false;
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
        private void openbtn_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            TopMost = true;
            //txtWords.Text = File.ReadAllText(Environment.CurrentDirectory + "\\Commands.txt");
        }

        private void cbVoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void commandlist_Load(object sender, EventArgs e)
        {
            Commandlist cl = new Commandlist();
            cl.TopMost = true;
            Marvel.Speak("May be this can help you, Here is the complete voice commands list");
        }

        private void minbtn_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Top = 0;
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
    }
}
#endregion
