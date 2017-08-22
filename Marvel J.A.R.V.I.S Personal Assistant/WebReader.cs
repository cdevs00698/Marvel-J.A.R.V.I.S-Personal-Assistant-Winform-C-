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
using Marvel_J.A.R.V.I.S_Personal_Assistant.Properties;

namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    public partial class WebReader : Form
    {
        int Top;
        int MoveX;
        int MoveY;
        SpeechRecognitionEngine speechRecognitionEngine = null;
        SpeechSynthesizer Marvel = new SpeechSynthesizer();
        Grammar webcommandgrammar;
        bool flag = false;
        int i = 0;
        String[] ArrayWebCommands;
        String[] ArrayWebResponse;
        public static String userName = Environment.UserName;
        StreamWriter sw;

        public WebReader()
        {
            InitializeComponent();
            try
            {
                speechRecognitionEngine = createSpeechEngine("en-US");
                speechRecognitionEngine.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(Environment.CurrentDirectory + "\\websitereader.txt")))));
                speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Web_SpeechRecognized);
                speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);
                // hook to events
                //speechRecognitionEngine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(engine_AudioLevelUpdated);

                // load dictionary
                //loadGrammarAndCommands();

                // use the system's default microphone
                speechRecognitionEngine.SetInputToDefaultAudioDevice();
                // start listening
                speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                Marvel.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(Marvel_SpeakCompleted);

                MarvelJPA.webcpath = Properties.Settings.Default.WebC;
                MarvelJPA.webrespath = Properties.Settings.Default.WebR;
                if (!File.Exists(MarvelJPA.webcpath))
                {
                    sw = File.CreateText(MarvelJPA.webcpath); sw.Write("Open Google"); sw.Close();
                }
                if (!File.Exists(MarvelJPA.webrespath))
                {
                    sw = File.CreateText(MarvelJPA.webrespath); sw.Write("Very well"); sw.Close();
                }
                ArrayWebCommands = File.ReadAllLines(MarvelJPA.webcpath); //This loads all written commands in our Custom Commands text documents into arrays so they can be loaded into our grammars
                ArrayWebResponse = File.ReadAllLines(MarvelJPA.webrespath);
                loadGrammarAndCommands();
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
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\websitereader.txt");
                texts.Add(lines);
                Grammar wordsList = new Grammar(new GrammarBuilder(texts));
                speechRecognitionEngine.LoadGrammar(wordsList);
                try
                {
                    webcommandgrammar = new Grammar(new GrammarBuilder(new Choices(ArrayWebCommands)));
                    speechRecognitionEngine.LoadGrammarAsync(webcommandgrammar);
                }
                catch
                { Marvel.SpeakAsync("I've detected an in valid entry in your web commands, possibly a blank line. web commands will cease to work until it is fixed."); }
            }
            catch (Exception ex)
            {
                Marvel.SpeakAsync("I've detected an in valid entry in your web commands, possibly a blank line. Web commands will cease to work until it is fixed." + ex);
            }
        }
        #region speechEngine events
        /// <summary>
        /// New event handler start here 
        /// 
        private void Web_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;

            i = 0;
            try
            {
                foreach (string line in ArrayWebCommands)
                {
                    if (line == speech)
                    {
                        Marvel.SpeakAsync(ArrayWebResponse[i]);
                        inputurltxt.Text = ArrayWebResponse[i];
                        searchbtn.PerformClick();
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
                case "convert text to audio":
                    btnDownload.PerformClick();
                    break;
                case "close website reader":
                    closebtn.PerformClick();
                    break;
                case "hide website reader":
                    closebtn.PerformClick();
                    break;
                case "show website reader":
                case "show website reader again":
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
            Marvel.SpeakAsync(convertedtxt.Text);
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
        private void WebReader_Load(object sender, EventArgs e)
        {



            convertedtxt.Text = string.Empty;
            labelnet.Text = NetworkInterface.GetIsNetworkAvailable().ToString();
            if (labelnet.Text != "True")
            {
                Marvel.Speak("Please check your internet connection, before the web reader work properly");
                this.Close();
            }
            else
            {
                Marvel.Speak("i am ready, what would you like to compile");
            }
            ////////////////////////commands ///////////////////////////////////
            //////////////here voice search ////////////////////////////

            ////////////////////////end here ///////////////////////////////////
            convertedtxt.AllowDrop = true;

            convertedtxt.DragEnter += new DragEventHandler(textdrop_DragEnter);

            convertedtxt.DragDrop += new DragEventHandler(textdrop_DragDrop);
            // Start listening for clipboard changes

        }

        private void textdrop_DragEnter(object sender, DragEventArgs e)

        {

            e.Effect = DragDropEffects.Copy;
        }
        private void textdrop_DragDrop(object sender, DragEventArgs e)

        {


            if (convertedtxt.Text != null)
            {
                convertedtxt.Clear();
                convertedtxt.Text = e.Data.GetData(typeof(string)).ToString(); //Get the Text From Data
                btnPlay.PerformClick();
            }
            else
            {
                convertedtxt.Text = e.Data.GetData(typeof(string)).ToString();
                btnPlay.PerformClick();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {


        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            convertedtxt.Clear();
            webreaderbrow.Navigate("https://en.wikipedia.org/wiki/" + inputurltxt.Text);
            string urlAddress = inputurltxt.Text;
            var Webclient = new WebClient();
            var pagesource = Webclient.DownloadString("http://en.wikipedia.org/w/api.php?format=xml&action=query&prop=extracts&titles=" + urlAddress + "&redirects=true");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(pagesource);

            var fnode = doc.GetElementsByTagName("extract")[0];

            try
            {

                string ss = fnode.InnerText;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                string.Format("Before:{0}", ss);
                ss = regex.Replace(ss, string.Empty);
                string result = String.Format(ss);
                convertedtxt.Text += result;
                btnPlay.PerformClick();
            }
            catch (Exception ex)
            {
                Marvel.Speak(ex.Message);
            }
            //////////////here voice search ////////////////////////////
            //SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
            //Grammar dictationGrammar = new DictationGrammar();
            //recognizer.LoadGrammar(dictationGrammar);
            //try
            //{
            //    recognizer.SetInputToDefaultAudioDevice();
            //    RecognitionResult result = recognizer.Recognize();

            //    try
            //    {

            //        ////Pass the filepath and filename to the StreamWriter Constructor
            //        //StreamWriter sw = new StreamWriter(inputtxt.Text);

            //        ////Write a line of text

            //        //sw.WriteLine(inputtxt.Text);
            //        inputurltxt.Text = result.Text;
            //        inputurltxt.Update();
            //        //Write a second line of text
            //        //sw.WriteLine("From the StreamWriter class");

            //        //Close the file
            //        //sw.Close();
            //    }
            //    catch (Exception)
            //    {
            //        //computer.Speak("Searching");
            //    }
            //    finally
            //    {
            //        computer.Speak("Searching ");
            //        //compwrods.Text += "Live: " + "Your name is added Successfully";
            //    }

            //}
            //catch (InvalidOperationException)

            //{
            //    computer.Speak("Could not recognize input from default aduio device. Is a microphone or sound card available");
            //}
            //finally
            //{
            //    recognizer.UnloadAllGrammars();
            //}
            ///////////////////////////////////////////////////////
            ////Thread.Sleep(1000);
            ////htmltagbox.Clear();
            ////convertedtxt.Clear();
            //string urlAddress = inputurltxt.Text;
            ////////////search engine //////////////////////////
            /////webreaderbrow.Navigate("https://en.wikipedia.org/wiki/" + urlAddress);

            /////////////////////////end here //////////////////////

            //webreaderbrow.Navigate("https://en.wikipedia.org/wiki/"+ urlAddress);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://en.wikipedia.org/wiki/" + urlAddress);
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            //    Stream receiveStream = response.GetResponseStream();
            //    StreamReader readStream = null;

            //    if (response.CharacterSet == null)
            //    {
            //        readStream = new StreamReader(receiveStream);
            //    }
            //    else
            //    {
            //        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
            //    }

            //    string data = readStream.ReadToEnd();

            //    response.Close();
            //    readStream.Close();
            //    htmltagbox.Text = data;
            //    HtmlToText convert = new HtmlToText();
            //    convertedtxt.Text = convert.Convert(htmltagbox.Text);
            //}
        }

        private void inputurltxt_TextChanged(object sender, EventArgs e)
        {

        }


        private void webreaderbrow_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void convertedtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

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

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Top == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MoveX, MousePosition.Y - MoveY);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Top = 1;
            MoveX = e.X;
            MoveY = e.Y;
        }
    }
}
#endregion
