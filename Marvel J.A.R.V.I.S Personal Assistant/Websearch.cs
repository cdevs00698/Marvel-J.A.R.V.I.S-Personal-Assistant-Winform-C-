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
using System.Security.Permissions;
using System.Xml;
using System.Text.RegularExpressions;
using mshtml;

namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    public partial class Websearch : Form
    {
        /// <summary>
        /// //Win32 integration
        /// </summary>
        SpeechRecognitionEngine speechRecognitionEngine = null;
        SpeechSynthesizer Marvel = new SpeechSynthesizer();
        Grammar websearchcommandgrammar;
        bool flag = false;
        int i = 0;
        String[] ArrayWebSearchCommands;
        String[] ArrayWebKeywordSearch;
        public static String userName = Environment.UserName;
        StreamWriter sw;
        int Top;
        int MoveX;
        int MoveY;
        public Websearch()
        {
            InitializeComponent();
            try
            {
                speechRecognitionEngine = createSpeechEngine("en-US");
                speechRecognitionEngine.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(Environment.CurrentDirectory + "\\websitereader.txt")))));
                speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(WebSearch_SpeechRecognized);
                speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);
                // hook to events
                //speechRecognitionEngine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(engine_AudioLevelUpdated);

                // load dictionary
                loadGrammarAndCommands();

                // use the system's default microphone
                speechRecognitionEngine.SetInputToDefaultAudioDevice();

                // start listening
                speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                Marvel.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(computer_SpeakCompleted);

                Properties.Settings.Default.Save();
                MarvelJPA.websearchpath = Properties.Settings.Default.WebS;
                MarvelJPA.websearchkeypath = Properties.Settings.Default.WebSK;
                if (!File.Exists(MarvelJPA.websearchpath))
                {
                    sw = File.CreateText(MarvelJPA.websearchpath); sw.Write("Open Google"); sw.Close();
                }
                if (!File.Exists(MarvelJPA.websearchkeypath))
                {
                    sw = File.CreateText(MarvelJPA.websearchkeypath); sw.Write("Google"); sw.Close();
                }
                ArrayWebSearchCommands = File.ReadAllLines(MarvelJPA.websearchpath); //This loads all written commands in our Custom Commands text documents into arrays so they can be loaded into our grammars
                ArrayWebKeywordSearch = File.ReadAllLines(MarvelJPA.websearchkeypath);
                loadGrammarAndCommands();
                if (Marvel.State == SynthesizerState.Speaking)
                    Marvel.SpeakAsyncCancelAll();
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
        private void WebSearch_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;

            i = 0;
            try
            {
                foreach (string line in ArrayWebSearchCommands)
                {
                    if (line == speech)
                    {
                        Marvel.SpeakAsync(ArrayWebKeywordSearch[i]);
                        keywordstxt.Text = ArrayWebKeywordSearch[i];
                        convertbtn.PerformClick();
                    }
                    i += 1;
                }
            }
            catch
            {
                i += 1;
                Marvel.SpeakAsync("Please check the " + speech + "web command on line " + i + ". It appears to be missing a proper response or web key words");
            }
        }

        private void voicespeed_ValueChanged(object sender, EventArgs e)
        {
            int rate = 2 * (voicespeed.Value - 5);
            Marvel.Rate = rate;
            lblSpeed.Text = string.Format("Speed: {0}", rate.ToString());
        }
        /// <summary>
        /// //////////////////////////end here ////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void computer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            btnPlay.Enabled = true;
            btnPause.Enabled = false;

        }
        private void loadGrammarAndCommands()
        {
            try
            {
                Choices texts = new Choices();
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\websearchkey.txt");
                // add the text to the known choices of speechengine
                texts.Add(lines);
                Grammar wordsList = new Grammar(new GrammarBuilder(texts));
                speechRecognitionEngine.LoadGrammar(wordsList);
                try
                {
                    websearchcommandgrammar = new Grammar(new GrammarBuilder(new Choices(ArrayWebSearchCommands)));
                    speechRecognitionEngine.LoadGrammarAsync(websearchcommandgrammar);
                }
                catch
                { Marvel.SpeakAsync("I've detected an in valid entry in your web commands, possibly a blank line. web commands will cease to work until it is fixed."); }
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
            //inputtxt.Text = getKnownTextOrExecute(e.Result.Text);
            string speech = (e.Result.Text);
            switch (speech)
            {
                //GREETINGS
                case "start reading":
                    CopyScreen.PerformClick();
                    btnPlay.PerformClick();
                    break;
                case "read the result":
                case "read the results":
                case "whats the results":
                case "whats the result":
                case "what is the result":
                    GetResult();
                    break;
                case "search":
                    convertbtn.PerformClick();
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
                case "close website search":
                    closebtn.PerformClick();
                    break;
                case "hide website reader":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Minimized;
                    TopMost = false;
                    break;
                case "show website reader":
                case "show website reader again":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Normal;
                    TopMost = true;
                    break;
                case "change voice to brian":
                    //cbVoice.SelectedIndex = 0;
                    if (cbVoice.Text != "IVONA 2 Brian OEM")
                    {
                        cbVoice.SelectedItem = "IVONA 2 Brian OEM";
                        Marvel.SelectVoice("IVONA 2 Brian OEM");
                    }
                    else
                    {
                        cbVoice.SelectedItem = "Microsoft David Desktop";
                        Marvel.SelectVoice("Microsoft David Desktop");
                        Marvel.Speak("ivona 2 brian is, not installed, here is microsoft david desktop, at your service");
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
                        Marvel.Speak("Ivona salli, is not installed, here is microsoft zira desktop, at your service");
                    }
                    break;
                case "change voice to microsoft devid":
                    //cbVoice.SelectedIndex = 0;
                    //computer.SelectVoice(cbVoice.Text);
                    cbVoice.SelectedItem = "Microsoft David Desktop";
                    Marvel.SelectVoice("Microsoft David Desktop");
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
                        Marvel.Speak("Ivona amy, is not installed, here is microsoft zira desktop, at your service");
                    }
                    break;
                case "change voice to microsoft zira":
                    //cbVoice.SelectedIndex = 2;
                    //computer.SelectVoice(cbVoice.Text);
                    cbVoice.SelectedItem = "Microsoft Zira Desktop";
                    Marvel.SelectVoice("Microsoft Zira Desktop");
                    break;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // unhook events
            //speechRecognitionEngine.RecognizeAsyncStop();
            // clean references
            //speechRecognitionEngine.Dispose();

        }
        private void Play(string words)
        {
            words = convertedtxt.Text;
            Marvel.SpeakAsync(words);
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {

            CopyScreen.PerformClick();
            if (cbVoice.SelectedIndex >= 0)
            {

                stopbtn.PerformClick();
                btnPlay.Enabled = false;
                btnPause.Enabled = true;
                //CopyScreen.PerformClick();
                Marvel.SpeakAsync(convertedtxt.Text);

            }
            else
            {
                Marvel.Speak("Please select a voice for reading");
                cbVoice.Focus();
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

            stopbtn.Text = "Stop";
            btnPlay.Enabled = true;
        }

        private void Websearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            //speechRecognitionEngine.RecognizeAsyncStop();
            // clean references
            //speechRecognitionEngine.Dispose();
            //timer1.Stop();
        }
        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void Websearch_Load(object sender, EventArgs e)
        {
            webreaderbrow.Visible = true;
            //convertedtxt.Text = string.Empty;
            labelnet.Text = NetworkInterface.GetIsNetworkAvailable().ToString();
            //if (labelnet.Text != "True")
            //{
            //    computer.Speak("Please check your internet connection, before the web reader work properly");
            //    this.Close();
            //}
            //else
            //{
            //    computer.Speak("Please wait , succeed");
            //}

            // Start listening for clipboard changes
            //webreaderbrow.Navigate("https://www.bing.com/");
        }
        private void convertbtn_Click(object sender, EventArgs e)
        {

            ////////////search engine //////////////////////////
            string urladdress = keywordstxt.Text;
            webreaderbrow.Navigate("http://www.bing.com/search?q=" + urladdress);
            ////string news = "<span name=\"desc\" class=\"snippet\">(.*?)</span>";
            /////////////////////////end here //////////////////////
            ////webreaderbrow.Document.ExecCommand("Copy", false, null);

        }

        private void CopyScreen_Click(object sender, EventArgs e)
        {
            /////////////////copy screen ////////////////////////////////
            IHTMLDocument2 htmlDocument = webreaderbrow.Document.DomDocument as IHTMLDocument2;

            IHTMLSelectionObject currentSelection = htmlDocument.selection;
            IHTMLTxtRange range = currentSelection.createRange() as IHTMLTxtRange;
            if (currentSelection != null)
            {
                if (range != null)
                {
                    convertedtxt.Text = range.text;
                    //btnPlay.PerformClick();
                }
            }
            //////////////end here /////////////////////////////////////
        }
        private void GetResult()
        {
            convertedTEXT.Items.Clear();
            string urladdress = keywordstxt.Text;
            WebClient webc = new WebClient();
            string page = webc.DownloadString("http://www.bing.com/search?q=" + urladdress);
            //string news = "<span name=\"desc\" class=\"snippet\">(.*?)</span>";
            //string news = "<span class=\"st\">(.*?)</span>";
            //< span class="snippet" data-tag="" name="desc">
            string news = "<div class=\"b_snippet\">(.*?)</div>";
            news = "<div class=\"b_attribution\">(.*?)</div>";
            news = "<p>(.*?)</p>";

            foreach (Match match in Regex.Matches(page, news))
                convertedTEXT.Items.Add(match.Groups[1].Value.Replace("<strong>", " ").Replace("</strong>", " ").Replace("&", " ").Replace("#", ""));
            foreach (string s in convertedTEXT.Items)
            {
                Marvel.SpeakAsync(s);

            }
        }
        private void btnback_Click(object sender, EventArgs e)
        {
            webreaderbrow.GoBack();
        }

        private void btnforward_Click(object sender, EventArgs e)
        {
            webreaderbrow.GoForward();
        }
        private void webreaderbrow_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            keywordstxt.Text = webreaderbrow.Url.ToString();
        }
        private void Navigate(String address)
        {
            if (String.IsNullOrEmpty(address)) return;
            if (address.Equals("about:blank")) return;
            if (!address.StartsWith("http://www.bing.com/search?q=/") &&
                !address.StartsWith("https://www.bing.com/search?q=/"))
            {
                address = "https://www.bing.com/search?q=/" + address;
            }
            try
            {
                webreaderbrow.Navigate(new Uri(address));
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }
        private void keywordstxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Navigate(keywordstxt.Text);
            }
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
