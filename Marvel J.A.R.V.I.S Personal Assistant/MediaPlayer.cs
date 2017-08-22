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
    public partial class MediaPlayer : Form
    {
        int Top;
        int MoveX;
        int MoveY;
        SpeechRecognitionEngine speechRecognitionEngine = null;
        SpeechSynthesizer Marvel = new SpeechSynthesizer();
        WMPLib.WindowsMediaPlayer WindowsMediaPlayer = new WMPLib.WindowsMediaPlayer();
        public static String userName = Environment.UserName;
        string[] files, paths;

        public MediaPlayer()
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
                //computer.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(computer_SpeakCompleted);
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
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\mediaplayercl.txt");

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
                case "open play list":
                case "add play list":
                    Marvel.Speak("choose, music file from your drives");
                    btnaddplay.PerformClick();
                    break;
                case "minimize":
                case "hide media player":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Minimized;
                    TopMost = false;
                    break;
                case "show media player":
                case "show media player again":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Normal;
                    TopMost = true;
                    break;
                case "play":
                    btnplay.PerformClick();
                    break;
                case "fast forward":
                    fastfarwordbuttton.PerformClick();
                    break;
                case "fast reverse":
                    fastreversebutton.PerformClick();
                    break;
                case "resume":
                    btnpause.PerformClick();
                    break;
                case "pause":
                    btnpause.PerformClick();
                    break;
                case "stop":
                    btnstop.PerformClick();
                    break;
                case "previous":
                    btnpre.PerformClick();
                    break;
                case "next":
                    btnnext.PerformClick();
                    break;
                case "media player full screen":
                    btnfullscreen.PerformClick();
                    break;
                case "mute volume":
                    btnvolumedown.PerformClick();
                    break;
                case "volume up":
                    btnvolumeup.PerformClick();
                    break;
                case "close media player":
                    closebtn.PerformClick();
                    break;
                case "exit full screen":
                    axWindowsMediaPlayer1.Focus();
                    axWindowsMediaPlayer1.fullScreen = false;
                    break;
            }
        }
        private void btnaddplay_Click(object sender, EventArgs e)
        {
            string userName = System.Environment.UserName;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\Users\" + userName + "\\Documents\\MyMusic";
            ofd.Filter = "(mp3,wav,mp4,mov,wmv,mpg,avi,3gp,flv)|*.mp3;*.wav;*.mp4;*.3gp;*.avi;*.mov;*.flv;*.wmv;*.mpg|all files|*.*";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                files = ofd.SafeFileNames;
                paths = ofd.FileNames;
                for (int i = 0; i < files.Length; i++)
                {
                    playlist.Items.Add(files[i]);
                }
            }
            //string[] files = Directory.GetFiles(@"C:\Users\" + userName + "\\Music\\", "*.mp4");
            //foreach (string filelist in files)
            //{
            //    playlist.Items.Add(Path.GetFileName(filelist));
            //    //axWindowsMediaPlayer1.URL = paths[playlist.SelectedIndex];
            //}
        }
        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                playbacktimer.Enabled = true;
                playbacktimer.Interval = 100;
            }
        }
        private void playlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = paths[playlist.SelectedIndex];
        }
        private void btnplay_Click(object sender, EventArgs e)
        {
            playlist.SelectedIndex = 0;
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void btnpause_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void btnpre_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                if (playlist.SelectedIndex == 0)
                {
                    playlist.SelectedIndex = 0;
                    playlist.Update();

                }
                else
                {
                    axWindowsMediaPlayer1.Ctlcontrols.previous();
                    playlist.SelectedIndex -= 1;
                    playlist.Update();
                }
            }
        }
        private void btnnext_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                if (playlist.SelectedIndex < (playlist.Items.Count - 1))
                {
                    axWindowsMediaPlayer1.Ctlcontrols.next();
                    playlist.SelectedIndex += 1;
                    playlist.Update();
                }
                else
                {
                    playlist.SelectedIndex = 0;
                    playlist.Update();
                }
            }
        }

        private void btnfullscreen_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer1.fullScreen = true;
            }
            else
            {
                axWindowsMediaPlayer1.fullScreen = false;
            }

        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Dispose();
            this.Close();
        }
        private void btnvolumeup_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = 100;
        }

        private void btnvolumedown_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.settings.volume == 100)
            {
                axWindowsMediaPlayer1.settings.volume = 0;
            }
            else
            {
                axWindowsMediaPlayer1.settings.volume = 100;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // unhook events
            speechRecognitionEngine.RecognizeAsyncStop();
            // clean references
            speechRecognitionEngine.Dispose();
        }
        public System.String uiMode { get; set; }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void MediaPlayer_Load(object sender, EventArgs e)
        {
            MediaPlayer mplayer = new MediaPlayer();
            mplayer.TopMost = true;
            axWindowsMediaPlayer1.PlayStateChange += axWindowsMediaPlayer1_PlayStateChange;
            axWindowsMediaPlayer1.uiMode = "none";


        }

        private void playbacktimer_Tick(object sender, EventArgs e)
        {
            playbacktimer.Start();
            if (playlist.SelectedIndex < files.Length - 1)
            {
                playlist.SelectedIndex++;
                playbacktimer.Enabled = false;
            }
            else
            {
                playlist.SelectedIndex = 0;
                playbacktimer.Enabled = false;
            }
            playbacktimer.Stop();
        }

        private void fastforwardbutton_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.fastReverse();
        }

        private void fastreversebutton_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.fastForward();
        }

        private void resumebtn_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int rate = 100 * (voicespeed.Value - 10);
            axWindowsMediaPlayer1.settings.volume = voicespeed.Value;
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

        private void uiModeOptions_OnSelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Get the selected UI mode in the list box as a string.
            string newMode = (string)(((System.Windows.Forms.ListBox)sender).SelectedItem);

            // Set the UI mode that the user selected.
            axWindowsMediaPlayer1.uiMode = newMode;
        }
    }
}
#endregion
