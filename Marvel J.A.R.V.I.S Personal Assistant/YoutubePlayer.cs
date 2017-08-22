using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxWMPLib;
using WMPLib;
using System.IO;
using System.Media;
using System.Diagnostics;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    public partial class YoutubePlayer : Form
    {
        int Top;
        int MoveX;
        int MoveY;
        SpeechRecognitionEngine speechRecognitionEngine = null;
        SpeechSynthesizer Marvel = new SpeechSynthesizer();
        Grammar youtubecommandgrammar;
        string[] files, paths;
        StreamWriter sw;
        String[] ArrayYoutubeCommands;
        String[] ArrayYoutubeAddress;
        int i = 0;
        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public YoutubePlayer()
        {

            InitializeComponent();
            try
            {
                speechRecognitionEngine = createSpeechEngine("en-US");
                // hook to events
                speechRecognitionEngine.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(Environment.CurrentDirectory + "\\youtubecommands.txt")))));
                //speechRecognitionEngine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(engine_AudioLevelUpdated);
                speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(youtube_SpeechRecognized);
                speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);

                // use the system's default microphone
                speechRecognitionEngine.SetInputToDefaultAudioDevice();
                // start listening
                speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                Properties.Settings.Default.Save();
                MarvelJPA.youtubecpath = Properties.Settings.Default.YTC;
                MarvelJPA.youtubeurlpath = Properties.Settings.Default.YTA;
                //computer.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(computer_SpeakCompleted);
                if (!File.Exists(MarvelJPA.websearchpath))
                {
                    sw = File.CreateText(MarvelJPA.websearchpath); sw.Write("Play promises song"); sw.Close();
                }
                if (!File.Exists(MarvelJPA.websearchkeypath))
                {
                    sw = File.CreateText(MarvelJPA.websearchkeypath); sw.Write("http://www.youtube.com/watch?v=HLphrgQFHUQ"); sw.Close();
                }
                ArrayYoutubeCommands = File.ReadAllLines(MarvelJPA.youtubecpath); //This loads all written commands in our Custom Commands text documents into arrays so they can be loaded into our grammars
                ArrayYoutubeAddress = File.ReadAllLines(MarvelJPA.youtubeurlpath);
                // load dictionary
                loadGrammarAndCommands();
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
        private void engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //scvText.ScrollToEnd();
            string speech = (e.Result.Text);
            switch (speech)
            {
                //GREETINGS
                case "play":
                    searchbtn.PerformClick();
                    break;
                case "play previous song":
                    prebtn.PerformClick();
                    break;
                case "stop":
                case "stop music":
                    stopbtn.PerformClick();
                    break;
                case "close youtube player":
                    this.Close();
                    break;
            }
        }
        #region speechEngine events
        /// <summary>
        /// Handles the SpeechRecognized event of the engine control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Speech.Recognition.SpeechRecognizedEventArgs"/> instance containing the event data.</param>
        void youtube_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            i = 0;
            try
            {
                foreach (string line in ArrayYoutubeCommands)
                {
                    if (line == speech)
                    {
                        Marvel.SpeakAsync("You said, " + ArrayYoutubeCommands[i]);
                        searchtxt.Text = ArrayYoutubeAddress[i];
                        searchbtn.PerformClick();
                    }
                    i += 1;
                }
            }
            catch
            {
                i += 1;
                Marvel.SpeakAsync("Please check the " + speech + " youtube command on line " + i + ". It appears to be missing a proper response");
            }
        }
        private void loadGrammarAndCommands()
        {
            try
            {
                Choices texts = new Choices();
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\youtubecommands.txt");
                texts.Add(lines);
                Grammar wordsList = new Grammar(new GrammarBuilder(texts));
                speechRecognitionEngine.LoadGrammar(wordsList);
                try
                { youtubecommandgrammar = new Grammar(new GrammarBuilder(new Choices(ArrayYoutubeCommands))); speechRecognitionEngine.LoadGrammarAsync(youtubecommandgrammar); }
                catch
                { Marvel.SpeakAsync("I've detected an in valid entry in your youtube commands, possibly a blank line. Youtube commands will cease to work until it is fixed."); }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void YoutubePlayer_Load(object sender, EventArgs e)
        {
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string videoEntryUrl = searchtxt.Text;
            if (youtubebrowser == null)
            {
                Marvel.SpeakAsync("Please say some thing");
            }
            else
            {
                youtubebrowser.Navigate(videoEntryUrl);
            }
        }

        private void stopbtn_Click(object sender, EventArgs e)
        {
            youtubebrowser.Navigate("https://www.youtube.com");
        }

        private void prebtn_Click(object sender, EventArgs e)
        {
            youtubebrowser.GoBack();
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // unhook events
            speechRecognitionEngine.RecognizeAsyncStop();
            // clean references
            speechRecognitionEngine.Dispose();
        }
    }
}
#endregion
