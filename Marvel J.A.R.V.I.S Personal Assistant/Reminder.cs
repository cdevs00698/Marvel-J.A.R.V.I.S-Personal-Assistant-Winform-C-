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
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Xml;
using System.Media;
using System.Threading;
using System.Globalization;
using System.Net;

namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    public partial class Reminder : Form
    {
        int Top;
        int MoveX;
        int MoveY;
        SpeechRecognitionEngine speechRecognitionEngine = null;
        SpeechSynthesizer Marvel = new SpeechSynthesizer();
        string title;
        string cdata;
        string temp;
        string condition;
        string high;
        string low;
        string humidity;
        string sunrise;
        string sunset;
        string windspeed;
        string userName = Environment.UserName;
        public Reminder()
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

                UnloadGrammarAndCommands();
                // use the system's default microphone
                speechRecognitionEngine.SetInputToDefaultAudioDevice();

                // start listening
                speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                Marvel.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(computer_SpeakCompleted);
                if (Marvel.State == SynthesizerState.Speaking)
                    Marvel.SpeakAsyncCancelAll();
                if (autoselectvoice.Text != "IVONA 2 Brian OEM")
                {
                    autoselectvoice.SelectedItem = "IVONA 2 Brian OEM";
                    Marvel.SelectVoice("IVONA 2 Brian OEM");

                }
                else
                {
                    autoselectvoice.SelectedItem = "Microsoft David Desktop";
                    Marvel.SelectVoice("Microsoft David Desktop");
                }
            }
            catch (Exception)
            {
                Marvel.SpeakAsync("");
            }
        }
        void computer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            //btnPlay.Enabled = true;
            //btnPause.Enabled = false;
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
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\timesetcommands.txt");
                foreach (string line in lines)
                {
                    // skip commentblocks and empty lines..
                    if (line.StartsWith("--") || line == String.Empty) continue;

                    // split the line
                    //var parts = line.Split(new char[] { '|' });

                    // add commandItem to the list for later lookup or execution
                    //words.Add(new Word() { Text = parts[0], AttachedText = parts[1], IsShellCommand = (parts[2] == "true") });

                    // add the text to the known choices of speechengine
                    texts.Add(line);
                }
                Grammar wordsList = new Grammar(new GrammarBuilder(texts));
                speechRecognitionEngine.LoadGrammar(wordsList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void UnloadGrammarAndCommands()
        {
            try
            {
                Choices textz = new Choices();
                string[] linez = File.ReadAllLines(Environment.CurrentDirectory + "\\timesetcommandsforremd.txt");
                textz.Add(linez);
                Grammar wordsListz = new Grammar(new GrammarBuilder(textz));
                speechRecognitionEngine.LoadGrammar(wordsListz);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void load()
        {
            speechRecognitionEngine.UnloadAllGrammars();
            loadGrammarAndCommands();
        }
        #region speechEngine events
        /// <summary>
        /// Handles the SpeechRecognized event of the engine control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Speech.Recognition.SpeechRecognizedEventArgs"/> instance containing the event data.</param>
        void engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            System.DayOfWeek today = System.DateTime.Today.DayOfWeek;
            //scvText.ScrollToEnd();
            string speech = (e.Result.Text);
            switch (speech)
            {
                //GREETINGS
                case "minimize":
                case "hide alarmr":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Minimized;
                    TopMost = false;
                    break;
                case "show alarm again":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Normal;
                    TopMost = true;
                    break;
                case "set alarm":
                case "fix alarm":
                    Marvel.SpeakAsync("ok, at which time");
                    btnset.Enabled = true;
                    break;
                case "stop alarm music":
                case "stop alarm clock":
                case "stop music":
                case "stop the song":
                    btnstop.PerformClick();
                    break;
                case "close alarm mode":
                case "close alarm":
                    closebtn.PerformClick();
                    break;
                case "i want to reset alarm":
                case "i want to change alarm date":
                    resetbtn.PerformClick();
                    break;
                case "fix alarm for tomorrow morning":
                    System.DateTime morning = System.DateTime.Now;
                    morning = morning.AddDays(1);
                    daysofweek.Text = morning.ToString("MM dd yyyy");
                    Marvel.Speak("Ok at which time");
                    break;
                case "fix alarm for tomorrow evening":
                    System.DateTime evening = System.DateTime.Now;
                    evening = evening.AddDays(1);
                    daysofweek.Text = evening.ToString("MM dd yyyy");
                    Marvel.Speak("Ok at which time");
                    break;
                case "fix alarm for monday":
                    if (today == DayOfWeek.Monday) { daysofweek.Text = today.ToString(); } else { daysofweek.Text = DayOfWeek.Monday.ToString(); }
                    Marvel.Speak("Ok at which time");
                    load();
                    break;
                case "fix alarm for tuesday":
                    if (today == DayOfWeek.Monday) { daysofweek.Text = today.ToString(); } else { daysofweek.Text = DayOfWeek.Tuesday.ToString(); }
                    Marvel.Speak("Ok at which time");
                    load();
                    break;
                case "fix alarm for wednesday":
                    if (today == DayOfWeek.Monday) { daysofweek.Text = today.ToString(); } else { daysofweek.Text = DayOfWeek.Wednesday.ToString(); }
                    Marvel.Speak("Ok at which time");
                    load();
                    break;
                case "fix alarm for thursday":
                    if (today == DayOfWeek.Monday) { daysofweek.Text = today.ToString(); } else { daysofweek.Text = DayOfWeek.Thursday.ToString(); }
                    Marvel.Speak("Ok at which time");
                    load();
                    break;
                case "fix alarm for friday":
                    if (today == DayOfWeek.Monday) { daysofweek.Text = today.ToString(); } else { daysofweek.Text = DayOfWeek.Friday.ToString(); }
                    Marvel.Speak("Ok at which time");
                    load();
                    break;
                case "fix alarm for saturday":
                    if (today == DayOfWeek.Monday) { daysofweek.Text = today.ToString(); } else { daysofweek.Text = DayOfWeek.Saturday.ToString(); }
                    Marvel.Speak("Ok at which time");
                    load();
                    break;
                case "fix alarm for sunday":
                    if (today == DayOfWeek.Monday) { daysofweek.Text = today.ToString(); } else { daysofweek.Text = DayOfWeek.Sunday.ToString(); }
                    Marvel.Speak("Ok at which time");
                    load();
                    break;
                case "1 AM":
                case "1 1 AM":
                case "1 2 AM":
                case "1 3 AM":
                case "1 4 AM":
                case "1 5 AM":
                case "1 6 AM":
                case "1 7 AM":
                case "1 8 AM":
                case "1 9 AM":
                case "1 10 AM":
                case "1 11 AM":
                case "1 12 AM":
                case "1 13 AM":
                case "1 14 AM":
                case "1 15 AM":
                case "1 16 AM":
                case "1 17 AM":
                case "1 18 AM":
                case "1 19 AM":
                case "1 20 AM":
                case "1 21 AM":
                case "1 22 AM":
                case "1 23 AM":
                case "1 24 AM":
                case "1 25 AM":
                case "1 26 AM":
                case "1 27 AM":
                case "1 28 AM":
                case "1 29 AM":
                case "1 30 AM":
                case "1 31 AM":
                case "1 32 AM":
                case "1 33 AM":
                case "1 34 AM":
                case "1 35 AM":
                case "1 36 AM":
                case "1 37 AM":
                case "1 38 AM":
                case "1 39 AM":
                case "1 40 AM":
                case "1 41 AM":
                case "1 42 AM":
                case "1 43 AM":
                case "1 44 AM":
                case "1 45 AM":
                case "1 46 AM":
                case "1 47 AM":
                case "1 48 AM":
                case "1 49 AM":
                case "1 50 AM":
                case "1 51 AM":
                case "1 52 AM":
                case "1 53 AM":
                case "1 54 AM":
                case "1 55 AM":
                case "1 56 AM":
                case "1 57 AM":
                case "1 58 AM":
                case "1 59 AM":
                case "2 AM":
                case "2 1 AM":
                case "2 2 AM":
                case "2 3 AM":
                case "2 4 AM":
                case "2 5 AM":
                case "2 6 AM":
                case "2 7 AM":
                case "2 8 AM":
                case "2 9 AM":
                case "2 10 AM":
                case "2 11 AM":
                case "2 12 AM":
                case "2 13 AM":
                case "2 14 AM":
                case "2 15 AM":
                case "2 16 AM":
                case "2 17 AM":
                case "2 18 AM":
                case "2 19 AM":
                case "2 20 AM":
                case "2 21 AM":
                case "2 22 AM":
                case "2 23 AM":
                case "2 24 AM":
                case "2 25 AM":
                case "2 26 AM":
                case "2 27 AM":
                case "2 28 AM":
                case "2 29 AM":
                case "2 30 AM":
                case "2 31 AM":
                case "2 32 AM":
                case "2 33 AM":
                case "2 34 AM":
                case "2 35 AM":
                case "2 36 AM":
                case "2 37 AM":
                case "2 38 AM":
                case "2 39 AM":
                case "2 40 AM":
                case "2 41 AM":
                case "2 42 AM":
                case "2 43 AM":
                case "2 44 AM":
                case "2 45 AM":
                case "2 46 AM":
                case "2 47 AM":
                case "2 48 AM":
                case "2 49 AM":
                case "2 50 AM":
                case "2 51 AM":
                case "2 52 AM":
                case "2 53 AM":
                case "2 54 AM":
                case "2 55 AM":
                case "2 56 AM":
                case "2 57 AM":
                case "2 58 AM":
                case "2 59 AM":
                case "3 AM":
                case "3 1 AM":
                case "3 2 AM":
                case "3 3 AM":
                case "3 4 AM":
                case "3 5 AM":
                case "3 6 AM":
                case "3 7 AM":
                case "3 8 AM":
                case "3 9 AM":
                case "3 10 AM":
                case "3 11 AM":
                case "3 12 AM":
                case "3 13 AM":
                case "3 14 AM":
                case "3 15 AM":
                case "3 16 AM":
                case "3 17 AM":
                case "3 18 AM":
                case "3 19 AM":
                case "3 20 AM":
                case "3 21 AM":
                case "3 22 AM":
                case "3 23 AM":
                case "3 24 AM":
                case "3 25 AM":
                case "3 26 AM":
                case "3 27 AM":
                case "3 28 AM":
                case "3 29 AM":
                case "3 30 AM":
                case "3 31 AM":
                case "3 32 AM":
                case "3 33 AM":
                case "3 34 AM":
                case "3 35 AM":
                case "3 36 AM":
                case "3 37 AM":
                case "3 38 AM":
                case "3 39 AM":
                case "3 40 AM":
                case "3 41 AM":
                case "3 42 AM":
                case "3 43 AM":
                case "3 44 AM":
                case "3 45 AM":
                case "3 46 AM":
                case "3 47 AM":
                case "3 48 AM":
                case "3 49 AM":
                case "3 50 AM":
                case "3 51 AM":
                case "3 52 AM":
                case "3 53 AM":
                case "3 54 AM":
                case "3 55 AM":
                case "3 56 AM":
                case "3 57 AM":
                case "3 58 AM":
                case "3 59 AM":
                case "4 AM":
                case "4 1 AM":
                case "4 2 AM":
                case "4 3 AM":
                case "4 4 AM":
                case "4 5 AM":
                case "4 6 AM":
                case "4 7 AM":
                case "4 8 AM":
                case "4 9 AM":
                case "4 10 AM":
                case "4 11 AM":
                case "4 12 AM":
                case "4 13 AM":
                case "4 14 AM":
                case "4 15 AM":
                case "4 16 AM":
                case "4 17 AM":
                case "4 18 AM":
                case "4 19 AM":
                case "4 20 AM":
                case "4 21 AM":
                case "4 22 AM":
                case "4 23 AM":
                case "4 24 AM":
                case "4 25 AM":
                case "4 26 AM":
                case "4 27 AM":
                case "4 28 AM":
                case "4 29 AM":
                case "4 30 AM":
                case "4 31 AM":
                case "4 32 AM":
                case "4 33 AM":
                case "4 34 AM":
                case "4 35 AM":
                case "4 36 AM":
                case "4 37 AM":
                case "4 38 AM":
                case "4 39 AM":
                case "4 40 AM":
                case "4 41 AM":
                case "4 42 AM":
                case "4 43 AM":
                case "4 44 AM":
                case "4 45 AM":
                case "4 46 AM":
                case "4 47 AM":
                case "4 48 AM":
                case "4 49 AM":
                case "4 50 AM":
                case "4 51 AM":
                case "4 52 AM":
                case "4 53 AM":
                case "4 54 AM":
                case "4 55 AM":
                case "4 56 AM":
                case "4 57 AM":
                case "4 58 AM":
                case "4 59 AM":
                case "5 AM":
                case "5 1 AM":
                case "5 2 AM":
                case "5 3 AM":
                case "5 4 AM":
                case "5 5 AM":
                case "5 6 AM":
                case "5 7 AM":
                case "5 8 AM":
                case "5 9 AM":
                case "5 10 AM":
                case "5 11 AM":
                case "5 12 AM":
                case "5 13 AM":
                case "5 14 AM":
                case "5 15 AM":
                case "5 16 AM":
                case "5 17 AM":
                case "5 18 AM":
                case "5 19 AM":
                case "5 20 AM":
                case "5 21 AM":
                case "5 22 AM":
                case "5 23 AM":
                case "5 24 AM":
                case "5 25 AM":
                case "5 26 AM":
                case "5 27 AM":
                case "5 28 AM":
                case "5 29 AM":
                case "5 30 AM":
                case "5 31 AM":
                case "5 32 AM":
                case "5 33 AM":
                case "5 34 AM":
                case "5 35 AM":
                case "5 36 AM":
                case "5 37 AM":
                case "5 38 AM":
                case "5 39 AM":
                case "5 40 AM":
                case "5 41 AM":
                case "5 42 AM":
                case "5 43 AM":
                case "5 44 AM":
                case "5 45 AM":
                case "5 46 AM":
                case "5 47 AM":
                case "5 48 AM":
                case "5 49 AM":
                case "5 50 AM":
                case "5 51 AM":
                case "5 52 AM":
                case "5 53 AM":
                case "5 54 AM":
                case "5 55 AM":
                case "5 56 AM":
                case "5 57 AM":
                case "5 58 AM":
                case "5 59 AM":
                case "6 AM":
                case "6 1 AM":
                case "6 2 AM":
                case "6 3 AM":
                case "6 4 AM":
                case "6 5 AM":
                case "6 6 AM":
                case "6 7 AM":
                case "6 8 AM":
                case "6 9 AM":
                case "6 10 AM":
                case "6 11 AM":
                case "6 12 AM":
                case "6 13 AM":
                case "6 14 AM":
                case "6 15 AM":
                case "6 16 AM":
                case "6 17 AM":
                case "6 18 AM":
                case "6 19 AM":
                case "6 20 AM":
                case "6 21 AM":
                case "6 22 AM":
                case "6 23 AM":
                case "6 24 AM":
                case "6 25 AM":
                case "6 26 AM":
                case "6 27 AM":
                case "6 28 AM":
                case "6 29 AM":
                case "6 30 AM":
                case "6 31 AM":
                case "6 32 AM":
                case "6 33 AM":
                case "6 34 AM":
                case "6 35 AM":
                case "6 36 AM":
                case "6 37 AM":
                case "6 38 AM":
                case "6 39 AM":
                case "6 40 AM":
                case "6 41 AM":
                case "6 42 AM":
                case "6 43 AM":
                case "6 44 AM":
                case "6 45 AM":
                case "6 46 AM":
                case "6 47 AM":
                case "6 48 AM":
                case "6 49 AM":
                case "6 50 AM":
                case "6 51 AM":
                case "6 52 AM":
                case "6 53 AM":
                case "6 54 AM":
                case "6 55 AM":
                case "6 56 AM":
                case "6 57 AM":
                case "6 58 AM":
                case "6 59 AM":
                case "7 AM":
                case "7 1 AM":
                case "7 2 AM":
                case "7 3 AM":
                case "7 4 AM":
                case "7 5 AM":
                case "7 6 AM":
                case "7 7 AM":
                case "7 8 AM":
                case "7 9 AM":
                case "7 10 AM":
                case "7 11 AM":
                case "7 12 AM":
                case "7 13 AM":
                case "7 14 AM":
                case "7 15 AM":
                case "7 16 AM":
                case "7 17 AM":
                case "7 18 AM":
                case "7 19 AM":
                case "7 20 AM":
                case "7 21 AM":
                case "7 22 AM":
                case "7 23 AM":
                case "7 24 AM":
                case "7 25 AM":
                case "7 26 AM":
                case "7 27 AM":
                case "7 28 AM":
                case "7 29 AM":
                case "7 30 AM":
                case "7 31 AM":
                case "7 32 AM":
                case "7 33 AM":
                case "7 34 AM":
                case "7 35 AM":
                case "7 36 AM":
                case "7 37 AM":
                case "7 38 AM":
                case "7 39 AM":
                case "7 40 AM":
                case "7 41 AM":
                case "7 42 AM":
                case "7 43 AM":
                case "7 44 AM":
                case "7 45 AM":
                case "7 46 AM":
                case "7 47 AM":
                case "7 48 AM":
                case "7 49 AM":
                case "7 50 AM":
                case "7 51 AM":
                case "7 52 AM":
                case "7 53 AM":
                case "7 54 AM":
                case "7 55 AM":
                case "7 56 AM":
                case "7 57 AM":
                case "7 58 AM":
                case "7 59 AM":
                case "8 AM":
                case "8 1 AM":
                case "8 2 AM":
                case "8 3 AM":
                case "8 4 AM":
                case "8 5 AM":
                case "8 6 AM":
                case "8 7 AM":
                case "8 8 AM":
                case "8 9 AM":
                case "8 10 AM":
                case "8 11 AM":
                case "8 12 AM":
                case "8 13 AM":
                case "8 14 AM":
                case "8 15 AM":
                case "8 16 AM":
                case "8 17 AM":
                case "8 18 AM":
                case "8 19 AM":
                case "8 20 AM":
                case "8 21 AM":
                case "8 22 AM":
                case "8 23 AM":
                case "8 24 AM":
                case "8 25 AM":
                case "8 26 AM":
                case "8 27 AM":
                case "8 28 AM":
                case "8 29 AM":
                case "8 30 AM":
                case "8 31 AM":
                case "8 32 AM":
                case "8 33 AM":
                case "8 34 AM":
                case "8 35 AM":
                case "8 36 AM":
                case "8 37 AM":
                case "8 38 AM":
                case "8 39 AM":
                case "8 40 AM":
                case "8 41 AM":
                case "8 42 AM":
                case "8 43 AM":
                case "8 44 AM":
                case "8 45 AM":
                case "8 46 AM":
                case "8 47 AM":
                case "8 48 AM":
                case "8 49 AM":
                case "8 50 AM":
                case "8 51 AM":
                case "8 52 AM":
                case "8 53 AM":
                case "8 54 AM":
                case "8 55 AM":
                case "8 56 AM":
                case "8 57 AM":
                case "8 58 AM":
                case "8 59 AM":
                case "9 AM":
                case "9 1 AM":
                case "9 2 AM":
                case "9 3 AM":
                case "9 4 AM":
                case "9 5 AM":
                case "9 6 AM":
                case "9 7 AM":
                case "9 8 AM":
                case "9 9 AM":
                case "9 10 AM":
                case "9 11 AM":
                case "9 12 AM":
                case "9 13 AM":
                case "9 14 AM":
                case "9 15 AM":
                case "9 16 AM":
                case "9 17 AM":
                case "9 18 AM":
                case "9 19 AM":
                case "9 20 AM":
                case "9 21 AM":
                case "9 22 AM":
                case "9 23 AM":
                case "9 24 AM":
                case "9 25 AM":
                case "9 26 AM":
                case "9 27 AM":
                case "9 28 AM":
                case "9 29 AM":
                case "9 30 AM":
                case "9 31 AM":
                case "9 32 AM":
                case "9 33 AM":
                case "9 34 AM":
                case "9 35 AM":
                case "9 36 AM":
                case "9 37 AM":
                case "9 38 AM":
                case "9 39 AM":
                case "9 40 AM":
                case "9 41 AM":
                case "9 42 AM":
                case "9 43 AM":
                case "9 44 AM":
                case "9 45 AM":
                case "9 46 AM":
                case "9 47 AM":
                case "9 48 AM":
                case "9 49 AM":
                case "9 50 AM":
                case "9 51 AM":
                case "9 52 AM":
                case "9 53 AM":
                case "9 54 AM":
                case "9 55 AM":
                case "9 56 AM":
                case "9 57 AM":
                case "9 58 AM":
                case "9 59 AM":
                case "10 AM":
                case "10 1 AM":
                case "10 2 AM":
                case "10 3 AM":
                case "10 4 AM":
                case "10 5 AM":
                case "10 6 AM":
                case "10 7 AM":
                case "10 8 AM":
                case "10 9 AM":
                case "10 10 AM":
                case "10 11 AM":
                case "10 12 AM":
                case "10 13 AM":
                case "10 14 AM":
                case "10 15 AM":
                case "10 16 AM":
                case "10 17 AM":
                case "10 18 AM":
                case "10 19 AM":
                case "10 20 AM":
                case "10 21 AM":
                case "10 22 AM":
                case "10 23 AM":
                case "10 24 AM":
                case "10 25 AM":
                case "10 26 AM":
                case "10 27 AM":
                case "10 28 AM":
                case "10 29 AM":
                case "10 30 AM":
                case "10 31 AM":
                case "10 32 AM":
                case "10 33 AM":
                case "10 34 AM":
                case "10 35 AM":
                case "10 36 AM":
                case "10 37 AM":
                case "10 38 AM":
                case "10 39 AM":
                case "10 40 AM":
                case "10 41 AM":
                case "10 42 AM":
                case "10 43 AM":
                case "10 44 AM":
                case "10 45 AM":
                case "10 46 AM":
                case "10 47 AM":
                case "10 48 AM":
                case "10 49 AM":
                case "10 50 AM":
                case "10 51 AM":
                case "10 52 AM":
                case "10 53 AM":
                case "10 54 AM":
                case "10 55 AM":
                case "10 56 AM":
                case "10 57 AM":
                case "10 58 AM":
                case "10 59 AM":
                case "11 AM":
                case "11 1 AM":
                case "11 2 AM":
                case "11 3 AM":
                case "11 4 AM":
                case "11 5 AM":
                case "11 6 AM":
                case "11 7 AM":
                case "11 8 AM":
                case "11 9 AM":
                case "11 10 AM":
                case "11 11 AM":
                case "11 12 AM":
                case "11 13 AM":
                case "11 14 AM":
                case "11 15 AM":
                case "11 16 AM":
                case "11 17 AM":
                case "11 18 AM":
                case "11 19 AM":
                case "11 20 AM":
                case "11 21 AM":
                case "11 22 AM":
                case "11 23 AM":
                case "11 24 AM":
                case "11 25 AM":
                case "11 26 AM":
                case "11 27 AM":
                case "11 28 AM":
                case "11 29 AM":
                case "11 30 AM":
                case "11 31 AM":
                case "11 32 AM":
                case "11 33 AM":
                case "11 34 AM":
                case "11 35 AM":
                case "11 36 AM":
                case "11 37 AM":
                case "11 38 AM":
                case "11 39 AM":
                case "11 40 AM":
                case "11 41 AM":
                case "11 42 AM":
                case "11 43 AM":
                case "11 44 AM":
                case "11 45 AM":
                case "11 46 AM":
                case "11 47 AM":
                case "11 48 AM":
                case "11 49 AM":
                case "11 50 AM":
                case "11 51 AM":
                case "11 52 AM":
                case "11 53 AM":
                case "11 54 AM":
                case "11 55 AM":
                case "11 56 AM":
                case "11 57 AM":
                case "11 58 AM":
                case "11 59 AM":
                case "12 AM":
                case "12 1 AM":
                case "12 2 AM":
                case "12 3 AM":
                case "12 4 AM":
                case "12 5 AM":
                case "12 6 AM":
                case "12 7 AM":
                case "12 8 AM":
                case "12 9 AM":
                case "12 10 AM":
                case "12 11 AM":
                case "12 12 AM":
                case "12 13 AM":
                case "12 14 AM":
                case "12 15 AM":
                case "12 16 AM":
                case "12 17 AM":
                case "12 18 AM":
                case "12 19 AM":
                case "12 20 AM":
                case "12 21 AM":
                case "12 22 AM":
                case "12 23 AM":
                case "12 24 AM":
                case "12 25 AM":
                case "12 26 AM":
                case "12 27 AM":
                case "12 28 AM":
                case "12 29 AM":
                case "12 30 AM":
                case "12 31 AM":
                case "12 32 AM":
                case "12 33 AM":
                case "12 34 AM":
                case "12 35 AM":
                case "12 36 AM":
                case "12 37 AM":
                case "12 38 AM":
                case "12 39 AM":
                case "12 40 AM":
                case "12 41 AM":
                case "12 42 AM":
                case "12 43 AM":
                case "12 44 AM":
                case "12 45 AM":
                case "12 46 AM":
                case "12 47 AM":
                case "12 48 AM":
                case "12 49 AM":
                case "12 50 AM":
                case "12 51 AM":
                case "12 52 AM":
                case "12 53 AM":
                case "12 54 AM":
                case "12 55 AM":
                case "12 56 AM":
                case "12 57 AM":
                case "12 58 AM":
                case "12 59 AM":
                case "1 PM":
                case "1 1 PM":
                case "1 2 PM":
                case "1 3 PM":
                case "1 4 PM":
                case "1 5 PM":
                case "1 6 PM":
                case "1 7 PM":
                case "1 8 PM":
                case "1 9 PM":
                case "1 10 PM":
                case "1 11 PM":
                case "1 12 PM":
                case "1 13 PM":
                case "1 14 PM":
                case "1 15 PM":
                case "1 16 PM":
                case "1 17 PM":
                case "1 18 PM":
                case "1 19 PM":
                case "1 20 PM":
                case "1 21 PM":
                case "1 22 PM":
                case "1 23 PM":
                case "1 24 PM":
                case "1 25 PM":
                case "1 26 PM":
                case "1 27 PM":
                case "1 28 PM":
                case "1 29 PM":
                case "1 30 PM":
                case "1 31 PM":
                case "1 32 PM":
                case "1 33 PM":
                case "1 34 PM":
                case "1 35 PM":
                case "1 36 PM":
                case "1 37 PM":
                case "1 38 PM":
                case "1 39 PM":
                case "1 40 PM":
                case "1 41 PM":
                case "1 42 PM":
                case "1 43 PM":
                case "1 44 PM":
                case "1 45 PM":
                case "1 46 PM":
                case "1 47 PM":
                case "1 48 PM":
                case "1 49 PM":
                case "1 50 PM":
                case "1 51 PM":
                case "1 52 PM":
                case "1 53 PM":
                case "1 54 PM":
                case "1 55 PM":
                case "1 56 PM":
                case "1 57 PM":
                case "1 58 PM":
                case "1 59 PM":
                case "2 PM":
                case "2 1 PM":
                case "2 2 PM":
                case "2 3 PM":
                case "2 4 PM":
                case "2 5 PM":
                case "2 6 PM":
                case "2 7 PM":
                case "2 8 PM":
                case "2 9 PM":
                case "2 10 PM":
                case "2 11 PM":
                case "2 12 PM":
                case "2 13 PM":
                case "2 14 PM":
                case "2 15 PM":
                case "2 16 PM":
                case "2 17 PM":
                case "2 18 PM":
                case "2 19 PM":
                case "2 20 PM":
                case "2 21 PM":
                case "2 22 PM":
                case "2 23 PM":
                case "2 24 PM":
                case "2 25 PM":
                case "2 26 PM":
                case "2 27 PM":
                case "2 28 PM":
                case "2 29 PM":
                case "2 30 PM":
                case "2 31 PM":
                case "2 32 PM":
                case "2 33 PM":
                case "2 34 PM":
                case "2 35 PM":
                case "2 36 PM":
                case "2 37 PM":
                case "2 38 PM":
                case "2 39 PM":
                case "2 40 PM":
                case "2 41 PM":
                case "2 42 PM":
                case "2 43 PM":
                case "2 44 PM":
                case "2 45 PM":
                case "2 46 PM":
                case "2 47 PM":
                case "2 48 PM":
                case "2 49 PM":
                case "2 50 PM":
                case "2 51 PM":
                case "2 52 PM":
                case "2 53 PM":
                case "2 54 PM":
                case "2 55 PM":
                case "2 56 PM":
                case "2 57 PM":
                case "2 58 PM":
                case "2 59 PM":
                case "3 PM":
                case "3 1 PM":
                case "3 2 PM":
                case "3 3 PM":
                case "3 4 PM":
                case "3 5 PM":
                case "3 6 PM":
                case "3 7 PM":
                case "3 8 PM":
                case "3 9 PM":
                case "3 10 PM":
                case "3 11 PM":
                case "3 12 PM":
                case "3 13 PM":
                case "3 14 PM":
                case "3 15 PM":
                case "3 16 PM":
                case "3 17 PM":
                case "3 18 PM":
                case "3 19 PM":
                case "3 20 PM":
                case "3 21 PM":
                case "3 22 PM":
                case "3 23 PM":
                case "3 24 PM":
                case "3 25 PM":
                case "3 26 PM":
                case "3 27 PM":
                case "3 28 PM":
                case "3 29 PM":
                case "3 30 PM":
                case "3 31 PM":
                case "3 32 PM":
                case "3 33 PM":
                case "3 34 PM":
                case "3 35 PM":
                case "3 36 PM":
                case "3 37 PM":
                case "3 38 PM":
                case "3 39 PM":
                case "3 40 PM":
                case "3 41 PM":
                case "3 42 PM":
                case "3 43 PM":
                case "3 44 PM":
                case "3 45 PM":
                case "3 46 PM":
                case "3 47 PM":
                case "3 48 PM":
                case "3 49 PM":
                case "3 50 PM":
                case "3 51 PM":
                case "3 52 PM":
                case "3 53 PM":
                case "3 54 PM":
                case "3 55 PM":
                case "3 56 PM":
                case "3 57 PM":
                case "3 58 PM":
                case "3 59 PM":
                case "4 PM":
                case "4 1 PM":
                case "4 2 PM":
                case "4 3 PM":
                case "4 4 PM":
                case "4 5 PM":
                case "4 6 PM":
                case "4 7 PM":
                case "4 8 PM":
                case "4 9 PM":
                case "4 10 PM":
                case "4 11 PM":
                case "4 12 PM":
                case "4 13 PM":
                case "4 14 PM":
                case "4 15 PM":
                case "4 16 PM":
                case "4 17 PM":
                case "4 18 PM":
                case "4 19 PM":
                case "4 20 PM":
                case "4 21 PM":
                case "4 22 PM":
                case "4 23 PM":
                case "4 24 PM":
                case "4 25 PM":
                case "4 26 PM":
                case "4 27 PM":
                case "4 28 PM":
                case "4 29 PM":
                case "4 30 PM":
                case "4 31 PM":
                case "4 32 PM":
                case "4 33 PM":
                case "4 34 PM":
                case "4 35 PM":
                case "4 36 PM":
                case "4 37 PM":
                case "4 38 PM":
                case "4 39 PM":
                case "4 40 PM":
                case "4 41 PM":
                case "4 42 PM":
                case "4 43 PM":
                case "4 44 PM":
                case "4 45 PM":
                case "4 46 PM":
                case "4 47 PM":
                case "4 48 PM":
                case "4 49 PM":
                case "4 50 PM":
                case "4 51 PM":
                case "4 52 PM":
                case "4 53 PM":
                case "4 54 PM":
                case "4 55 PM":
                case "4 56 PM":
                case "4 57 PM":
                case "4 58 PM":
                case "4 59 PM":
                case "5 PM":
                case "5 1 PM":
                case "5 2 PM":
                case "5 3 PM":
                case "5 4 PM":
                case "5 5 PM":
                case "5 6 PM":
                case "5 7 PM":
                case "5 8 PM":
                case "5 9 PM":
                case "5 10 PM":
                case "5 11 PM":
                case "5 12 PM":
                case "5 13 PM":
                case "5 14 PM":
                case "5 15 PM":
                case "5 16 PM":
                case "5 17 PM":
                case "5 18 PM":
                case "5 19 PM":
                case "5 20 PM":
                case "5 21 PM":
                case "5 22 PM":
                case "5 23 PM":
                case "5 24 PM":
                case "5 25 PM":
                case "5 26 PM":
                case "5 27 PM":
                case "5 28 PM":
                case "5 29 PM":
                case "5 30 PM":
                case "5 31 PM":
                case "5 32 PM":
                case "5 33 PM":
                case "5 34 PM":
                case "5 35 PM":
                case "5 36 PM":
                case "5 37 PM":
                case "5 38 PM":
                case "5 39 PM":
                case "5 40 PM":
                case "5 41 PM":
                case "5 42 PM":
                case "5 43 PM":
                case "5 44 PM":
                case "5 45 PM":
                case "5 46 PM":
                case "5 47 PM":
                case "5 48 PM":
                case "5 49 PM":
                case "5 50 PM":
                case "5 51 PM":
                case "5 52 PM":
                case "5 53 PM":
                case "5 54 PM":
                case "5 55 PM":
                case "5 56 PM":
                case "5 57 PM":
                case "5 58 PM":
                case "5 59 PM":
                case "6 PM":
                case "6 1 PM":
                case "6 2 PM":
                case "6 3 PM":
                case "6 4 PM":
                case "6 5 PM":
                case "6 6 PM":
                case "6 7 PM":
                case "6 8 PM":
                case "6 9 PM":
                case "6 10 PM":
                case "6 11 PM":
                case "6 12 PM":
                case "6 13 PM":
                case "6 14 PM":
                case "6 15 PM":
                case "6 16 PM":
                case "6 17 PM":
                case "6 18 PM":
                case "6 19 PM":
                case "6 20 PM":
                case "6 21 PM":
                case "6 22 PM":
                case "6 23 PM":
                case "6 24 PM":
                case "6 25 PM":
                case "6 26 PM":
                case "6 27 PM":
                case "6 28 PM":
                case "6 29 PM":
                case "6 30 PM":
                case "6 31 PM":
                case "6 32 PM":
                case "6 33 PM":
                case "6 34 PM":
                case "6 35 PM":
                case "6 36 PM":
                case "6 37 PM":
                case "6 38 PM":
                case "6 39 PM":
                case "6 40 PM":
                case "6 41 PM":
                case "6 42 PM":
                case "6 43 PM":
                case "6 44 PM":
                case "6 45 PM":
                case "6 46 PM":
                case "6 47 PM":
                case "6 48 PM":
                case "6 49 PM":
                case "6 50 PM":
                case "6 51 PM":
                case "6 52 PM":
                case "6 53 PM":
                case "6 54 PM":
                case "6 55 PM":
                case "6 56 PM":
                case "6 57 PM":
                case "6 58 PM":
                case "6 59 PM":
                case "7 PM":
                case "7 1 PM":
                case "7 2 PM":
                case "7 3 PM":
                case "7 4 PM":
                case "7 5 PM":
                case "7 6 PM":
                case "7 7 PM":
                case "7 8 PM":
                case "7 9 PM":
                case "7 10 PM":
                case "7 11 PM":
                case "7 12 PM":
                case "7 13 PM":
                case "7 14 PM":
                case "7 15 PM":
                case "7 16 PM":
                case "7 17 PM":
                case "7 18 PM":
                case "7 19 PM":
                case "7 20 PM":
                case "7 21 PM":
                case "7 22 PM":
                case "7 23 PM":
                case "7 24 PM":
                case "7 25 PM":
                case "7 26 PM":
                case "7 27 PM":
                case "7 28 PM":
                case "7 29 PM":
                case "7 30 PM":
                case "7 31 PM":
                case "7 32 PM":
                case "7 33 PM":
                case "7 34 PM":
                case "7 35 PM":
                case "7 36 PM":
                case "7 37 PM":
                case "7 38 PM":
                case "7 39 PM":
                case "7 40 PM":
                case "7 41 PM":
                case "7 42 PM":
                case "7 43 PM":
                case "7 44 PM":
                case "7 45 PM":
                case "7 46 PM":
                case "7 47 PM":
                case "7 48 PM":
                case "7 49 PM":
                case "7 50 PM":
                case "7 51 PM":
                case "7 52 PM":
                case "7 53 PM":
                case "7 54 PM":
                case "7 55 PM":
                case "7 56 PM":
                case "7 57 PM":
                case "7 58 PM":
                case "7 59 PM":
                case "8 PM":
                case "8 1 PM":
                case "8 2 PM":
                case "8 3 PM":
                case "8 4 PM":
                case "8 5 PM":
                case "8 6 PM":
                case "8 7 PM":
                case "8 8 PM":
                case "8 9 PM":
                case "8 10 PM":
                case "8 11 PM":
                case "8 12 PM":
                case "8 13 PM":
                case "8 14 PM":
                case "8 15 PM":
                case "8 16 PM":
                case "8 17 PM":
                case "8 18 PM":
                case "8 19 PM":
                case "8 20 PM":
                case "8 21 PM":
                case "8 22 PM":
                case "8 23 PM":
                case "8 24 PM":
                case "8 25 PM":
                case "8 26 PM":
                case "8 27 PM":
                case "8 28 PM":
                case "8 29 PM":
                case "8 30 PM":
                case "8 31 PM":
                case "8 32 PM":
                case "8 33 PM":
                case "8 34 PM":
                case "8 35 PM":
                case "8 36 PM":
                case "8 37 PM":
                case "8 38 PM":
                case "8 39 PM":
                case "8 40 PM":
                case "8 41 PM":
                case "8 42 PM":
                case "8 43 PM":
                case "8 44 PM":
                case "8 45 PM":
                case "8 46 PM":
                case "8 47 PM":
                case "8 48 PM":
                case "8 49 PM":
                case "8 50 PM":
                case "8 51 PM":
                case "8 52 PM":
                case "8 53 PM":
                case "8 54 PM":
                case "8 55 PM":
                case "8 56 PM":
                case "8 57 PM":
                case "8 58 PM":
                case "8 59 PM":
                case "9 PM":
                case "9 1 PM":
                case "9 2 PM":
                case "9 3 PM":
                case "9 4 PM":
                case "9 5 PM":
                case "9 6 PM":
                case "9 7 PM":
                case "9 8 PM":
                case "9 9 PM":
                case "9 10 PM":
                case "9 11 PM":
                case "9 12 PM":
                case "9 13 PM":
                case "9 14 PM":
                case "9 15 PM":
                case "9 16 PM":
                case "9 17 PM":
                case "9 18 PM":
                case "9 19 PM":
                case "9 20 PM":
                case "9 21 PM":
                case "9 22 PM":
                case "9 23 PM":
                case "9 24 PM":
                case "9 25 PM":
                case "9 26 PM":
                case "9 27 PM":
                case "9 28 PM":
                case "9 29 PM":
                case "9 30 PM":
                case "9 31 PM":
                case "9 32 PM":
                case "9 33 PM":
                case "9 34 PM":
                case "9 35 PM":
                case "9 36 PM":
                case "9 37 PM":
                case "9 38 PM":
                case "9 39 PM":
                case "9 40 PM":
                case "9 41 PM":
                case "9 42 PM":
                case "9 43 PM":
                case "9 44 PM":
                case "9 45 PM":
                case "9 46 PM":
                case "9 47 PM":
                case "9 48 PM":
                case "9 49 PM":
                case "9 50 PM":
                case "9 51 PM":
                case "9 52 PM":
                case "9 53 PM":
                case "9 54 PM":
                case "9 55 PM":
                case "9 56 PM":
                case "9 57 PM":
                case "9 58 PM":
                case "9 59 PM":
                case "10 PM":
                case "10 1 PM":
                case "10 2 PM":
                case "10 3 PM":
                case "10 4 PM":
                case "10 5 PM":
                case "10 6 PM":
                case "10 7 PM":
                case "10 8 PM":
                case "10 9 PM":
                case "10 10 PM":
                case "10 11 PM":
                case "10 12 PM":
                case "10 13 PM":
                case "10 14 PM":
                case "10 15 PM":
                case "10 16 PM":
                case "10 17 PM":
                case "10 18 PM":
                case "10 19 PM":
                case "10 20 PM":
                case "10 21 PM":
                case "10 22 PM":
                case "10 23 PM":
                case "10 24 PM":
                case "10 25 PM":
                case "10 26 PM":
                case "10 27 PM":
                case "10 28 PM":
                case "10 29 PM":
                case "10 30 PM":
                case "10 31 PM":
                case "10 32 PM":
                case "10 33 PM":
                case "10 34 PM":
                case "10 35 PM":
                case "10 36 PM":
                case "10 37 PM":
                case "10 38 PM":
                case "10 39 PM":
                case "10 40 PM":
                case "10 41 PM":
                case "10 42 PM":
                case "10 43 PM":
                case "10 44 PM":
                case "10 45 PM":
                case "10 46 PM":
                case "10 47 PM":
                case "10 48 PM":
                case "10 49 PM":
                case "10 50 PM":
                case "10 51 PM":
                case "10 52 PM":
                case "10 53 PM":
                case "10 54 PM":
                case "10 55 PM":
                case "10 56 PM":
                case "10 57 PM":
                case "10 58 PM":
                case "10 59 PM":
                case "11 PM":
                case "11 1 PM":
                case "11 2 PM":
                case "11 3 PM":
                case "11 4 PM":
                case "11 5 PM":
                case "11 6 PM":
                case "11 7 PM":
                case "11 8 PM":
                case "11 9 PM":
                case "11 10 PM":
                case "11 11 PM":
                case "11 12 PM":
                case "11 13 PM":
                case "11 14 PM":
                case "11 15 PM":
                case "11 16 PM":
                case "11 17 PM":
                case "11 18 PM":
                case "11 19 PM":
                case "11 20 PM":
                case "11 21 PM":
                case "11 22 PM":
                case "11 23 PM":
                case "11 24 PM":
                case "11 25 PM":
                case "11 26 PM":
                case "11 27 PM":
                case "11 28 PM":
                case "11 29 PM":
                case "11 30 PM":
                case "11 31 PM":
                case "11 32 PM":
                case "11 33 PM":
                case "11 34 PM":
                case "11 35 PM":
                case "11 36 PM":
                case "11 37 PM":
                case "11 38 PM":
                case "11 39 PM":
                case "11 40 PM":
                case "11 41 PM":
                case "11 42 PM":
                case "11 43 PM":
                case "11 44 PM":
                case "11 45 PM":
                case "11 46 PM":
                case "11 47 PM":
                case "11 48 PM":
                case "11 49 PM":
                case "11 50 PM":
                case "11 51 PM":
                case "11 52 PM":
                case "11 53 PM":
                case "11 54 PM":
                case "11 55 PM":
                case "11 56 PM":
                case "11 57 PM":
                case "11 58 PM":
                case "11 59 PM":
                case "12 PM":
                case "12 1 PM":
                case "12 2 PM":
                case "12 3 PM":
                case "12 4 PM":
                case "12 5 PM":
                case "12 6 PM":
                case "12 7 PM":
                case "12 8 PM":
                case "12 9 PM":
                case "12 10 PM":
                case "12 11 PM":
                case "12 12 PM":
                case "12 13 PM":
                case "12 14 PM":
                case "12 15 PM":
                case "12 16 PM":
                case "12 17 PM":
                case "12 18 PM":
                case "12 19 PM":
                case "12 20 PM":
                case "12 21 PM":
                case "12 22 PM":
                case "12 23 PM":
                case "12 24 PM":
                case "12 25 PM":
                case "12 26 PM":
                case "12 27 PM":
                case "12 28 PM":
                case "12 29 PM":
                case "12 30 PM":
                case "12 31 PM":
                case "12 32 PM":
                case "12 33 PM":
                case "12 34 PM":
                case "12 35 PM":
                case "12 36 PM":
                case "12 37 PM":
                case "12 38 PM":
                case "12 39 PM":
                case "12 40 PM":
                case "12 41 PM":
                case "12 42 PM":
                case "12 43 PM":
                case "12 44 PM":
                case "12 45 PM":
                case "12 46 PM":
                case "12 47 PM":
                case "12 48 PM":
                case "12 49 PM":
                case "12 50 PM":
                case "12 51 PM":
                case "12 52 PM":
                case "12 53 PM":
                case "12 54 PM":
                case "12 55 PM":
                case "12 56 PM":
                case "12 57 PM":
                case "12 58 PM":
                case "12 59 PM":
                    settimetxt.Text = e.Result.Text;
                    settimetxt.Update();
                    if (settimetxt.Text == "1 AM")
                    {
                        timereplacetxt.Text = "01:00:00:AM";
                    }
                    if (settimetxt.Text == "1 1 AM")
                    {
                        timereplacetxt.Text = "01:01:00:AM";
                    }
                    if (settimetxt.Text == "1 2 AM")
                    {
                        timereplacetxt.Text = "01:02:00:AM";
                    }
                    if (settimetxt.Text == "1 3 AM")
                    {
                        timereplacetxt.Text = "01:03:00:AM";
                    }
                    if (settimetxt.Text == "1 4 AM")
                    {
                        timereplacetxt.Text = "01:04:00:AM";
                    }
                    if (settimetxt.Text == "1 5 AM")
                    {
                        timereplacetxt.Text = "01:05:00:AM";
                    }
                    if (settimetxt.Text == "1 6 AM")
                    {
                        timereplacetxt.Text = "01:06:00:AM";
                    }
                    if (settimetxt.Text == "1 7 AM")
                    {
                        timereplacetxt.Text = "01:07:00:AM";
                    }
                    if (settimetxt.Text == "1 8 AM")
                    {
                        timereplacetxt.Text = "01:08:00:AM";
                    }
                    if (settimetxt.Text == "1 9 AM")
                    {
                        timereplacetxt.Text = "01:09:00:AM";
                    }
                    if (settimetxt.Text == "1 10 AM")
                    {
                        timereplacetxt.Text = "01:10:00:AM";
                    }
                    if (settimetxt.Text == "1 11 AM")
                    {
                        timereplacetxt.Text = "01:11:00:AM";
                    }
                    if (settimetxt.Text == "1 12 AM")
                    {
                        timereplacetxt.Text = "01:12:00:AM";
                    }
                    if (settimetxt.Text == "1 13 AM")
                    {
                        timereplacetxt.Text = "01:13:00:AM";
                    }
                    if (settimetxt.Text == "1 14 AM")
                    {
                        timereplacetxt.Text = "01:14:00:AM";
                    }
                    if (settimetxt.Text == "1 15 AM")
                    {
                        timereplacetxt.Text = "01:15:00:AM";
                    }
                    if (settimetxt.Text == "1 16 AM")
                    {
                        timereplacetxt.Text = "01:16:00:AM";
                    }
                    if (settimetxt.Text == "1 17 AM")
                    {
                        timereplacetxt.Text = "01:17:00:AM";
                    }
                    if (settimetxt.Text == "1 18 AM")
                    {
                        timereplacetxt.Text = "01:18:00:AM";
                    }
                    if (settimetxt.Text == "1 19 AM")
                    {
                        timereplacetxt.Text = "01:19:00:AM";
                    }
                    if (settimetxt.Text == "1 20 AM")
                    {
                        timereplacetxt.Text = "01:20:00:AM";
                    }
                    if (settimetxt.Text == "1 21 AM")
                    {
                        timereplacetxt.Text = "01:21:00:AM";
                    }
                    if (settimetxt.Text == "1 22 AM")
                    {
                        timereplacetxt.Text = "01:22:00:AM";
                    }
                    if (settimetxt.Text == "1 23 AM")
                    {
                        timereplacetxt.Text = "01:23:00:AM";
                    }
                    if (settimetxt.Text == "1 24 AM")
                    {
                        timereplacetxt.Text = "01:24:00:AM";
                    }
                    if (settimetxt.Text == "1 25 AM")
                    {
                        timereplacetxt.Text = "01:25:00:AM";
                    }
                    if (settimetxt.Text == "1 26 AM")
                    {
                        timereplacetxt.Text = "01:26:00:AM";
                    }
                    if (settimetxt.Text == "1 27 AM")
                    {
                        timereplacetxt.Text = "01:27:00:AM";
                    }
                    if (settimetxt.Text == "1 28 AM")
                    {
                        timereplacetxt.Text = "01:28:00:AM";
                    }
                    if (settimetxt.Text == "1 29 AM")
                    {
                        timereplacetxt.Text = "01:29:00:AM";
                    }
                    if (settimetxt.Text == "1 30 AM")
                    {
                        timereplacetxt.Text = "01:30:00:AM";
                    }
                    if (settimetxt.Text == "1 31 AM")
                    {
                        timereplacetxt.Text = "01:31:00:AM";
                    }
                    if (settimetxt.Text == "1 32 AM")
                    {
                        timereplacetxt.Text = "01:32:00:AM";
                    }
                    if (settimetxt.Text == "1 33 AM")
                    {
                        timereplacetxt.Text = "01:33:00:AM";
                    }
                    if (settimetxt.Text == "1 34 AM")
                    {
                        timereplacetxt.Text = "01:34:00:AM";
                    }
                    if (settimetxt.Text == "1 35 AM")
                    {
                        timereplacetxt.Text = "01:35:00:AM";
                    }
                    if (settimetxt.Text == "1 36 AM")
                    {
                        timereplacetxt.Text = "01:36:00:AM";
                    }
                    if (settimetxt.Text == "1 37 AM")
                    {
                        timereplacetxt.Text = "01:37:00:AM";
                    }
                    if (settimetxt.Text == "1 38 AM")
                    {
                        timereplacetxt.Text = "01:38:00:AM";
                    }
                    if (settimetxt.Text == "1 39 AM")
                    {
                        timereplacetxt.Text = "01:39:00:AM";
                    }
                    if (settimetxt.Text == "1 40 AM")
                    {
                        timereplacetxt.Text = "01:40:00:AM";
                    }
                    if (settimetxt.Text == "1 41 AM")
                    {
                        timereplacetxt.Text = "01:41:00:AM";
                    }
                    if (settimetxt.Text == "1 42 AM")
                    {
                        timereplacetxt.Text = "01:42:00:AM";
                    }
                    if (settimetxt.Text == "1 43 AM")
                    {
                        timereplacetxt.Text = "01:43:00:AM";
                    }
                    if (settimetxt.Text == "1 44 AM")
                    {
                        timereplacetxt.Text = "01:44:00:AM";
                    }
                    if (settimetxt.Text == "1 45 AM")
                    {
                        timereplacetxt.Text = "01:45:00:AM";
                    }
                    if (settimetxt.Text == "1 46 AM")
                    {
                        timereplacetxt.Text = "01:46:00:AM";
                    }
                    if (settimetxt.Text == "1 47 AM")
                    {
                        timereplacetxt.Text = "01:47:00:AM";
                    }
                    if (settimetxt.Text == "1 48 AM")
                    {
                        timereplacetxt.Text = "01:48:00:AM";
                    }
                    if (settimetxt.Text == "1 49 AM")
                    {
                        timereplacetxt.Text = "01:49:00:AM";
                    }
                    if (settimetxt.Text == "1 50 AM")
                    {
                        timereplacetxt.Text = "01:50:00:AM";
                    }
                    if (settimetxt.Text == "1 51 AM")
                    {
                        timereplacetxt.Text = "01:51:00:AM";
                    }
                    if (settimetxt.Text == "1 52 AM")
                    {
                        timereplacetxt.Text = "01:52:00:AM";
                    }
                    if (settimetxt.Text == "1 53 AM")
                    {
                        timereplacetxt.Text = "01:53:00:AM";
                    }
                    if (settimetxt.Text == "1 54 AM")
                    {
                        timereplacetxt.Text = "01:54:00:AM";
                    }
                    if (settimetxt.Text == "1 55 AM")
                    {
                        timereplacetxt.Text = "01:55:00:AM";
                    }
                    if (settimetxt.Text == "1 56 AM")
                    {
                        timereplacetxt.Text = "01:56:00:AM";
                    }
                    if (settimetxt.Text == "1 57 AM")
                    {
                        timereplacetxt.Text = "01:57:00:AM";
                    }
                    if (settimetxt.Text == "1 58 AM")
                    {
                        timereplacetxt.Text = "01:58:00:AM";
                    }
                    if (settimetxt.Text == "1 59 AM")
                    {
                        timereplacetxt.Text = "01:59:00:AM";
                    }
                    if (settimetxt.Text == "2 AM")
                    {
                        timereplacetxt.Text = "02:00:00:AM";
                    }
                    if (settimetxt.Text == "2 1 AM")
                    {
                        timereplacetxt.Text = "02:01:00:AM";
                    }
                    if (settimetxt.Text == "2 2 AM")
                    {
                        timereplacetxt.Text = "02:02:00:AM";
                    }
                    if (settimetxt.Text == "2 3 AM")
                    {
                        timereplacetxt.Text = "02:03:00:AM";
                    }
                    if (settimetxt.Text == "2 4 AM")
                    {
                        timereplacetxt.Text = "02:04:00:AM";
                    }
                    if (settimetxt.Text == "2 5 AM")
                    {
                        timereplacetxt.Text = "02:05:00:AM";
                    }
                    if (settimetxt.Text == "2 6 AM")
                    {
                        timereplacetxt.Text = "02:06:00:AM";
                    }
                    if (settimetxt.Text == "2 7 AM")
                    {
                        timereplacetxt.Text = "02:07:00:AM";
                    }
                    if (settimetxt.Text == "2 8 AM")
                    {
                        timereplacetxt.Text = "02:08:00:AM";
                    }
                    if (settimetxt.Text == "2 9 AM")
                    {
                        timereplacetxt.Text = "02:09:00:AM";
                    }
                    if (settimetxt.Text == "2 10 AM")
                    {
                        timereplacetxt.Text = "02:10:00:AM";
                    }
                    if (settimetxt.Text == "2 11 AM")
                    {
                        timereplacetxt.Text = "02:11:00:AM";
                    }
                    if (settimetxt.Text == "2 12 AM")
                    {
                        timereplacetxt.Text = "02:12:00:AM";
                    }
                    if (settimetxt.Text == "2 13 AM")
                    {
                        timereplacetxt.Text = "02:13:00:AM";
                    }
                    if (settimetxt.Text == "2 14 AM")
                    {
                        timereplacetxt.Text = "02:14:00:AM";
                    }
                    if (settimetxt.Text == "2 15 AM")
                    {
                        timereplacetxt.Text = "02:15:00:AM";
                    }
                    if (settimetxt.Text == "2 16 AM")
                    {
                        timereplacetxt.Text = "02:16:00:AM";
                    }
                    if (settimetxt.Text == "2 17 AM")
                    {
                        timereplacetxt.Text = "02:17:00:AM";
                    }
                    if (settimetxt.Text == "2 18 AM")
                    {
                        timereplacetxt.Text = "02:18:00:AM";
                    }
                    if (settimetxt.Text == "2 19 AM")
                    {
                        timereplacetxt.Text = "02:19:00:AM";
                    }
                    if (settimetxt.Text == "2 20 AM")
                    {
                        timereplacetxt.Text = "02:20:00:AM";
                    }
                    if (settimetxt.Text == "2 21 AM")
                    {
                        timereplacetxt.Text = "02:21:00:AM";
                    }
                    if (settimetxt.Text == "2 22 AM")
                    {
                        timereplacetxt.Text = "02:22:00:AM";
                    }
                    if (settimetxt.Text == "2 23 AM")
                    {
                        timereplacetxt.Text = "02:23:00:AM";
                    }
                    if (settimetxt.Text == "2 24 AM")
                    {
                        timereplacetxt.Text = "02:24:00:AM";
                    }
                    if (settimetxt.Text == "2 25 AM")
                    {
                        timereplacetxt.Text = "02:25:00:AM";
                    }
                    if (settimetxt.Text == "2 26 AM")
                    {
                        timereplacetxt.Text = "02:26:00:AM";
                    }
                    if (settimetxt.Text == "2 27 AM")
                    {
                        timereplacetxt.Text = "02:27:00:AM";
                    }
                    if (settimetxt.Text == "2 28 AM")
                    {
                        timereplacetxt.Text = "02:28:00:AM";
                    }
                    if (settimetxt.Text == "2 29 AM")
                    {
                        timereplacetxt.Text = "02:29:00:AM";
                    }
                    if (settimetxt.Text == "2 30 AM")
                    {
                        timereplacetxt.Text = "02:30:00:AM";
                    }
                    if (settimetxt.Text == "2 31 AM")
                    {
                        timereplacetxt.Text = "02:31:00:AM";
                    }
                    if (settimetxt.Text == "2 32 AM")
                    {
                        timereplacetxt.Text = "02:32:00:AM";
                    }
                    if (settimetxt.Text == "2 33 AM")
                    {
                        timereplacetxt.Text = "02:33:00:AM";
                    }
                    if (settimetxt.Text == "2 34 AM")
                    {
                        timereplacetxt.Text = "02:34:00:AM";
                    }
                    if (settimetxt.Text == "2 35 AM")
                    {
                        timereplacetxt.Text = "02:35:00:AM";
                    }
                    if (settimetxt.Text == "2 36 AM")
                    {
                        timereplacetxt.Text = "02:36:00:AM";
                    }
                    if (settimetxt.Text == "2 37 AM")
                    {
                        timereplacetxt.Text = "02:37:00:AM";
                    }
                    if (settimetxt.Text == "2 38 AM")
                    {
                        timereplacetxt.Text = "02:38:00:AM";
                    }
                    if (settimetxt.Text == "2 39 AM")
                    {
                        timereplacetxt.Text = "02:39:00:AM";
                    }
                    if (settimetxt.Text == "2 40 AM")
                    {
                        timereplacetxt.Text = "02:40:00:AM";
                    }
                    if (settimetxt.Text == "2 41 AM")
                    {
                        timereplacetxt.Text = "02:41:00:AM";
                    }
                    if (settimetxt.Text == "2 42 AM")
                    {
                        timereplacetxt.Text = "02:42:00:AM";
                    }
                    if (settimetxt.Text == "2 43 AM")
                    {
                        timereplacetxt.Text = "02:43:00:AM";
                    }
                    if (settimetxt.Text == "2 44 AM")
                    {
                        timereplacetxt.Text = "02:44:00:AM";
                    }
                    if (settimetxt.Text == "2 45 AM")
                    {
                        timereplacetxt.Text = "02:45:00:AM";
                    }
                    if (settimetxt.Text == "2 46 AM")
                    {
                        timereplacetxt.Text = "02:46:00:AM";
                    }
                    if (settimetxt.Text == "2 47 AM")
                    {
                        timereplacetxt.Text = "02:47:00:AM";
                    }
                    if (settimetxt.Text == "2 48 AM")
                    {
                        timereplacetxt.Text = "02:48:00:AM";
                    }
                    if (settimetxt.Text == "2 49 AM")
                    {
                        timereplacetxt.Text = "02:49:00:AM";
                    }
                    if (settimetxt.Text == "2 50 AM")
                    {
                        timereplacetxt.Text = "02:50:00:AM";
                    }
                    if (settimetxt.Text == "2 51 AM")
                    {
                        timereplacetxt.Text = "02:51:00:AM";
                    }
                    if (settimetxt.Text == "2 52 AM")
                    {
                        timereplacetxt.Text = "02:52:00:AM";
                    }
                    if (settimetxt.Text == "2 53 AM")
                    {
                        timereplacetxt.Text = "02:53:00:AM";
                    }
                    if (settimetxt.Text == "2 54 AM")
                    {
                        timereplacetxt.Text = "02:54:00:AM";
                    }
                    if (settimetxt.Text == "2 55 AM")
                    {
                        timereplacetxt.Text = "02:55:00:AM";
                    }
                    if (settimetxt.Text == "2 56 AM")
                    {
                        timereplacetxt.Text = "02:56:00:AM";
                    }
                    if (settimetxt.Text == "2 57 AM")
                    {
                        timereplacetxt.Text = "02:57:00:AM";
                    }
                    if (settimetxt.Text == "2 58 AM")
                    {
                        timereplacetxt.Text = "02:58:00:AM";
                    }
                    if (settimetxt.Text == "2 59 AM")
                    {
                        timereplacetxt.Text = "02:59:00:AM";
                    }
                    if (settimetxt.Text == "3 AM")
                    {
                        timereplacetxt.Text = "03:00:00:AM";
                    }
                    if (settimetxt.Text == "3 1 AM")
                    {
                        timereplacetxt.Text = "03:01:00:AM";
                    }
                    if (settimetxt.Text == "3 2 AM")
                    {
                        timereplacetxt.Text = "03:02:00:AM";
                    }
                    if (settimetxt.Text == "3 3 AM")
                    {
                        timereplacetxt.Text = "03:03:00:AM";
                    }
                    if (settimetxt.Text == "3 4 AM")
                    {
                        timereplacetxt.Text = "03:04:00:AM";
                    }
                    if (settimetxt.Text == "3 5 AM")
                    {
                        timereplacetxt.Text = "03:05:00:AM";
                    }
                    if (settimetxt.Text == "3 6 AM")
                    {
                        timereplacetxt.Text = "03:06:00:AM";
                    }
                    if (settimetxt.Text == "3 7 AM")
                    {
                        timereplacetxt.Text = "03:07:00:AM";
                    }
                    if (settimetxt.Text == "3 8 AM")
                    {
                        timereplacetxt.Text = "03:08:00:AM";
                    }
                    if (settimetxt.Text == "3 9 AM")
                    {
                        timereplacetxt.Text = "03:09:00:AM";
                    }
                    if (settimetxt.Text == "3 10 AM")
                    {
                        timereplacetxt.Text = "03:10:00:AM";
                    }
                    if (settimetxt.Text == "3 11 AM")
                    {
                        timereplacetxt.Text = "03:11:00:AM";
                    }
                    if (settimetxt.Text == "3 12 AM")
                    {
                        timereplacetxt.Text = "03:12:00:AM";
                    }
                    if (settimetxt.Text == "3 13 AM")
                    {
                        timereplacetxt.Text = "03:13:00:AM";
                    }
                    if (settimetxt.Text == "3 14 AM")
                    {
                        timereplacetxt.Text = "03:14:00:AM";
                    }
                    if (settimetxt.Text == "3 15 AM")
                    {
                        timereplacetxt.Text = "03:15:00:AM";
                    }
                    if (settimetxt.Text == "3 16 AM")
                    {
                        timereplacetxt.Text = "03:16:00:AM";
                    }
                    if (settimetxt.Text == "3 17 AM")
                    {
                        timereplacetxt.Text = "03:17:00:AM";
                    }
                    if (settimetxt.Text == "3 18 AM")
                    {
                        timereplacetxt.Text = "03:18:00:AM";
                    }
                    if (settimetxt.Text == "3 19 AM")
                    {
                        timereplacetxt.Text = "03:19:00:AM";
                    }
                    if (settimetxt.Text == "3 20 AM")
                    {
                        timereplacetxt.Text = "03:20:00:AM";
                    }
                    if (settimetxt.Text == "3 21 AM")
                    {
                        timereplacetxt.Text = "03:21:00:AM";
                    }
                    if (settimetxt.Text == "3 22 AM")
                    {
                        timereplacetxt.Text = "03:22:00:AM";
                    }
                    if (settimetxt.Text == "3 23 AM")
                    {
                        timereplacetxt.Text = "03:23:00:AM";
                    }
                    if (settimetxt.Text == "3 24 AM")
                    {
                        timereplacetxt.Text = "03:24:00:AM";
                    }
                    if (settimetxt.Text == "3 25 AM")
                    {
                        timereplacetxt.Text = "03:25:00:AM";
                    }
                    if (settimetxt.Text == "3 26 AM")
                    {
                        timereplacetxt.Text = "03:26:00:AM";
                    }
                    if (settimetxt.Text == "3 27 AM")
                    {
                        timereplacetxt.Text = "03:27:00:AM";
                    }
                    if (settimetxt.Text == "3 28 AM")
                    {
                        timereplacetxt.Text = "03:28:00:AM";
                    }
                    if (settimetxt.Text == "3 29 AM")
                    {
                        timereplacetxt.Text = "03:29:00:AM";
                    }
                    if (settimetxt.Text == "3 30 AM")
                    {
                        timereplacetxt.Text = "03:30:00:AM";
                    }
                    if (settimetxt.Text == "3 31 AM")
                    {
                        timereplacetxt.Text = "03:31:00:AM";
                    }
                    if (settimetxt.Text == "3 32 AM")
                    {
                        timereplacetxt.Text = "03:32:00:AM";
                    }
                    if (settimetxt.Text == "3 33 AM")
                    {
                        timereplacetxt.Text = "03:33:00:AM";
                    }
                    if (settimetxt.Text == "3 34 AM")
                    {
                        timereplacetxt.Text = "03:34:00:AM";
                    }
                    if (settimetxt.Text == "3 35 AM")
                    {
                        timereplacetxt.Text = "03:35:00:AM";
                    }
                    if (settimetxt.Text == "3 36 AM")
                    {
                        timereplacetxt.Text = "03:36:00:AM";
                    }
                    if (settimetxt.Text == "3 37 AM")
                    {
                        timereplacetxt.Text = "03:37:00:AM";
                    }
                    if (settimetxt.Text == "3 38 AM")
                    {
                        timereplacetxt.Text = "03:38:00:AM";
                    }
                    if (settimetxt.Text == "3 39 AM")
                    {
                        timereplacetxt.Text = "03:39:00:AM";
                    }
                    if (settimetxt.Text == "3 40 AM")
                    {
                        timereplacetxt.Text = "03:40:00:AM";
                    }
                    if (settimetxt.Text == "3 41 AM")
                    {
                        timereplacetxt.Text = "03:41:00:AM";
                    }
                    if (settimetxt.Text == "3 42 AM")
                    {
                        timereplacetxt.Text = "03:42:00:AM";
                    }
                    if (settimetxt.Text == "3 43 AM")
                    {
                        timereplacetxt.Text = "03:43:00:AM";
                    }
                    if (settimetxt.Text == "3 44 AM")
                    {
                        timereplacetxt.Text = "03:44:00:AM";
                    }
                    if (settimetxt.Text == "3 45 AM")
                    {
                        timereplacetxt.Text = "03:45:00:AM";
                    }
                    if (settimetxt.Text == "3 46 AM")
                    {
                        timereplacetxt.Text = "03:46:00:AM";
                    }
                    if (settimetxt.Text == "3 47 AM")
                    {
                        timereplacetxt.Text = "03:47:00:AM";
                    }
                    if (settimetxt.Text == "3 48 AM")
                    {
                        timereplacetxt.Text = "03:48:00:AM";
                    }
                    if (settimetxt.Text == "3 49 AM")
                    {
                        timereplacetxt.Text = "03:49:00:AM";
                    }
                    if (settimetxt.Text == "3 50 AM")
                    {
                        timereplacetxt.Text = "03:50:00:AM";
                    }
                    if (settimetxt.Text == "3 51 AM")
                    {
                        timereplacetxt.Text = "03:51:00:AM";
                    }
                    if (settimetxt.Text == "3 52 AM")
                    {
                        timereplacetxt.Text = "03:52:00:AM";
                    }
                    if (settimetxt.Text == "3 53 AM")
                    {
                        timereplacetxt.Text = "03:53:00:AM";
                    }
                    if (settimetxt.Text == "3 54 AM")
                    {
                        timereplacetxt.Text = "03:54:00:AM";
                    }
                    if (settimetxt.Text == "3 55 AM")
                    {
                        timereplacetxt.Text = "03:55:00:AM";
                    }
                    if (settimetxt.Text == "3 56 AM")
                    {
                        timereplacetxt.Text = "03:56:00:AM";
                    }
                    if (settimetxt.Text == "3 57 AM")
                    {
                        timereplacetxt.Text = "03:57:00:AM";
                    }
                    if (settimetxt.Text == "3 58 AM")
                    {
                        timereplacetxt.Text = "03:58:00:AM";
                    }
                    if (settimetxt.Text == "3 59 AM")
                    {
                        timereplacetxt.Text = "03:59:00:AM";
                    }
                    if (settimetxt.Text == "4 AM")
                    {
                        timereplacetxt.Text = "04:00:00:AM";
                    }
                    if (settimetxt.Text == "4 1 AM")
                    {
                        timereplacetxt.Text = "04:01:00:AM";
                    }
                    if (settimetxt.Text == "4 2 AM")
                    {
                        timereplacetxt.Text = "04:02:00:AM";
                    }
                    if (settimetxt.Text == "4 3 AM")
                    {
                        timereplacetxt.Text = "04:03:00:AM";
                    }
                    if (settimetxt.Text == "4 4 AM")
                    {
                        timereplacetxt.Text = "04:04:00:AM";
                    }
                    if (settimetxt.Text == "4 5 AM")
                    {
                        timereplacetxt.Text = "04:05:00:AM";
                    }
                    if (settimetxt.Text == "4 6 AM")
                    {
                        timereplacetxt.Text = "04:06:00:AM";
                    }
                    if (settimetxt.Text == "4 7 AM")
                    {
                        timereplacetxt.Text = "04:07:00:AM";
                    }
                    if (settimetxt.Text == "4 8 AM")
                    {
                        timereplacetxt.Text = "04:08:00:AM";
                    }
                    if (settimetxt.Text == "4 9 AM")
                    {
                        timereplacetxt.Text = "04:09:00:AM";
                    }
                    if (settimetxt.Text == "4 10 AM")
                    {
                        timereplacetxt.Text = "04:10:00:AM";
                    }
                    if (settimetxt.Text == "4 11 AM")
                    {
                        timereplacetxt.Text = "04:11:00:AM";
                    }
                    if (settimetxt.Text == "4 12 AM")
                    {
                        timereplacetxt.Text = "04:12:00:AM";
                    }
                    if (settimetxt.Text == "4 13 AM")
                    {
                        timereplacetxt.Text = "04:13:00:AM";
                    }
                    if (settimetxt.Text == "4 14 AM")
                    {
                        timereplacetxt.Text = "04:14:00:AM";
                    }
                    if (settimetxt.Text == "4 15 AM")
                    {
                        timereplacetxt.Text = "04:15:00:AM";
                    }
                    if (settimetxt.Text == "4 16 AM")
                    {
                        timereplacetxt.Text = "04:16:00:AM";
                    }
                    if (settimetxt.Text == "4 17 AM")
                    {
                        timereplacetxt.Text = "04:17:00:AM";
                    }
                    if (settimetxt.Text == "4 18 AM")
                    {
                        timereplacetxt.Text = "04:18:00:AM";
                    }
                    if (settimetxt.Text == "4 19 AM")
                    {
                        timereplacetxt.Text = "04:19:00:AM";
                    }
                    if (settimetxt.Text == "4 20 AM")
                    {
                        timereplacetxt.Text = "04:20:00:AM";
                    }
                    if (settimetxt.Text == "4 21 AM")
                    {
                        timereplacetxt.Text = "04:21:00:AM";
                    }
                    if (settimetxt.Text == "4 22 AM")
                    {
                        timereplacetxt.Text = "04:22:00:AM";
                    }
                    if (settimetxt.Text == "4 23 AM")
                    {
                        timereplacetxt.Text = "04:23:00:AM";
                    }
                    if (settimetxt.Text == "4 24 AM")
                    {
                        timereplacetxt.Text = "04:24:00:AM";
                    }
                    if (settimetxt.Text == "4 25 AM")
                    {
                        timereplacetxt.Text = "04:25:00:AM";
                    }
                    if (settimetxt.Text == "4 26 AM")
                    {
                        timereplacetxt.Text = "04:26:00:AM";
                    }
                    if (settimetxt.Text == "4 27 AM")
                    {
                        timereplacetxt.Text = "04:27:00:AM";
                    }
                    if (settimetxt.Text == "4 28 AM")
                    {
                        timereplacetxt.Text = "04:28:00:AM";
                    }
                    if (settimetxt.Text == "4 29 AM")
                    {
                        timereplacetxt.Text = "04:29:00:AM";
                    }
                    if (settimetxt.Text == "4 30 AM")
                    {
                        timereplacetxt.Text = "04:30:00:AM";
                    }
                    if (settimetxt.Text == "4 31 AM")
                    {
                        timereplacetxt.Text = "04:31:00:AM";
                    }
                    if (settimetxt.Text == "4 32 AM")
                    {
                        timereplacetxt.Text = "04:32:00:AM";
                    }
                    if (settimetxt.Text == "4 33 AM")
                    {
                        timereplacetxt.Text = "04:33:00:AM";
                    }
                    if (settimetxt.Text == "4 34 AM")
                    {
                        timereplacetxt.Text = "04:34:00:AM";
                    }
                    if (settimetxt.Text == "4 35 AM")
                    {
                        timereplacetxt.Text = "04:35:00:AM";
                    }
                    if (settimetxt.Text == "4 36 AM")
                    {
                        timereplacetxt.Text = "04:36:00:AM";
                    }
                    if (settimetxt.Text == "4 37 AM")
                    {
                        timereplacetxt.Text = "04:37:00:AM";
                    }
                    if (settimetxt.Text == "4 38 AM")
                    {
                        timereplacetxt.Text = "04:38:00:AM";
                    }
                    if (settimetxt.Text == "4 39 AM")
                    {
                        timereplacetxt.Text = "04:39:00:AM";
                    }
                    if (settimetxt.Text == "4 40 AM")
                    {
                        timereplacetxt.Text = "04:40:00:AM";
                    }
                    if (settimetxt.Text == "4 41 AM")
                    {
                        timereplacetxt.Text = "04:41:00:AM";
                    }
                    if (settimetxt.Text == "4 42 AM")
                    {
                        timereplacetxt.Text = "04:42:00:AM";
                    }
                    if (settimetxt.Text == "4 43 AM")
                    {
                        timereplacetxt.Text = "04:43:00:AM";
                    }
                    if (settimetxt.Text == "4 44 AM")
                    {
                        timereplacetxt.Text = "04:44:00:AM";
                    }
                    if (settimetxt.Text == "4 45 AM")
                    {
                        timereplacetxt.Text = "04:45:00:AM";
                    }
                    if (settimetxt.Text == "4 46 AM")
                    {
                        timereplacetxt.Text = "04:46:00:AM";
                    }
                    if (settimetxt.Text == "4 47 AM")
                    {
                        timereplacetxt.Text = "04:47:00:AM";
                    }
                    if (settimetxt.Text == "4 48 AM")
                    {
                        timereplacetxt.Text = "04:48:00:AM";
                    }
                    if (settimetxt.Text == "4 49 AM")
                    {
                        timereplacetxt.Text = "04:49:00:AM";
                    }
                    if (settimetxt.Text == "4 50 AM")
                    {
                        timereplacetxt.Text = "04:50:00:AM";
                    }
                    if (settimetxt.Text == "4 51 AM")
                    {
                        timereplacetxt.Text = "04:51:00:AM";
                    }
                    if (settimetxt.Text == "4 52 AM")
                    {
                        timereplacetxt.Text = "04:52:00:AM";
                    }
                    if (settimetxt.Text == "4 53 AM")
                    {
                        timereplacetxt.Text = "04:53:00:AM";
                    }
                    if (settimetxt.Text == "4 54 AM")
                    {
                        timereplacetxt.Text = "04:54:00:AM";
                    }
                    if (settimetxt.Text == "4 55 AM")
                    {
                        timereplacetxt.Text = "04:55:00:AM";
                    }
                    if (settimetxt.Text == "4 56 AM")
                    {
                        timereplacetxt.Text = "04:56:00:AM";
                    }
                    if (settimetxt.Text == "4 57 AM")
                    {
                        timereplacetxt.Text = "04:57:00:AM";
                    }
                    if (settimetxt.Text == "4 58 AM")
                    {
                        timereplacetxt.Text = "04:58:00:AM";
                    }
                    if (settimetxt.Text == "4 59 AM")
                    {
                        timereplacetxt.Text = "04:59:00:AM";
                    }
                    if (settimetxt.Text == "5 AM")
                    {
                        timereplacetxt.Text = "05:00:00:AM";
                    }
                    if (settimetxt.Text == "5 1 AM")
                    {
                        timereplacetxt.Text = "05:01:00:AM";
                    }
                    if (settimetxt.Text == "5 2 AM")
                    {
                        timereplacetxt.Text = "05:02:00:AM";
                    }
                    if (settimetxt.Text == "5 3 AM")
                    {
                        timereplacetxt.Text = "05:03:00:AM";
                    }
                    if (settimetxt.Text == "5 4 AM")
                    {
                        timereplacetxt.Text = "05:04:00:AM";
                    }
                    if (settimetxt.Text == "5 5 AM")
                    {
                        timereplacetxt.Text = "05:05:00:AM";
                    }
                    if (settimetxt.Text == "5 6 AM")
                    {
                        timereplacetxt.Text = "05:06:00:AM";
                    }
                    if (settimetxt.Text == "5 7 AM")
                    {
                        timereplacetxt.Text = "05:07:00:AM";
                    }
                    if (settimetxt.Text == "5 8 AM")
                    {
                        timereplacetxt.Text = "05:08:00:AM";
                    }
                    if (settimetxt.Text == "5 9 AM")
                    {
                        timereplacetxt.Text = "05:09:00:AM";
                    }
                    if (settimetxt.Text == "5 10 AM")
                    {
                        timereplacetxt.Text = "05:10:00:AM";
                    }
                    if (settimetxt.Text == "5 11 AM")
                    {
                        timereplacetxt.Text = "05:11:00:AM";
                    }
                    if (settimetxt.Text == "5 12 AM")
                    {
                        timereplacetxt.Text = "05:12:00:AM";
                    }
                    if (settimetxt.Text == "5 13 AM")
                    {
                        timereplacetxt.Text = "05:13:00:AM";
                    }
                    if (settimetxt.Text == "5 14 AM")
                    {
                        timereplacetxt.Text = "05:14:00:AM";
                    }
                    if (settimetxt.Text == "5 15 AM")
                    {
                        timereplacetxt.Text = "05:15:00:AM";
                    }
                    if (settimetxt.Text == "5 16 AM")
                    {
                        timereplacetxt.Text = "05:16:00:AM";
                    }
                    if (settimetxt.Text == "5 17 AM")
                    {
                        timereplacetxt.Text = "05:17:00:AM";
                    }
                    if (settimetxt.Text == "5 18 AM")
                    {
                        timereplacetxt.Text = "05:18:00:AM";
                    }
                    if (settimetxt.Text == "5 19 AM")
                    {
                        timereplacetxt.Text = "05:19:00:AM";
                    }
                    if (settimetxt.Text == "5 20 AM")
                    {
                        timereplacetxt.Text = "05:20:00:AM";
                    }
                    if (settimetxt.Text == "5 21 AM")
                    {
                        timereplacetxt.Text = "05:21:00:AM";
                    }
                    if (settimetxt.Text == "5 22 AM")
                    {
                        timereplacetxt.Text = "05:22:00:AM";
                    }
                    if (settimetxt.Text == "5 23 AM")
                    {
                        timereplacetxt.Text = "05:23:00:AM";
                    }
                    if (settimetxt.Text == "5 24 AM")
                    {
                        timereplacetxt.Text = "05:24:00:AM";
                    }
                    if (settimetxt.Text == "5 25 AM")
                    {
                        timereplacetxt.Text = "05:25:00:AM";
                    }
                    if (settimetxt.Text == "5 26 AM")
                    {
                        timereplacetxt.Text = "05:26:00:AM";
                    }
                    if (settimetxt.Text == "5 27 AM")
                    {
                        timereplacetxt.Text = "05:27:00:AM";
                    }
                    if (settimetxt.Text == "5 28 AM")
                    {
                        timereplacetxt.Text = "05:28:00:AM";
                    }
                    if (settimetxt.Text == "5 29 AM")
                    {
                        timereplacetxt.Text = "05:29:00:AM";
                    }
                    if (settimetxt.Text == "5 30 AM")
                    {
                        timereplacetxt.Text = "05:30:00:AM";
                    }
                    if (settimetxt.Text == "5 31 AM")
                    {
                        timereplacetxt.Text = "05:31:00:AM";
                    }
                    if (settimetxt.Text == "5 32 AM")
                    {
                        timereplacetxt.Text = "05:32:00:AM";
                    }
                    if (settimetxt.Text == "5 33 AM")
                    {
                        timereplacetxt.Text = "05:33:00:AM";
                    }
                    if (settimetxt.Text == "5 34 AM")
                    {
                        timereplacetxt.Text = "05:34:00:AM";
                    }
                    if (settimetxt.Text == "5 35 AM")
                    {
                        timereplacetxt.Text = "05:35:00:AM";
                    }
                    if (settimetxt.Text == "5 36 AM")
                    {
                        timereplacetxt.Text = "05:36:00:AM";
                    }
                    if (settimetxt.Text == "5 37 AM")
                    {
                        timereplacetxt.Text = "05:37:00:AM";
                    }
                    if (settimetxt.Text == "5 38 AM")
                    {
                        timereplacetxt.Text = "05:38:00:AM";
                    }
                    if (settimetxt.Text == "5 39 AM")
                    {
                        timereplacetxt.Text = "05:39:00:AM";
                    }
                    if (settimetxt.Text == "5 40 AM")
                    {
                        timereplacetxt.Text = "05:40:00:AM";
                    }
                    if (settimetxt.Text == "5 41 AM")
                    {
                        timereplacetxt.Text = "05:41:00:AM";
                    }
                    if (settimetxt.Text == "5 42 AM")
                    {
                        timereplacetxt.Text = "05:42:00:AM";
                    }
                    if (settimetxt.Text == "5 43 AM")
                    {
                        timereplacetxt.Text = "05:43:00:AM";
                    }
                    if (settimetxt.Text == "5 44 AM")
                    {
                        timereplacetxt.Text = "05:44:00:AM";
                    }
                    if (settimetxt.Text == "5 45 AM")
                    {
                        timereplacetxt.Text = "05:45:00:AM";
                    }
                    if (settimetxt.Text == "5 46 AM")
                    {
                        timereplacetxt.Text = "05:46:00:AM";
                    }
                    if (settimetxt.Text == "5 47 AM")
                    {
                        timereplacetxt.Text = "05:47:00:AM";
                    }
                    if (settimetxt.Text == "5 48 AM")
                    {
                        timereplacetxt.Text = "05:48:00:AM";
                    }
                    if (settimetxt.Text == "5 49 AM")
                    {
                        timereplacetxt.Text = "05:49:00:AM";
                    }
                    if (settimetxt.Text == "5 50 AM")
                    {
                        timereplacetxt.Text = "05:50:00:AM";
                    }
                    if (settimetxt.Text == "5 51 AM")
                    {
                        timereplacetxt.Text = "05:51:00:AM";
                    }
                    if (settimetxt.Text == "5 52 AM")
                    {
                        timereplacetxt.Text = "05:52:00:AM";
                    }
                    if (settimetxt.Text == "5 53 AM")
                    {
                        timereplacetxt.Text = "05:53:00:AM";
                    }
                    if (settimetxt.Text == "5 54 AM")
                    {
                        timereplacetxt.Text = "05:54:00:AM";
                    }
                    if (settimetxt.Text == "5 55 AM")
                    {
                        timereplacetxt.Text = "05:55:00:AM";
                    }
                    if (settimetxt.Text == "5 56 AM")
                    {
                        timereplacetxt.Text = "05:56:00:AM";
                    }
                    if (settimetxt.Text == "5 57 AM")
                    {
                        timereplacetxt.Text = "05:57:00:AM";
                    }
                    if (settimetxt.Text == "5 58 AM")
                    {
                        timereplacetxt.Text = "05:58:00:AM";
                    }
                    if (settimetxt.Text == "5 59 AM")
                    {
                        timereplacetxt.Text = "05:59:00:AM";
                    }
                    if (settimetxt.Text == "6 AM")
                    {
                        timereplacetxt.Text = "06:00:00:AM";
                    }
                    if (settimetxt.Text == "6 1 AM")
                    {
                        timereplacetxt.Text = "06:01:00:AM";
                    }
                    if (settimetxt.Text == "6 2 AM")
                    {
                        timereplacetxt.Text = "06:02:00:AM";
                    }
                    if (settimetxt.Text == "6 3 AM")
                    {
                        timereplacetxt.Text = "06:03:00:AM";
                    }
                    if (settimetxt.Text == "6 4 AM")
                    {
                        timereplacetxt.Text = "06:04:00:AM";
                    }
                    if (settimetxt.Text == "6 5 AM")
                    {
                        timereplacetxt.Text = "06:05:00:AM";
                    }
                    if (settimetxt.Text == "6 6 AM")
                    {
                        timereplacetxt.Text = "06:06:00:AM";
                    }
                    if (settimetxt.Text == "6 7 AM")
                    {
                        timereplacetxt.Text = "06:07:00:AM";
                    }
                    if (settimetxt.Text == "6 8 AM")
                    {
                        timereplacetxt.Text = "06:08:00:AM";
                    }
                    if (settimetxt.Text == "6 9 AM")
                    {
                        timereplacetxt.Text = "06:09:00:AM";
                    }
                    if (settimetxt.Text == "6 10 AM")
                    {
                        timereplacetxt.Text = "06:10:00:AM";
                    }
                    if (settimetxt.Text == "6 11 AM")
                    {
                        timereplacetxt.Text = "06:11:00:AM";
                    }
                    if (settimetxt.Text == "6 12 AM")
                    {
                        timereplacetxt.Text = "06:12:00:AM";
                    }
                    if (settimetxt.Text == "6 13 AM")
                    {
                        timereplacetxt.Text = "06:13:00:AM";
                    }
                    if (settimetxt.Text == "6 14 AM")
                    {
                        timereplacetxt.Text = "06:14:00:AM";
                    }
                    if (settimetxt.Text == "6 15 AM")
                    {
                        timereplacetxt.Text = "06:15:00:AM";
                    }
                    if (settimetxt.Text == "6 16 AM")
                    {
                        timereplacetxt.Text = "06:16:00:AM";
                    }
                    if (settimetxt.Text == "6 17 AM")
                    {
                        timereplacetxt.Text = "06:17:00:AM";
                    }
                    if (settimetxt.Text == "6 18 AM")
                    {
                        timereplacetxt.Text = "06:18:00:AM";
                    }
                    if (settimetxt.Text == "6 19 AM")
                    {
                        timereplacetxt.Text = "06:19:00:AM";
                    }
                    if (settimetxt.Text == "6 20 AM")
                    {
                        timereplacetxt.Text = "06:20:00:AM";
                    }
                    if (settimetxt.Text == "6 21 AM")
                    {
                        timereplacetxt.Text = "06:21:00:AM";
                    }
                    if (settimetxt.Text == "6 22 AM")
                    {
                        timereplacetxt.Text = "06:22:00:AM";
                    }
                    if (settimetxt.Text == "6 23 AM")
                    {
                        timereplacetxt.Text = "06:23:00:AM";
                    }
                    if (settimetxt.Text == "6 24 AM")
                    {
                        timereplacetxt.Text = "06:24:00:AM";
                    }
                    if (settimetxt.Text == "6 25 AM")
                    {
                        timereplacetxt.Text = "06:25:00:AM";
                    }
                    if (settimetxt.Text == "6 26 AM")
                    {
                        timereplacetxt.Text = "06:26:00:AM";
                    }
                    if (settimetxt.Text == "6 27 AM")
                    {
                        timereplacetxt.Text = "06:27:00:AM";
                    }
                    if (settimetxt.Text == "6 28 AM")
                    {
                        timereplacetxt.Text = "06:28:00:AM";
                    }
                    if (settimetxt.Text == "6 29 AM")
                    {
                        timereplacetxt.Text = "06:29:00:AM";
                    }
                    if (settimetxt.Text == "6 30 AM")
                    {
                        timereplacetxt.Text = "06:30:00:AM";
                    }
                    if (settimetxt.Text == "6 31 AM")
                    {
                        timereplacetxt.Text = "06:31:00:AM";
                    }
                    if (settimetxt.Text == "6 32 AM")
                    {
                        timereplacetxt.Text = "06:32:00:AM";
                    }
                    if (settimetxt.Text == "6 33 AM")
                    {
                        timereplacetxt.Text = "06:33:00:AM";
                    }
                    if (settimetxt.Text == "6 34 AM")
                    {
                        timereplacetxt.Text = "06:34:00:AM";
                    }
                    if (settimetxt.Text == "6 35 AM")
                    {
                        timereplacetxt.Text = "06:35:00:AM";
                    }
                    if (settimetxt.Text == "6 36 AM")
                    {
                        timereplacetxt.Text = "06:36:00:AM";
                    }
                    if (settimetxt.Text == "6 37 AM")
                    {
                        timereplacetxt.Text = "06:37:00:AM";
                    }
                    if (settimetxt.Text == "6 38 AM")
                    {
                        timereplacetxt.Text = "06:38:00:AM";
                    }
                    if (settimetxt.Text == "6 39 AM")
                    {
                        timereplacetxt.Text = "06:39:00:AM";
                    }
                    if (settimetxt.Text == "6 40 AM")
                    {
                        timereplacetxt.Text = "06:40:00:AM";
                    }
                    if (settimetxt.Text == "6 41 AM")
                    {
                        timereplacetxt.Text = "06:41:00:AM";
                    }
                    if (settimetxt.Text == "6 42 AM")
                    {
                        timereplacetxt.Text = "06:42:00:AM";
                    }
                    if (settimetxt.Text == "6 43 AM")
                    {
                        timereplacetxt.Text = "06:43:00:AM";
                    }
                    if (settimetxt.Text == "6 44 AM")
                    {
                        timereplacetxt.Text = "06:44:00:AM";
                    }
                    if (settimetxt.Text == "6 45 AM")
                    {
                        timereplacetxt.Text = "06:45:00:AM";
                    }
                    if (settimetxt.Text == "6 46 AM")
                    {
                        timereplacetxt.Text = "06:46:00:AM";
                    }
                    if (settimetxt.Text == "6 47 AM")
                    {
                        timereplacetxt.Text = "06:47:00:AM";
                    }
                    if (settimetxt.Text == "6 48 AM")
                    {
                        timereplacetxt.Text = "06:48:00:AM";
                    }
                    if (settimetxt.Text == "6 49 AM")
                    {
                        timereplacetxt.Text = "06:49:00:AM";
                    }
                    if (settimetxt.Text == "6 50 AM")
                    {
                        timereplacetxt.Text = "06:50:00:AM";
                    }
                    if (settimetxt.Text == "6 51 AM")
                    {
                        timereplacetxt.Text = "06:51:00:AM";
                    }
                    if (settimetxt.Text == "6 52 AM")
                    {
                        timereplacetxt.Text = "06:52:00:AM";
                    }
                    if (settimetxt.Text == "6 53 AM")
                    {
                        timereplacetxt.Text = "06:53:00:AM";
                    }
                    if (settimetxt.Text == "6 54 AM")
                    {
                        timereplacetxt.Text = "06:54:00:AM";
                    }
                    if (settimetxt.Text == "6 55 AM")
                    {
                        timereplacetxt.Text = "06:55:00:AM";
                    }
                    if (settimetxt.Text == "6 56 AM")
                    {
                        timereplacetxt.Text = "06:56:00:AM";
                    }
                    if (settimetxt.Text == "6 57 AM")
                    {
                        timereplacetxt.Text = "06:57:00:AM";
                    }
                    if (settimetxt.Text == "6 58 AM")
                    {
                        timereplacetxt.Text = "06:58:00:AM";
                    }
                    if (settimetxt.Text == "6 59 AM")
                    {
                        timereplacetxt.Text = "06:59:00:AM";
                    }
                    if (settimetxt.Text == "7 AM")
                    {
                        timereplacetxt.Text = "07:00:00:AM";
                    }
                    if (settimetxt.Text == "7 1 AM")
                    {
                        timereplacetxt.Text = "07:01:00:AM";
                    }
                    if (settimetxt.Text == "7 2 AM")
                    {
                        timereplacetxt.Text = "07:02:00:AM";
                    }
                    if (settimetxt.Text == "7 3 AM")
                    {
                        timereplacetxt.Text = "07:03:00:AM";
                    }
                    if (settimetxt.Text == "7 4 AM")
                    {
                        timereplacetxt.Text = "07:04:00:AM";
                    }
                    if (settimetxt.Text == "7 5 AM")
                    {
                        timereplacetxt.Text = "07:05:00:AM";
                    }
                    if (settimetxt.Text == "7 6 AM")
                    {
                        timereplacetxt.Text = "07:06:00:AM";
                    }
                    if (settimetxt.Text == "7 7 AM")
                    {
                        timereplacetxt.Text = "07:07:00:AM";
                    }
                    if (settimetxt.Text == "7 8 AM")
                    {
                        timereplacetxt.Text = "07:08:00:AM";
                    }
                    if (settimetxt.Text == "7 9 AM")
                    {
                        timereplacetxt.Text = "07:09:00:AM";
                    }
                    if (settimetxt.Text == "7 10 AM")
                    {
                        timereplacetxt.Text = "07:10:00:AM";
                    }
                    if (settimetxt.Text == "7 11 AM")
                    {
                        timereplacetxt.Text = "07:11:00:AM";
                    }
                    if (settimetxt.Text == "7 12 AM")
                    {
                        timereplacetxt.Text = "07:12:00:AM";
                    }
                    if (settimetxt.Text == "7 13 AM")
                    {
                        timereplacetxt.Text = "07:13:00:AM";
                    }
                    if (settimetxt.Text == "7 14 AM")
                    {
                        timereplacetxt.Text = "07:14:00:AM";
                    }
                    if (settimetxt.Text == "7 15 AM")
                    {
                        timereplacetxt.Text = "07:15:00:AM";
                    }
                    if (settimetxt.Text == "7 16 AM")
                    {
                        timereplacetxt.Text = "07:16:00:AM";
                    }
                    if (settimetxt.Text == "7 17 AM")
                    {
                        timereplacetxt.Text = "07:17:00:AM";
                    }
                    if (settimetxt.Text == "7 18 AM")
                    {
                        timereplacetxt.Text = "07:18:00:AM";
                    }
                    if (settimetxt.Text == "7 19 AM")
                    {
                        timereplacetxt.Text = "07:19:00:AM";
                    }
                    if (settimetxt.Text == "7 20 AM")
                    {
                        timereplacetxt.Text = "07:20:00:AM";
                    }
                    if (settimetxt.Text == "7 21 AM")
                    {
                        timereplacetxt.Text = "07:21:00:AM";
                    }
                    if (settimetxt.Text == "7 22 AM")
                    {
                        timereplacetxt.Text = "07:22:00:AM";
                    }
                    if (settimetxt.Text == "7 23 AM")
                    {
                        timereplacetxt.Text = "07:23:00:AM";
                    }
                    if (settimetxt.Text == "7 24 AM")
                    {
                        timereplacetxt.Text = "07:24:00:AM";
                    }
                    if (settimetxt.Text == "7 25 AM")
                    {
                        timereplacetxt.Text = "07:25:00:AM";
                    }
                    if (settimetxt.Text == "7 26 AM")
                    {
                        timereplacetxt.Text = "07:26:00:AM";
                    }
                    if (settimetxt.Text == "7 27 AM")
                    {
                        timereplacetxt.Text = "07:27:00:AM";
                    }
                    if (settimetxt.Text == "7 28 AM")
                    {
                        timereplacetxt.Text = "07:28:00:AM";
                    }
                    if (settimetxt.Text == "7 29 AM")
                    {
                        timereplacetxt.Text = "07:29:00:AM";
                    }
                    if (settimetxt.Text == "7 30 AM")
                    {
                        timereplacetxt.Text = "07:30:00:AM";
                    }
                    if (settimetxt.Text == "7 31 AM")
                    {
                        timereplacetxt.Text = "07:31:00:AM";
                    }
                    if (settimetxt.Text == "7 32 AM")
                    {
                        timereplacetxt.Text = "07:32:00:AM";
                    }
                    if (settimetxt.Text == "7 33 AM")
                    {
                        timereplacetxt.Text = "07:33:00:AM";
                    }
                    if (settimetxt.Text == "7 34 AM")
                    {
                        timereplacetxt.Text = "07:34:00:AM";
                    }
                    if (settimetxt.Text == "7 35 AM")
                    {
                        timereplacetxt.Text = "07:35:00:AM";
                    }
                    if (settimetxt.Text == "7 36 AM")
                    {
                        timereplacetxt.Text = "07:36:00:AM";
                    }
                    if (settimetxt.Text == "7 37 AM")
                    {
                        timereplacetxt.Text = "07:37:00:AM";
                    }
                    if (settimetxt.Text == "7 38 AM")
                    {
                        timereplacetxt.Text = "07:38:00:AM";
                    }
                    if (settimetxt.Text == "7 39 AM")
                    {
                        timereplacetxt.Text = "07:39:00:AM";
                    }
                    if (settimetxt.Text == "7 40 AM")
                    {
                        timereplacetxt.Text = "07:40:00:AM";
                    }
                    if (settimetxt.Text == "7 41 AM")
                    {
                        timereplacetxt.Text = "07:41:00:AM";
                    }
                    if (settimetxt.Text == "7 42 AM")
                    {
                        timereplacetxt.Text = "07:42:00:AM";
                    }
                    if (settimetxt.Text == "7 43 AM")
                    {
                        timereplacetxt.Text = "07:43:00:AM";
                    }
                    if (settimetxt.Text == "7 44 AM")
                    {
                        timereplacetxt.Text = "07:44:00:AM";
                    }
                    if (settimetxt.Text == "7 45 AM")
                    {
                        timereplacetxt.Text = "07:45:00:AM";
                    }
                    if (settimetxt.Text == "7 46 AM")
                    {
                        timereplacetxt.Text = "07:46:00:AM";
                    }
                    if (settimetxt.Text == "7 47 AM")
                    {
                        timereplacetxt.Text = "07:47:00:AM";
                    }
                    if (settimetxt.Text == "7 48 AM")
                    {
                        timereplacetxt.Text = "07:48:00:AM";
                    }
                    if (settimetxt.Text == "7 49 AM")
                    {
                        timereplacetxt.Text = "07:49:00:AM";
                    }
                    if (settimetxt.Text == "7 50 AM")
                    {
                        timereplacetxt.Text = "07:50:00:AM";
                    }
                    if (settimetxt.Text == "7 51 AM")
                    {
                        timereplacetxt.Text = "07:51:00:AM";
                    }
                    if (settimetxt.Text == "7 52 AM")
                    {
                        timereplacetxt.Text = "07:52:00:AM";
                    }
                    if (settimetxt.Text == "7 53 AM")
                    {
                        timereplacetxt.Text = "07:53:00:AM";
                    }
                    if (settimetxt.Text == "7 54 AM")
                    {
                        timereplacetxt.Text = "07:54:00:AM";
                    }
                    if (settimetxt.Text == "7 55 AM")
                    {
                        timereplacetxt.Text = "07:55:00:AM";
                    }
                    if (settimetxt.Text == "7 56 AM")
                    {
                        timereplacetxt.Text = "07:56:00:AM";
                    }
                    if (settimetxt.Text == "7 57 AM")
                    {
                        timereplacetxt.Text = "07:57:00:AM";
                    }
                    if (settimetxt.Text == "7 58 AM")
                    {
                        timereplacetxt.Text = "07:58:00:AM";
                    }
                    if (settimetxt.Text == "7 59 AM")
                    {
                        timereplacetxt.Text = "07:59:00:AM";
                    }
                    if (settimetxt.Text == "8 AM")
                    {
                        timereplacetxt.Text = "08:00:00:AM";
                    }
                    if (settimetxt.Text == "8 1 AM")
                    {
                        timereplacetxt.Text = "08:01:00:AM";
                    }
                    if (settimetxt.Text == "8 2 AM")
                    {
                        timereplacetxt.Text = "08:02:00:AM";
                    }
                    if (settimetxt.Text == "8 3 AM")
                    {
                        timereplacetxt.Text = "08:03:00:AM";
                    }
                    if (settimetxt.Text == "8 4 AM")
                    {
                        timereplacetxt.Text = "08:04:00:AM";
                    }
                    if (settimetxt.Text == "8 5 AM")
                    {
                        timereplacetxt.Text = "08:05:00:AM";
                    }
                    if (settimetxt.Text == "8 6 AM")
                    {
                        timereplacetxt.Text = "08:06:00:AM";
                    }
                    if (settimetxt.Text == "8 7 AM")
                    {
                        timereplacetxt.Text = "08:07:00:AM";
                    }
                    if (settimetxt.Text == "8 8 AM")
                    {
                        timereplacetxt.Text = "08:08:00:AM";
                    }
                    if (settimetxt.Text == "8 9 AM")
                    {
                        timereplacetxt.Text = "08:09:00:AM";
                    }
                    if (settimetxt.Text == "8 10 AM")
                    {
                        timereplacetxt.Text = "08:10:00:AM";
                    }
                    if (settimetxt.Text == "8 11 AM")
                    {
                        timereplacetxt.Text = "08:11:00:AM";
                    }
                    if (settimetxt.Text == "8 12 AM")
                    {
                        timereplacetxt.Text = "08:12:00:AM";
                    }
                    if (settimetxt.Text == "8 13 AM")
                    {
                        timereplacetxt.Text = "08:13:00:AM";
                    }
                    if (settimetxt.Text == "8 14 AM")
                    {
                        timereplacetxt.Text = "08:14:00:AM";
                    }
                    if (settimetxt.Text == "8 15 AM")
                    {
                        timereplacetxt.Text = "08:15:00:AM";
                    }
                    if (settimetxt.Text == "8 16 AM")
                    {
                        timereplacetxt.Text = "08:16:00:AM";
                    }
                    if (settimetxt.Text == "8 17 AM")
                    {
                        timereplacetxt.Text = "08:17:00:AM";
                    }
                    if (settimetxt.Text == "8 18 AM")
                    {
                        timereplacetxt.Text = "08:18:00:AM";
                    }
                    if (settimetxt.Text == "8 19 AM")
                    {
                        timereplacetxt.Text = "08:19:00:AM";
                    }
                    if (settimetxt.Text == "8 20 AM")
                    {
                        timereplacetxt.Text = "08:20:00:AM";
                    }
                    if (settimetxt.Text == "8 21 AM")
                    {
                        timereplacetxt.Text = "08:21:00:AM";
                    }
                    if (settimetxt.Text == "8 22 AM")
                    {
                        timereplacetxt.Text = "08:22:00:AM";
                    }
                    if (settimetxt.Text == "8 23 AM")
                    {
                        timereplacetxt.Text = "08:23:00:AM";
                    }
                    if (settimetxt.Text == "8 24 AM")
                    {
                        timereplacetxt.Text = "08:24:00:AM";
                    }
                    if (settimetxt.Text == "8 25 AM")
                    {
                        timereplacetxt.Text = "08:25:00:AM";
                    }
                    if (settimetxt.Text == "8 26 AM")
                    {
                        timereplacetxt.Text = "08:26:00:AM";
                    }
                    if (settimetxt.Text == "8 27 AM")
                    {
                        timereplacetxt.Text = "08:27:00:AM";
                    }
                    if (settimetxt.Text == "8 28 AM")
                    {
                        timereplacetxt.Text = "08:28:00:AM";
                    }
                    if (settimetxt.Text == "8 29 AM")
                    {
                        timereplacetxt.Text = "08:29:00:AM";
                    }
                    if (settimetxt.Text == "8 30 AM")
                    {
                        timereplacetxt.Text = "08:30:00:AM";
                    }
                    if (settimetxt.Text == "8 31 AM")
                    {
                        timereplacetxt.Text = "08:31:00:AM";
                    }
                    if (settimetxt.Text == "8 32 AM")
                    {
                        timereplacetxt.Text = "08:32:00:AM";
                    }
                    if (settimetxt.Text == "8 33 AM")
                    {
                        timereplacetxt.Text = "08:33:00:AM";
                    }
                    if (settimetxt.Text == "8 34 AM")
                    {
                        timereplacetxt.Text = "08:34:00:AM";
                    }
                    if (settimetxt.Text == "8 35 AM")
                    {
                        timereplacetxt.Text = "08:35:00:AM";
                    }
                    if (settimetxt.Text == "8 36 AM")
                    {
                        timereplacetxt.Text = "08:36:00:AM";
                    }
                    if (settimetxt.Text == "8 37 AM")
                    {
                        timereplacetxt.Text = "08:37:00:AM";
                    }
                    if (settimetxt.Text == "8 38 AM")
                    {
                        timereplacetxt.Text = "08:38:00:AM";
                    }
                    if (settimetxt.Text == "8 39 AM")
                    {
                        timereplacetxt.Text = "08:39:00:AM";
                    }
                    if (settimetxt.Text == "8 40 AM")
                    {
                        timereplacetxt.Text = "08:40:00:AM";
                    }
                    if (settimetxt.Text == "8 41 AM")
                    {
                        timereplacetxt.Text = "08:41:00:AM";
                    }
                    if (settimetxt.Text == "8 42 AM")
                    {
                        timereplacetxt.Text = "08:42:00:AM";
                    }
                    if (settimetxt.Text == "8 43 AM")
                    {
                        timereplacetxt.Text = "08:43:00:AM";
                    }
                    if (settimetxt.Text == "8 44 AM")
                    {
                        timereplacetxt.Text = "08:44:00:AM";
                    }
                    if (settimetxt.Text == "8 45 AM")
                    {
                        timereplacetxt.Text = "08:45:00:AM";
                    }
                    if (settimetxt.Text == "8 46 AM")
                    {
                        timereplacetxt.Text = "08:46:00:AM";
                    }
                    if (settimetxt.Text == "8 47 AM")
                    {
                        timereplacetxt.Text = "08:47:00:AM";
                    }
                    if (settimetxt.Text == "8 48 AM")
                    {
                        timereplacetxt.Text = "08:48:00:AM";
                    }
                    if (settimetxt.Text == "8 49 AM")
                    {
                        timereplacetxt.Text = "08:49:00:AM";
                    }
                    if (settimetxt.Text == "8 50 AM")
                    {
                        timereplacetxt.Text = "08:50:00:AM";
                    }
                    if (settimetxt.Text == "8 51 AM")
                    {
                        timereplacetxt.Text = "08:51:00:AM";
                    }
                    if (settimetxt.Text == "8 52 AM")
                    {
                        timereplacetxt.Text = "08:52:00:AM";
                    }
                    if (settimetxt.Text == "8 53 AM")
                    {
                        timereplacetxt.Text = "08:53:00:AM";
                    }
                    if (settimetxt.Text == "8 54 AM")
                    {
                        timereplacetxt.Text = "08:54:00:AM";
                    }
                    if (settimetxt.Text == "8 55 AM")
                    {
                        timereplacetxt.Text = "08:55:00:AM";
                    }
                    if (settimetxt.Text == "8 56 AM")
                    {
                        timereplacetxt.Text = "08:56:00:AM";
                    }
                    if (settimetxt.Text == "8 57 AM")
                    {
                        timereplacetxt.Text = "08:57:00:AM";
                    }
                    if (settimetxt.Text == "8 58 AM")
                    {
                        timereplacetxt.Text = "08:58:00:AM";
                    }
                    if (settimetxt.Text == "8 59 AM")
                    {
                        timereplacetxt.Text = "08:59:00:AM";
                    }
                    if (settimetxt.Text == "9 AM")
                    {
                        timereplacetxt.Text = "09:00:00:AM";
                    }
                    if (settimetxt.Text == "9 1 AM")
                    {
                        timereplacetxt.Text = "09:01:00:AM";
                    }
                    if (settimetxt.Text == "9 2 AM")
                    {
                        timereplacetxt.Text = "09:02:00:AM";
                    }
                    if (settimetxt.Text == "9 3 AM")
                    {
                        timereplacetxt.Text = "09:03:00:AM";
                    }
                    if (settimetxt.Text == "9 4 AM")
                    {
                        timereplacetxt.Text = "09:04:00:AM";
                    }
                    if (settimetxt.Text == "9 5 AM")
                    {
                        timereplacetxt.Text = "09:05:00:AM";
                    }
                    if (settimetxt.Text == "9 6 AM")
                    {
                        timereplacetxt.Text = "09:06:00:AM";
                    }
                    if (settimetxt.Text == "9 7 AM")
                    {
                        timereplacetxt.Text = "09:07:00:AM";
                    }
                    if (settimetxt.Text == "9 8 AM")
                    {
                        timereplacetxt.Text = "09:08:00:AM";
                    }
                    if (settimetxt.Text == "9 9 AM")
                    {
                        timereplacetxt.Text = "09:09:00:AM";
                    }
                    if (settimetxt.Text == "9 10 AM")
                    {
                        timereplacetxt.Text = "09:10:00:AM";
                    }
                    if (settimetxt.Text == "9 11 AM")
                    {
                        timereplacetxt.Text = "09:11:00:AM";
                    }
                    if (settimetxt.Text == "9 12 AM")
                    {
                        timereplacetxt.Text = "09:12:00:AM";
                    }
                    if (settimetxt.Text == "9 13 AM")
                    {
                        timereplacetxt.Text = "09:13:00:AM";
                    }
                    if (settimetxt.Text == "9 14 AM")
                    {
                        timereplacetxt.Text = "09:14:00:AM";
                    }
                    if (settimetxt.Text == "9 15 AM")
                    {
                        timereplacetxt.Text = "09:15:00:AM";
                    }
                    if (settimetxt.Text == "9 16 AM")
                    {
                        timereplacetxt.Text = "09:16:00:AM";
                    }
                    if (settimetxt.Text == "9 17 AM")
                    {
                        timereplacetxt.Text = "09:17:00:AM";
                    }
                    if (settimetxt.Text == "9 18 AM")
                    {
                        timereplacetxt.Text = "09:18:00:AM";
                    }
                    if (settimetxt.Text == "9 19 AM")
                    {
                        timereplacetxt.Text = "09:19:00:AM";
                    }
                    if (settimetxt.Text == "9 20 AM")
                    {
                        timereplacetxt.Text = "09:20:00:AM";
                    }
                    if (settimetxt.Text == "9 21 AM")
                    {
                        timereplacetxt.Text = "09:21:00:AM";
                    }
                    if (settimetxt.Text == "9 22 AM")
                    {
                        timereplacetxt.Text = "09:22:00:AM";
                    }
                    if (settimetxt.Text == "9 23 AM")
                    {
                        timereplacetxt.Text = "09:23:00:AM";
                    }
                    if (settimetxt.Text == "9 24 AM")
                    {
                        timereplacetxt.Text = "09:24:00:AM";
                    }
                    if (settimetxt.Text == "9 25 AM")
                    {
                        timereplacetxt.Text = "09:25:00:AM";
                    }
                    if (settimetxt.Text == "9 26 AM")
                    {
                        timereplacetxt.Text = "09:26:00:AM";
                    }
                    if (settimetxt.Text == "9 27 AM")
                    {
                        timereplacetxt.Text = "09:27:00:AM";
                    }
                    if (settimetxt.Text == "9 28 AM")
                    {
                        timereplacetxt.Text = "09:28:00:AM";
                    }
                    if (settimetxt.Text == "9 29 AM")
                    {
                        timereplacetxt.Text = "09:29:00:AM";
                    }
                    if (settimetxt.Text == "9 30 AM")
                    {
                        timereplacetxt.Text = "09:30:00:AM";
                    }
                    if (settimetxt.Text == "9 31 AM")
                    {
                        timereplacetxt.Text = "09:31:00:AM";
                    }
                    if (settimetxt.Text == "9 32 AM")
                    {
                        timereplacetxt.Text = "09:32:00:AM";
                    }
                    if (settimetxt.Text == "9 33 AM")
                    {
                        timereplacetxt.Text = "09:33:00:AM";
                    }
                    if (settimetxt.Text == "9 34 AM")
                    {
                        timereplacetxt.Text = "09:34:00:AM";
                    }
                    if (settimetxt.Text == "9 35 AM")
                    {
                        timereplacetxt.Text = "09:35:00:AM";
                    }
                    if (settimetxt.Text == "9 36 AM")
                    {
                        timereplacetxt.Text = "09:36:00:AM";
                    }
                    if (settimetxt.Text == "9 37 AM")
                    {
                        timereplacetxt.Text = "09:37:00:AM";
                    }
                    if (settimetxt.Text == "9 38 AM")
                    {
                        timereplacetxt.Text = "09:38:00:AM";
                    }
                    if (settimetxt.Text == "9 39 AM")
                    {
                        timereplacetxt.Text = "09:39:00:AM";
                    }
                    if (settimetxt.Text == "9 40 AM")
                    {
                        timereplacetxt.Text = "09:40:00:AM";
                    }
                    if (settimetxt.Text == "9 41 AM")
                    {
                        timereplacetxt.Text = "09:41:00:AM";
                    }
                    if (settimetxt.Text == "9 42 AM")
                    {
                        timereplacetxt.Text = "09:42:00:AM";
                    }
                    if (settimetxt.Text == "9 43 AM")
                    {
                        timereplacetxt.Text = "09:43:00:AM";
                    }
                    if (settimetxt.Text == "9 44 AM")
                    {
                        timereplacetxt.Text = "09:44:00:AM";
                    }
                    if (settimetxt.Text == "9 45 AM")
                    {
                        timereplacetxt.Text = "09:45:00:AM";
                    }
                    if (settimetxt.Text == "9 46 AM")
                    {
                        timereplacetxt.Text = "09:46:00:AM";
                    }
                    if (settimetxt.Text == "9 47 AM")
                    {
                        timereplacetxt.Text = "09:47:00:AM";
                    }
                    if (settimetxt.Text == "9 48 AM")
                    {
                        timereplacetxt.Text = "09:48:00:AM";
                    }
                    if (settimetxt.Text == "9 49 AM")
                    {
                        timereplacetxt.Text = "09:49:00:AM";
                    }
                    if (settimetxt.Text == "9 50 AM")
                    {
                        timereplacetxt.Text = "09:50:00:AM";
                    }
                    if (settimetxt.Text == "9 51 AM")
                    {
                        timereplacetxt.Text = "09:51:00:AM";
                    }
                    if (settimetxt.Text == "9 52 AM")
                    {
                        timereplacetxt.Text = "09:52:00:AM";
                    }
                    if (settimetxt.Text == "9 53 AM")
                    {
                        timereplacetxt.Text = "09:53:00:AM";
                    }
                    if (settimetxt.Text == "9 54 AM")
                    {
                        timereplacetxt.Text = "09:54:00:AM";
                    }
                    if (settimetxt.Text == "9 55 AM")
                    {
                        timereplacetxt.Text = "09:55:00:AM";
                    }
                    if (settimetxt.Text == "9 56 AM")
                    {
                        timereplacetxt.Text = "09:56:00:AM";
                    }
                    if (settimetxt.Text == "9 57 AM")
                    {
                        timereplacetxt.Text = "09:57:00:AM";
                    }
                    if (settimetxt.Text == "9 58 AM")
                    {
                        timereplacetxt.Text = "09:58:00:AM";
                    }
                    if (settimetxt.Text == "9 59 AM")
                    {
                        timereplacetxt.Text = "09:59:00:AM";
                    }
                    if (settimetxt.Text == "10 AM")
                    {
                        timereplacetxt.Text = "10:00:00:AM";
                    }
                    if (settimetxt.Text == "10 1 AM")
                    {
                        timereplacetxt.Text = "10:01:00:AM";
                    }
                    if (settimetxt.Text == "10 2 AM")
                    {
                        timereplacetxt.Text = "10:02:00:AM";
                    }
                    if (settimetxt.Text == "10 3 AM")
                    {
                        timereplacetxt.Text = "10:03:00:AM";
                    }
                    if (settimetxt.Text == "10 4 AM")
                    {
                        timereplacetxt.Text = "10:04:00:AM";
                    }
                    if (settimetxt.Text == "10 5 AM")
                    {
                        timereplacetxt.Text = "10:05:00:AM";
                    }
                    if (settimetxt.Text == "10 6 AM")
                    {
                        timereplacetxt.Text = "10:06:00:AM";
                    }
                    if (settimetxt.Text == "10 7 AM")
                    {
                        timereplacetxt.Text = "10:07:00:AM";
                    }
                    if (settimetxt.Text == "10 8 AM")
                    {
                        timereplacetxt.Text = "10:08:00:AM";
                    }
                    if (settimetxt.Text == "10 9 AM")
                    {
                        timereplacetxt.Text = "10:09:00:AM";
                    }
                    if (settimetxt.Text == "10 10 AM")
                    {
                        timereplacetxt.Text = "10:10:00:AM";
                    }
                    if (settimetxt.Text == "10 11 AM")
                    {
                        timereplacetxt.Text = "10:11:00:AM";
                    }
                    if (settimetxt.Text == "10 12 AM")
                    {
                        timereplacetxt.Text = "10:12:00:AM";
                    }
                    if (settimetxt.Text == "10 13 AM")
                    {
                        timereplacetxt.Text = "10:13:00:AM";
                    }
                    if (settimetxt.Text == "10 14 AM")
                    {
                        timereplacetxt.Text = "10:14:00:AM";
                    }
                    if (settimetxt.Text == "10 15 AM")
                    {
                        timereplacetxt.Text = "10:15:00:AM";
                    }
                    if (settimetxt.Text == "10 16 AM")
                    {
                        timereplacetxt.Text = "10:16:00:AM";
                    }
                    if (settimetxt.Text == "10 17 AM")
                    {
                        timereplacetxt.Text = "10:17:00:AM";
                    }
                    if (settimetxt.Text == "10 18 AM")
                    {
                        timereplacetxt.Text = "10:18:00:AM";
                    }
                    if (settimetxt.Text == "10 19 AM")
                    {
                        timereplacetxt.Text = "10:19:00:AM";
                    }
                    if (settimetxt.Text == "10 20 AM")
                    {
                        timereplacetxt.Text = "10:20:00:AM";
                    }
                    if (settimetxt.Text == "10 21 AM")
                    {
                        timereplacetxt.Text = "10:21:00:AM";
                    }
                    if (settimetxt.Text == "10 22 AM")
                    {
                        timereplacetxt.Text = "10:22:00:AM";
                    }
                    if (settimetxt.Text == "10 23 AM")
                    {
                        timereplacetxt.Text = "10:23:00:AM";
                    }
                    if (settimetxt.Text == "10 24 AM")
                    {
                        timereplacetxt.Text = "10:24:00:AM";
                    }
                    if (settimetxt.Text == "10 25 AM")
                    {
                        timereplacetxt.Text = "10:25:00:AM";
                    }
                    if (settimetxt.Text == "10 26 AM")
                    {
                        timereplacetxt.Text = "10:26:00:AM";
                    }
                    if (settimetxt.Text == "10 27 AM")
                    {
                        timereplacetxt.Text = "10:27:00:AM";
                    }
                    if (settimetxt.Text == "10 28 AM")
                    {
                        timereplacetxt.Text = "10:28:00:AM";
                    }
                    if (settimetxt.Text == "10 29 AM")
                    {
                        timereplacetxt.Text = "10:29:00:AM";
                    }
                    if (settimetxt.Text == "10 30 AM")
                    {
                        timereplacetxt.Text = "10:30:00:AM";
                    }
                    if (settimetxt.Text == "10 31 AM")
                    {
                        timereplacetxt.Text = "10:31:00:AM";
                    }
                    if (settimetxt.Text == "10 32 AM")
                    {
                        timereplacetxt.Text = "10:32:00:AM";
                    }
                    if (settimetxt.Text == "10 33 AM")
                    {
                        timereplacetxt.Text = "10:33:00:AM";
                    }
                    if (settimetxt.Text == "10 34 AM")
                    {
                        timereplacetxt.Text = "10:34:00:AM";
                    }
                    if (settimetxt.Text == "10 35 AM")
                    {
                        timereplacetxt.Text = "10:35:00:AM";
                    }
                    if (settimetxt.Text == "10 36 AM")
                    {
                        timereplacetxt.Text = "10:36:00:AM";
                    }
                    if (settimetxt.Text == "10 37 AM")
                    {
                        timereplacetxt.Text = "10:37:00:AM";
                    }
                    if (settimetxt.Text == "10 38 AM")
                    {
                        timereplacetxt.Text = "10:38:00:AM";
                    }
                    if (settimetxt.Text == "10 39 AM")
                    {
                        timereplacetxt.Text = "10:39:00:AM";
                    }
                    if (settimetxt.Text == "10 40 AM")
                    {
                        timereplacetxt.Text = "10:40:00:AM";
                    }
                    if (settimetxt.Text == "10 41 AM")
                    {
                        timereplacetxt.Text = "10:41:00:AM";
                    }
                    if (settimetxt.Text == "10 42 AM")
                    {
                        timereplacetxt.Text = "10:42:00:AM";
                    }
                    if (settimetxt.Text == "10 43 AM")
                    {
                        timereplacetxt.Text = "10:43:00:AM";
                    }
                    if (settimetxt.Text == "10 44 AM")
                    {
                        timereplacetxt.Text = "10:44:00:AM";
                    }
                    if (settimetxt.Text == "10 45 AM")
                    {
                        timereplacetxt.Text = "10:45:00:AM";
                    }
                    if (settimetxt.Text == "10 46 AM")
                    {
                        timereplacetxt.Text = "10:46:00:AM";
                    }
                    if (settimetxt.Text == "10 47 AM")
                    {
                        timereplacetxt.Text = "10:47:00:AM";
                    }
                    if (settimetxt.Text == "10 48 AM")
                    {
                        timereplacetxt.Text = "10:48:00:AM";
                    }
                    if (settimetxt.Text == "10 49 AM")
                    {
                        timereplacetxt.Text = "10:49:00:AM";
                    }
                    if (settimetxt.Text == "10 50 AM")
                    {
                        timereplacetxt.Text = "10:50:00:AM";
                    }
                    if (settimetxt.Text == "10 51 AM")
                    {
                        timereplacetxt.Text = "10:51:00:AM";
                    }
                    if (settimetxt.Text == "10 52 AM")
                    {
                        timereplacetxt.Text = "10:52:00:AM";
                    }
                    if (settimetxt.Text == "10 53 AM")
                    {
                        timereplacetxt.Text = "10:53:00:AM";
                    }
                    if (settimetxt.Text == "10 54 AM")
                    {
                        timereplacetxt.Text = "10:54:00:AM";
                    }
                    if (settimetxt.Text == "10 55 AM")
                    {
                        timereplacetxt.Text = "10:55:00:AM";
                    }
                    if (settimetxt.Text == "10 56 AM")
                    {
                        timereplacetxt.Text = "10:56:00:AM";
                    }
                    if (settimetxt.Text == "10 57 AM")
                    {
                        timereplacetxt.Text = "10:57:00:AM";
                    }
                    if (settimetxt.Text == "10 58 AM")
                    {
                        timereplacetxt.Text = "10:58:00:AM";
                    }
                    if (settimetxt.Text == "10 59 AM")
                    {
                        timereplacetxt.Text = "10:59:00:AM";
                    }
                    if (settimetxt.Text == "11 AM")
                    {
                        timereplacetxt.Text = "11:00:00:AM";
                    }
                    if (settimetxt.Text == "11 1 AM")
                    {
                        timereplacetxt.Text = "11:01:00:AM";
                    }
                    if (settimetxt.Text == "11 2 AM")
                    {
                        timereplacetxt.Text = "11:02:00:AM";
                    }
                    if (settimetxt.Text == "11 3 AM")
                    {
                        timereplacetxt.Text = "11:03:00:AM";
                    }
                    if (settimetxt.Text == "11 4 AM")
                    {
                        timereplacetxt.Text = "11:04:00:AM";
                    }
                    if (settimetxt.Text == "11 5 AM")
                    {
                        timereplacetxt.Text = "11:05:00:AM";
                    }
                    if (settimetxt.Text == "11 6 AM")
                    {
                        timereplacetxt.Text = "11:06:00:AM";
                    }
                    if (settimetxt.Text == "11 7 AM")
                    {
                        timereplacetxt.Text = "11:07:00:AM";
                    }
                    if (settimetxt.Text == "11 8 AM")
                    {
                        timereplacetxt.Text = "11:08:00:AM";
                    }
                    if (settimetxt.Text == "11 9 AM")
                    {
                        timereplacetxt.Text = "11:09:00:AM";
                    }
                    if (settimetxt.Text == "11 10 AM")
                    {
                        timereplacetxt.Text = "11:10:00:AM";
                    }
                    if (settimetxt.Text == "11 11 AM")
                    {
                        timereplacetxt.Text = "11:11:00:AM";
                    }
                    if (settimetxt.Text == "11 12 AM")
                    {
                        timereplacetxt.Text = "11:12:00:AM";
                    }
                    if (settimetxt.Text == "11 13 AM")
                    {
                        timereplacetxt.Text = "11:13:00:AM";
                    }
                    if (settimetxt.Text == "11 14 AM")
                    {
                        timereplacetxt.Text = "11:14:00:AM";
                    }
                    if (settimetxt.Text == "11 15 AM")
                    {
                        timereplacetxt.Text = "11:15:00:AM";
                    }
                    if (settimetxt.Text == "11 16 AM")
                    {
                        timereplacetxt.Text = "11:16:00:AM";
                    }
                    if (settimetxt.Text == "11 17 AM")
                    {
                        timereplacetxt.Text = "11:17:00:AM";
                    }
                    if (settimetxt.Text == "11 18 AM")
                    {
                        timereplacetxt.Text = "11:18:00:AM";
                    }
                    if (settimetxt.Text == "11 19 AM")
                    {
                        timereplacetxt.Text = "11:19:00:AM";
                    }
                    if (settimetxt.Text == "11 20 AM")
                    {
                        timereplacetxt.Text = "11:20:00:AM";
                    }
                    if (settimetxt.Text == "11 21 AM")
                    {
                        timereplacetxt.Text = "11:21:00:AM";
                    }
                    if (settimetxt.Text == "11 22 AM")
                    {
                        timereplacetxt.Text = "11:22:00:AM";
                    }
                    if (settimetxt.Text == "11 23 AM")
                    {
                        timereplacetxt.Text = "11:23:00:AM";
                    }
                    if (settimetxt.Text == "11 24 AM")
                    {
                        timereplacetxt.Text = "11:24:00:AM";
                    }
                    if (settimetxt.Text == "11 25 AM")
                    {
                        timereplacetxt.Text = "11:25:00:AM";
                    }
                    if (settimetxt.Text == "11 26 AM")
                    {
                        timereplacetxt.Text = "11:26:00:AM";
                    }
                    if (settimetxt.Text == "11 27 AM")
                    {
                        timereplacetxt.Text = "11:27:00:AM";
                    }
                    if (settimetxt.Text == "11 28 AM")
                    {
                        timereplacetxt.Text = "11:28:00:AM";
                    }
                    if (settimetxt.Text == "11 29 AM")
                    {
                        timereplacetxt.Text = "11:29:00:AM";
                    }
                    if (settimetxt.Text == "11 30 AM")
                    {
                        timereplacetxt.Text = "11:30:00:AM";
                    }
                    if (settimetxt.Text == "11 31 AM")
                    {
                        timereplacetxt.Text = "11:31:00:AM";
                    }
                    if (settimetxt.Text == "11 32 AM")
                    {
                        timereplacetxt.Text = "11:32:00:AM";
                    }
                    if (settimetxt.Text == "11 33 AM")
                    {
                        timereplacetxt.Text = "11:33:00:AM";
                    }
                    if (settimetxt.Text == "11 34 AM")
                    {
                        timereplacetxt.Text = "11:34:00:AM";
                    }
                    if (settimetxt.Text == "11 35 AM")
                    {
                        timereplacetxt.Text = "11:35:00:AM";
                    }
                    if (settimetxt.Text == "11 36 AM")
                    {
                        timereplacetxt.Text = "11:36:00:AM";
                    }
                    if (settimetxt.Text == "11 37 AM")
                    {
                        timereplacetxt.Text = "11:37:00:AM";
                    }
                    if (settimetxt.Text == "11 38 AM")
                    {
                        timereplacetxt.Text = "11:38:00:AM";
                    }
                    if (settimetxt.Text == "11 39 AM")
                    {
                        timereplacetxt.Text = "11:39:00:AM";
                    }
                    if (settimetxt.Text == "11 40 AM")
                    {
                        timereplacetxt.Text = "11:40:00:AM";
                    }
                    if (settimetxt.Text == "11 41 AM")
                    {
                        timereplacetxt.Text = "11:41:00:AM";
                    }
                    if (settimetxt.Text == "11 42 AM")
                    {
                        timereplacetxt.Text = "11:42:00:AM";
                    }
                    if (settimetxt.Text == "11 43 AM")
                    {
                        timereplacetxt.Text = "11:43:00:AM";
                    }
                    if (settimetxt.Text == "11 44 AM")
                    {
                        timereplacetxt.Text = "11:44:00:AM";
                    }
                    if (settimetxt.Text == "11 45 AM")
                    {
                        timereplacetxt.Text = "11:45:00:AM";
                    }
                    if (settimetxt.Text == "11 46 AM")
                    {
                        timereplacetxt.Text = "11:46:00:AM";
                    }
                    if (settimetxt.Text == "11 47 AM")
                    {
                        timereplacetxt.Text = "11:47:00:AM";
                    }
                    if (settimetxt.Text == "11 48 AM")
                    {
                        timereplacetxt.Text = "11:48:00:AM";
                    }
                    if (settimetxt.Text == "11 49 AM")
                    {
                        timereplacetxt.Text = "11:49:00:AM";
                    }
                    if (settimetxt.Text == "11 50 AM")
                    {
                        timereplacetxt.Text = "11:50:00:AM";
                    }
                    if (settimetxt.Text == "11 51 AM")
                    {
                        timereplacetxt.Text = "11:51:00:AM";
                    }
                    if (settimetxt.Text == "11 52 AM")
                    {
                        timereplacetxt.Text = "11:52:00:AM";
                    }
                    if (settimetxt.Text == "11 53 AM")
                    {
                        timereplacetxt.Text = "11:53:00:AM";
                    }
                    if (settimetxt.Text == "11 54 AM")
                    {
                        timereplacetxt.Text = "11:54:00:AM";
                    }
                    if (settimetxt.Text == "11 55 AM")
                    {
                        timereplacetxt.Text = "11:55:00:AM";
                    }
                    if (settimetxt.Text == "11 56 AM")
                    {
                        timereplacetxt.Text = "11:56:00:AM";
                    }
                    if (settimetxt.Text == "11 57 AM")
                    {
                        timereplacetxt.Text = "11:57:00:AM";
                    }
                    if (settimetxt.Text == "11 58 AM")
                    {
                        timereplacetxt.Text = "11:58:00:AM";
                    }
                    if (settimetxt.Text == "11 59 AM")
                    {
                        timereplacetxt.Text = "11:59:00:AM";
                    }
                    if (settimetxt.Text == "12 AM")
                    {
                        timereplacetxt.Text = "12:00:00:AM";
                    }
                    if (settimetxt.Text == "12 1 AM")
                    {
                        timereplacetxt.Text = "12:01:00:AM";
                    }
                    if (settimetxt.Text == "12 2 AM")
                    {
                        timereplacetxt.Text = "12:02:00:AM";
                    }
                    if (settimetxt.Text == "12 3 AM")
                    {
                        timereplacetxt.Text = "12:03:00:AM";
                    }
                    if (settimetxt.Text == "12 4 AM")
                    {
                        timereplacetxt.Text = "12:04:00:AM";
                    }
                    if (settimetxt.Text == "12 5 AM")
                    {
                        timereplacetxt.Text = "12:05:00:AM";
                    }
                    if (settimetxt.Text == "12 6 AM")
                    {
                        timereplacetxt.Text = "12:06:00:AM";
                    }
                    if (settimetxt.Text == "12 7 AM")
                    {
                        timereplacetxt.Text = "12:07:00:AM";
                    }
                    if (settimetxt.Text == "12 8 AM")
                    {
                        timereplacetxt.Text = "12:08:00:AM";
                    }
                    if (settimetxt.Text == "12 9 AM")
                    {
                        timereplacetxt.Text = "12:09:00:AM";
                    }
                    if (settimetxt.Text == "12 10 AM")
                    {
                        timereplacetxt.Text = "12:10:00:AM";
                    }
                    if (settimetxt.Text == "12 11 AM")
                    {
                        timereplacetxt.Text = "12:11:00:AM";
                    }
                    if (settimetxt.Text == "12 12 AM")
                    {
                        timereplacetxt.Text = "12:12:00:AM";
                    }
                    if (settimetxt.Text == "12 13 AM")
                    {
                        timereplacetxt.Text = "12:13:00:AM";
                    }
                    if (settimetxt.Text == "12 14 AM")
                    {
                        timereplacetxt.Text = "12:14:00:AM";
                    }
                    if (settimetxt.Text == "12 15 AM")
                    {
                        timereplacetxt.Text = "12:15:00:AM";
                    }
                    if (settimetxt.Text == "12 16 AM")
                    {
                        timereplacetxt.Text = "12:16:00:AM";
                    }
                    if (settimetxt.Text == "12 17 AM")
                    {
                        timereplacetxt.Text = "12:17:00:AM";
                    }
                    if (settimetxt.Text == "12 18 AM")
                    {
                        timereplacetxt.Text = "12:18:00:AM";
                    }
                    if (settimetxt.Text == "12 19 AM")
                    {
                        timereplacetxt.Text = "12:19:00:AM";
                    }
                    if (settimetxt.Text == "12 20 AM")
                    {
                        timereplacetxt.Text = "12:20:00:AM";
                    }
                    if (settimetxt.Text == "12 21 AM")
                    {
                        timereplacetxt.Text = "12:21:00:AM";
                    }
                    if (settimetxt.Text == "12 22 AM")
                    {
                        timereplacetxt.Text = "12:22:00:AM";
                    }
                    if (settimetxt.Text == "12 23 AM")
                    {
                        timereplacetxt.Text = "12:23:00:AM";
                    }
                    if (settimetxt.Text == "12 24 AM")
                    {
                        timereplacetxt.Text = "12:24:00:AM";
                    }
                    if (settimetxt.Text == "12 25 AM")
                    {
                        timereplacetxt.Text = "12:25:00:AM";
                    }
                    if (settimetxt.Text == "12 26 AM")
                    {
                        timereplacetxt.Text = "12:26:00:AM";
                    }
                    if (settimetxt.Text == "12 27 AM")
                    {
                        timereplacetxt.Text = "12:27:00:AM";
                    }
                    if (settimetxt.Text == "12 28 AM")
                    {
                        timereplacetxt.Text = "12:28:00:AM";
                    }
                    if (settimetxt.Text == "12 29 AM")
                    {
                        timereplacetxt.Text = "12:29:00:AM";
                    }
                    if (settimetxt.Text == "12 30 AM")
                    {
                        timereplacetxt.Text = "12:30:00:AM";
                    }
                    if (settimetxt.Text == "12 31 AM")
                    {
                        timereplacetxt.Text = "12:31:00:AM";
                    }
                    if (settimetxt.Text == "12 32 AM")
                    {
                        timereplacetxt.Text = "12:32:00:AM";
                    }
                    if (settimetxt.Text == "12 33 AM")
                    {
                        timereplacetxt.Text = "12:33:00:AM";
                    }
                    if (settimetxt.Text == "12 34 AM")
                    {
                        timereplacetxt.Text = "12:34:00:AM";
                    }
                    if (settimetxt.Text == "12 35 AM")
                    {
                        timereplacetxt.Text = "12:35:00:AM";
                    }
                    if (settimetxt.Text == "12 36 AM")
                    {
                        timereplacetxt.Text = "12:36:00:AM";
                    }
                    if (settimetxt.Text == "12 37 AM")
                    {
                        timereplacetxt.Text = "12:37:00:AM";
                    }
                    if (settimetxt.Text == "12 38 AM")
                    {
                        timereplacetxt.Text = "12:38:00:AM";
                    }
                    if (settimetxt.Text == "12 39 AM")
                    {
                        timereplacetxt.Text = "12:39:00:AM";
                    }
                    if (settimetxt.Text == "12 40 AM")
                    {
                        timereplacetxt.Text = "12:40:00:AM";
                    }
                    if (settimetxt.Text == "12 41 AM")
                    {
                        timereplacetxt.Text = "12:41:00:AM";
                    }
                    if (settimetxt.Text == "12 42 AM")
                    {
                        timereplacetxt.Text = "12:42:00:AM";
                    }
                    if (settimetxt.Text == "12 43 AM")
                    {
                        timereplacetxt.Text = "12:43:00:AM";
                    }
                    if (settimetxt.Text == "12 44 AM")
                    {
                        timereplacetxt.Text = "12:44:00:AM";
                    }
                    if (settimetxt.Text == "12 45 AM")
                    {
                        timereplacetxt.Text = "12:45:00:AM";
                    }
                    if (settimetxt.Text == "12 46 AM")
                    {
                        timereplacetxt.Text = "12:46:00:AM";
                    }
                    if (settimetxt.Text == "12 47 AM")
                    {
                        timereplacetxt.Text = "12:47:00:AM";
                    }
                    if (settimetxt.Text == "12 48 AM")
                    {
                        timereplacetxt.Text = "12:48:00:AM";
                    }
                    if (settimetxt.Text == "12 49 AM")
                    {
                        timereplacetxt.Text = "12:49:00:AM";
                    }
                    if (settimetxt.Text == "12 50 AM")
                    {
                        timereplacetxt.Text = "12:50:00:AM";
                    }
                    if (settimetxt.Text == "12 51 AM")
                    {
                        timereplacetxt.Text = "12:51:00:AM";
                    }
                    if (settimetxt.Text == "12 52 AM")
                    {
                        timereplacetxt.Text = "12:52:00:AM";
                    }
                    if (settimetxt.Text == "12 53 AM")
                    {
                        timereplacetxt.Text = "12:53:00:AM";
                    }
                    if (settimetxt.Text == "12 54 AM")
                    {
                        timereplacetxt.Text = "12:54:00:AM";
                    }
                    if (settimetxt.Text == "12 55 AM")
                    {
                        timereplacetxt.Text = "12:55:00:AM";
                    }
                    if (settimetxt.Text == "12 56 AM")
                    {
                        timereplacetxt.Text = "12:56:00:AM";
                    }
                    if (settimetxt.Text == "12 57 AM")
                    {
                        timereplacetxt.Text = "12:57:00:AM";
                    }
                    if (settimetxt.Text == "12 58 AM")
                    {
                        timereplacetxt.Text = "12:58:00:AM";
                    }
                    if (settimetxt.Text == "12 59 AM")
                    {
                        timereplacetxt.Text = "12:59:00:AM";
                    }
                    if (settimetxt.Text == "1 PM")
                    {
                        timereplacetxt.Text = "01:00:00:PM";
                    }
                    if (settimetxt.Text == "1 1 PM")
                    {
                        timereplacetxt.Text = "01:01:00:PM";
                    }
                    if (settimetxt.Text == "1 2 PM")
                    {
                        timereplacetxt.Text = "01:02:00:PM";
                    }
                    if (settimetxt.Text == "1 3 PM")
                    {
                        timereplacetxt.Text = "01:03:00:PM";
                    }
                    if (settimetxt.Text == "1 4 PM")
                    {
                        timereplacetxt.Text = "01:04:00:PM";
                    }
                    if (settimetxt.Text == "1 5 PM")
                    {
                        timereplacetxt.Text = "01:05:00:PM";
                    }
                    if (settimetxt.Text == "1 6 PM")
                    {
                        timereplacetxt.Text = "01:06:00:PM";
                    }
                    if (settimetxt.Text == "1 7 PM")
                    {
                        timereplacetxt.Text = "01:07:00:PM";
                    }
                    if (settimetxt.Text == "1 8 PM")
                    {
                        timereplacetxt.Text = "01:08:00:PM";
                    }
                    if (settimetxt.Text == "1 9 PM")
                    {
                        timereplacetxt.Text = "01:09:00:PM";
                    }
                    if (settimetxt.Text == "1 10 PM")
                    {
                        timereplacetxt.Text = "01:10:00:PM";
                    }
                    if (settimetxt.Text == "1 11 PM")
                    {
                        timereplacetxt.Text = "01:11:00:PM";
                    }
                    if (settimetxt.Text == "1 12 PM")
                    {
                        timereplacetxt.Text = "01:12:00:PM";
                    }
                    if (settimetxt.Text == "1 13 PM")
                    {
                        timereplacetxt.Text = "01:13:00:PM";
                    }
                    if (settimetxt.Text == "1 14 PM")
                    {
                        timereplacetxt.Text = "01:14:00:PM";
                    }
                    if (settimetxt.Text == "1 15 PM")
                    {
                        timereplacetxt.Text = "01:15:00:PM";
                    }
                    if (settimetxt.Text == "1 16 PM")
                    {
                        timereplacetxt.Text = "01:16:00:PM";
                    }
                    if (settimetxt.Text == "1 17 PM")
                    {
                        timereplacetxt.Text = "01:17:00:PM";
                    }
                    if (settimetxt.Text == "1 18 PM")
                    {
                        timereplacetxt.Text = "01:18:00:PM";
                    }
                    if (settimetxt.Text == "1 19 PM")
                    {
                        timereplacetxt.Text = "01:19:00:PM";
                    }
                    if (settimetxt.Text == "1 20 PM")
                    {
                        timereplacetxt.Text = "01:20:00:PM";
                    }
                    if (settimetxt.Text == "1 21 PM")
                    {
                        timereplacetxt.Text = "01:21:00:PM";
                    }
                    if (settimetxt.Text == "1 22 PM")
                    {
                        timereplacetxt.Text = "01:22:00:PM";
                    }
                    if (settimetxt.Text == "1 23 PM")
                    {
                        timereplacetxt.Text = "01:23:00:PM";
                    }
                    if (settimetxt.Text == "1 24 PM")
                    {
                        timereplacetxt.Text = "01:24:00:PM";
                    }
                    if (settimetxt.Text == "1 25 PM")
                    {
                        timereplacetxt.Text = "01:25:00:PM";
                    }
                    if (settimetxt.Text == "1 26 PM")
                    {
                        timereplacetxt.Text = "01:26:00:PM";
                    }
                    if (settimetxt.Text == "1 27 PM")
                    {
                        timereplacetxt.Text = "01:27:00:PM";
                    }
                    if (settimetxt.Text == "1 28 PM")
                    {
                        timereplacetxt.Text = "01:28:00:PM";
                    }
                    if (settimetxt.Text == "1 29 PM")
                    {
                        timereplacetxt.Text = "01:29:00:PM";
                    }
                    if (settimetxt.Text == "1 30 PM")
                    {
                        timereplacetxt.Text = "01:30:00:PM";
                    }
                    if (settimetxt.Text == "1 31 PM")
                    {
                        timereplacetxt.Text = "01:31:00:PM";
                    }
                    if (settimetxt.Text == "1 32 PM")
                    {
                        timereplacetxt.Text = "01:32:00:PM";
                    }
                    if (settimetxt.Text == "1 33 PM")
                    {
                        timereplacetxt.Text = "01:33:00:PM";
                    }
                    if (settimetxt.Text == "1 34 PM")
                    {
                        timereplacetxt.Text = "01:34:00:PM";
                    }
                    if (settimetxt.Text == "1 35 PM")
                    {
                        timereplacetxt.Text = "01:35:00:PM";
                    }
                    if (settimetxt.Text == "1 36 PM")
                    {
                        timereplacetxt.Text = "01:36:00:PM";
                    }
                    if (settimetxt.Text == "1 37 PM")
                    {
                        timereplacetxt.Text = "01:37:00:PM";
                    }
                    if (settimetxt.Text == "1 38 PM")
                    {
                        timereplacetxt.Text = "01:38:00:PM";
                    }
                    if (settimetxt.Text == "1 39 PM")
                    {
                        timereplacetxt.Text = "01:39:00:PM";
                    }
                    if (settimetxt.Text == "1 40 PM")
                    {
                        timereplacetxt.Text = "01:40:00:PM";
                    }
                    if (settimetxt.Text == "1 41 PM")
                    {
                        timereplacetxt.Text = "01:41:00:PM";
                    }
                    if (settimetxt.Text == "1 42 PM")
                    {
                        timereplacetxt.Text = "01:42:00:PM";
                    }
                    if (settimetxt.Text == "1 43 PM")
                    {
                        timereplacetxt.Text = "01:43:00:PM";
                    }
                    if (settimetxt.Text == "1 44 PM")
                    {
                        timereplacetxt.Text = "01:44:00:PM";
                    }
                    if (settimetxt.Text == "1 45 PM")
                    {
                        timereplacetxt.Text = "01:45:00:PM";
                    }
                    if (settimetxt.Text == "1 46 PM")
                    {
                        timereplacetxt.Text = "01:46:00:PM";
                    }
                    if (settimetxt.Text == "1 47 PM")
                    {
                        timereplacetxt.Text = "01:47:00:PM";
                    }
                    if (settimetxt.Text == "1 48 PM")
                    {
                        timereplacetxt.Text = "01:48:00:PM";
                    }
                    if (settimetxt.Text == "1 49 PM")
                    {
                        timereplacetxt.Text = "01:49:00:PM";
                    }
                    if (settimetxt.Text == "1 50 PM")
                    {
                        timereplacetxt.Text = "01:50:00:PM";
                    }
                    if (settimetxt.Text == "1 51 PM")
                    {
                        timereplacetxt.Text = "01:51:00:PM";
                    }
                    if (settimetxt.Text == "1 52 PM")
                    {
                        timereplacetxt.Text = "01:52:00:PM";
                    }
                    if (settimetxt.Text == "1 53 PM")
                    {
                        timereplacetxt.Text = "01:53:00:PM";
                    }
                    if (settimetxt.Text == "1 54 PM")
                    {
                        timereplacetxt.Text = "01:54:00:PM";
                    }
                    if (settimetxt.Text == "1 55 PM")
                    {
                        timereplacetxt.Text = "01:55:00:PM";
                    }
                    if (settimetxt.Text == "1 56 PM")
                    {
                        timereplacetxt.Text = "01:56:00:PM";
                    }
                    if (settimetxt.Text == "1 57 PM")
                    {
                        timereplacetxt.Text = "01:57:00:PM";
                    }
                    if (settimetxt.Text == "1 58 PM")
                    {
                        timereplacetxt.Text = "01:58:00:PM";
                    }
                    if (settimetxt.Text == "1 59 PM")
                    {
                        timereplacetxt.Text = "01:59:00:PM";
                    }
                    if (settimetxt.Text == "2 PM")
                    {
                        timereplacetxt.Text = "02:00:00:PM";
                    }
                    if (settimetxt.Text == "2 1 PM")
                    {
                        timereplacetxt.Text = "02:01:00:PM";
                    }
                    if (settimetxt.Text == "2 2 PM")
                    {
                        timereplacetxt.Text = "02:02:00:PM";
                    }
                    if (settimetxt.Text == "2 3 PM")
                    {
                        timereplacetxt.Text = "02:03:00:PM";
                    }
                    if (settimetxt.Text == "2 4 PM")
                    {
                        timereplacetxt.Text = "02:04:00:PM";
                    }
                    if (settimetxt.Text == "2 5 PM")
                    {
                        timereplacetxt.Text = "02:05:00:PM";
                    }
                    if (settimetxt.Text == "2 6 PM")
                    {
                        timereplacetxt.Text = "02:06:00:PM";
                    }
                    if (settimetxt.Text == "2 7 PM")
                    {
                        timereplacetxt.Text = "02:07:00:PM";
                    }
                    if (settimetxt.Text == "2 8 PM")
                    {
                        timereplacetxt.Text = "02:08:00:PM";
                    }
                    if (settimetxt.Text == "2 9 PM")
                    {
                        timereplacetxt.Text = "02:09:00:PM";
                    }
                    if (settimetxt.Text == "2 10 PM")
                    {
                        timereplacetxt.Text = "02:10:00:PM";
                    }
                    if (settimetxt.Text == "2 11 PM")
                    {
                        timereplacetxt.Text = "02:11:00:PM";
                    }
                    if (settimetxt.Text == "2 12 PM")
                    {
                        timereplacetxt.Text = "02:12:00:PM";
                    }
                    if (settimetxt.Text == "2 13 PM")
                    {
                        timereplacetxt.Text = "02:13:00:PM";
                    }
                    if (settimetxt.Text == "2 14 PM")
                    {
                        timereplacetxt.Text = "02:14:00:PM";
                    }
                    if (settimetxt.Text == "2 15 PM")
                    {
                        timereplacetxt.Text = "02:15:00:PM";
                    }
                    if (settimetxt.Text == "2 16 PM")
                    {
                        timereplacetxt.Text = "02:16:00:PM";
                    }
                    if (settimetxt.Text == "2 17 PM")
                    {
                        timereplacetxt.Text = "02:17:00:PM";
                    }
                    if (settimetxt.Text == "2 18 PM")
                    {
                        timereplacetxt.Text = "02:18:00:PM";
                    }
                    if (settimetxt.Text == "2 19 PM")
                    {
                        timereplacetxt.Text = "02:19:00:PM";
                    }
                    if (settimetxt.Text == "2 20 PM")
                    {
                        timereplacetxt.Text = "02:20:00:PM";
                    }
                    if (settimetxt.Text == "2 21 PM")
                    {
                        timereplacetxt.Text = "02:21:00:PM";
                    }
                    if (settimetxt.Text == "2 22 PM")
                    {
                        timereplacetxt.Text = "02:22:00:PM";
                    }
                    if (settimetxt.Text == "2 23 PM")
                    {
                        timereplacetxt.Text = "02:23:00:PM";
                    }
                    if (settimetxt.Text == "2 24 PM")
                    {
                        timereplacetxt.Text = "02:24:00:PM";
                    }
                    if (settimetxt.Text == "2 25 PM")
                    {
                        timereplacetxt.Text = "02:25:00:PM";
                    }
                    if (settimetxt.Text == "2 26 PM")
                    {
                        timereplacetxt.Text = "02:26:00:PM";
                    }
                    if (settimetxt.Text == "2 27 PM")
                    {
                        timereplacetxt.Text = "02:27:00:PM";
                    }
                    if (settimetxt.Text == "2 28 PM")
                    {
                        timereplacetxt.Text = "02:28:00:PM";
                    }
                    if (settimetxt.Text == "2 29 PM")
                    {
                        timereplacetxt.Text = "02:29:00:PM";
                    }
                    if (settimetxt.Text == "2 30 PM")
                    {
                        timereplacetxt.Text = "02:30:00:PM";
                    }
                    if (settimetxt.Text == "2 31 PM")
                    {
                        timereplacetxt.Text = "02:31:00:PM";
                    }
                    if (settimetxt.Text == "2 32 PM")
                    {
                        timereplacetxt.Text = "02:32:00:PM";
                    }
                    if (settimetxt.Text == "2 33 PM")
                    {
                        timereplacetxt.Text = "02:33:00:PM";
                    }
                    if (settimetxt.Text == "2 34 PM")
                    {
                        timereplacetxt.Text = "02:34:00:PM";
                    }
                    if (settimetxt.Text == "2 35 PM")
                    {
                        timereplacetxt.Text = "02:35:00:PM";
                    }
                    if (settimetxt.Text == "2 36 PM")
                    {
                        timereplacetxt.Text = "02:36:00:PM";
                    }
                    if (settimetxt.Text == "2 37 PM")
                    {
                        timereplacetxt.Text = "02:37:00:PM";
                    }
                    if (settimetxt.Text == "2 38 PM")
                    {
                        timereplacetxt.Text = "02:38:00:PM";
                    }
                    if (settimetxt.Text == "2 39 PM")
                    {
                        timereplacetxt.Text = "02:39:00:PM";
                    }
                    if (settimetxt.Text == "2 40 PM")
                    {
                        timereplacetxt.Text = "02:40:00:PM";
                    }
                    if (settimetxt.Text == "2 41 PM")
                    {
                        timereplacetxt.Text = "02:41:00:PM";
                    }
                    if (settimetxt.Text == "2 42 PM")
                    {
                        timereplacetxt.Text = "02:42:00:PM";
                    }
                    if (settimetxt.Text == "2 43 PM")
                    {
                        timereplacetxt.Text = "02:43:00:PM";
                    }
                    if (settimetxt.Text == "2 44 PM")
                    {
                        timereplacetxt.Text = "02:44:00:PM";
                    }
                    if (settimetxt.Text == "2 45 PM")
                    {
                        timereplacetxt.Text = "02:45:00:PM";
                    }
                    if (settimetxt.Text == "2 46 PM")
                    {
                        timereplacetxt.Text = "02:46:00:PM";
                    }
                    if (settimetxt.Text == "2 47 PM")
                    {
                        timereplacetxt.Text = "02:47:00:PM";
                    }
                    if (settimetxt.Text == "2 48 PM")
                    {
                        timereplacetxt.Text = "02:48:00:PM";
                    }
                    if (settimetxt.Text == "2 49 PM")
                    {
                        timereplacetxt.Text = "02:49:00:PM";
                    }
                    if (settimetxt.Text == "2 50 PM")
                    {
                        timereplacetxt.Text = "02:50:00:PM";
                    }
                    if (settimetxt.Text == "2 51 PM")
                    {
                        timereplacetxt.Text = "02:51:00:PM";
                    }
                    if (settimetxt.Text == "2 52 PM")
                    {
                        timereplacetxt.Text = "02:52:00:PM";
                    }
                    if (settimetxt.Text == "2 53 PM")
                    {
                        timereplacetxt.Text = "02:53:00:PM";
                    }
                    if (settimetxt.Text == "2 54 PM")
                    {
                        timereplacetxt.Text = "02:54:00:PM";
                    }
                    if (settimetxt.Text == "2 55 PM")
                    {
                        timereplacetxt.Text = "02:55:00:PM";
                    }
                    if (settimetxt.Text == "2 56 PM")
                    {
                        timereplacetxt.Text = "02:56:00:PM";
                    }
                    if (settimetxt.Text == "2 57 PM")
                    {
                        timereplacetxt.Text = "02:57:00:PM";
                    }
                    if (settimetxt.Text == "2 58 PM")
                    {
                        timereplacetxt.Text = "02:58:00:PM";
                    }
                    if (settimetxt.Text == "2 59 PM")
                    {
                        timereplacetxt.Text = "02:59:00:PM";
                    }
                    if (settimetxt.Text == "3 PM")
                    {
                        timereplacetxt.Text = "03:00:00:PM";
                    }
                    if (settimetxt.Text == "3 1 PM")
                    {
                        timereplacetxt.Text = "03:01:00:PM";
                    }
                    if (settimetxt.Text == "3 2 PM")
                    {
                        timereplacetxt.Text = "03:02:00:PM";
                    }
                    if (settimetxt.Text == "3 3 PM")
                    {
                        timereplacetxt.Text = "03:03:00:PM";
                    }
                    if (settimetxt.Text == "3 4 PM")
                    {
                        timereplacetxt.Text = "03:04:00:PM";
                    }
                    if (settimetxt.Text == "3 5 PM")
                    {
                        timereplacetxt.Text = "03:05:00:PM";
                    }
                    if (settimetxt.Text == "3 6 PM")
                    {
                        timereplacetxt.Text = "03:06:00:PM";
                    }
                    if (settimetxt.Text == "3 7 PM")
                    {
                        timereplacetxt.Text = "03:07:00:PM";
                    }
                    if (settimetxt.Text == "3 8 PM")
                    {
                        timereplacetxt.Text = "03:08:00:PM";
                    }
                    if (settimetxt.Text == "3 9 PM")
                    {
                        timereplacetxt.Text = "03:09:00:PM";
                    }
                    if (settimetxt.Text == "3 10 PM")
                    {
                        timereplacetxt.Text = "03:10:00:PM";
                    }
                    if (settimetxt.Text == "3 11 PM")
                    {
                        timereplacetxt.Text = "03:11:00:PM";
                    }
                    if (settimetxt.Text == "3 12 PM")
                    {
                        timereplacetxt.Text = "03:12:00:PM";
                    }
                    if (settimetxt.Text == "3 13 PM")
                    {
                        timereplacetxt.Text = "03:13:00:PM";
                    }
                    if (settimetxt.Text == "3 14 PM")
                    {
                        timereplacetxt.Text = "03:14:00:PM";
                    }
                    if (settimetxt.Text == "3 15 PM")
                    {
                        timereplacetxt.Text = "03:15:00:PM";
                    }
                    if (settimetxt.Text == "3 16 PM")
                    {
                        timereplacetxt.Text = "03:16:00:PM";
                    }
                    if (settimetxt.Text == "3 17 PM")
                    {
                        timereplacetxt.Text = "03:17:00:PM";
                    }
                    if (settimetxt.Text == "3 18 PM")
                    {
                        timereplacetxt.Text = "03:18:00:PM";
                    }
                    if (settimetxt.Text == "3 19 PM")
                    {
                        timereplacetxt.Text = "03:19:00:PM";
                    }
                    if (settimetxt.Text == "3 20 PM")
                    {
                        timereplacetxt.Text = "03:20:00:PM";
                    }
                    if (settimetxt.Text == "3 21 PM")
                    {
                        timereplacetxt.Text = "03:21:00:PM";
                    }
                    if (settimetxt.Text == "3 22 PM")
                    {
                        timereplacetxt.Text = "03:22:00:PM";
                    }
                    if (settimetxt.Text == "3 23 PM")
                    {
                        timereplacetxt.Text = "03:23:00:PM";
                    }
                    if (settimetxt.Text == "3 24 PM")
                    {
                        timereplacetxt.Text = "03:24:00:PM";
                    }
                    if (settimetxt.Text == "3 25 PM")
                    {
                        timereplacetxt.Text = "03:25:00:PM";
                    }
                    if (settimetxt.Text == "3 26 PM")
                    {
                        timereplacetxt.Text = "03:26:00:PM";
                    }
                    if (settimetxt.Text == "3 27 PM")
                    {
                        timereplacetxt.Text = "03:27:00:PM";
                    }
                    if (settimetxt.Text == "3 28 PM")
                    {
                        timereplacetxt.Text = "03:28:00:PM";
                    }
                    if (settimetxt.Text == "3 29 PM")
                    {
                        timereplacetxt.Text = "03:29:00:PM";
                    }
                    if (settimetxt.Text == "3 30 PM")
                    {
                        timereplacetxt.Text = "03:30:00:PM";
                    }
                    if (settimetxt.Text == "3 31 PM")
                    {
                        timereplacetxt.Text = "03:31:00:PM";
                    }
                    if (settimetxt.Text == "3 32 PM")
                    {
                        timereplacetxt.Text = "03:32:00:PM";
                    }
                    if (settimetxt.Text == "3 33 PM")
                    {
                        timereplacetxt.Text = "03:33:00:PM";
                    }
                    if (settimetxt.Text == "3 34 PM")
                    {
                        timereplacetxt.Text = "03:34:00:PM";
                    }
                    if (settimetxt.Text == "3 35 PM")
                    {
                        timereplacetxt.Text = "03:35:00:PM";
                    }
                    if (settimetxt.Text == "3 36 PM")
                    {
                        timereplacetxt.Text = "03:36:00:PM";
                    }
                    if (settimetxt.Text == "3 37 PM")
                    {
                        timereplacetxt.Text = "03:37:00:PM";
                    }
                    if (settimetxt.Text == "3 38 PM")
                    {
                        timereplacetxt.Text = "03:38:00:PM";
                    }
                    if (settimetxt.Text == "3 39 PM")
                    {
                        timereplacetxt.Text = "03:39:00:PM";
                    }
                    if (settimetxt.Text == "3 40 PM")
                    {
                        timereplacetxt.Text = "03:40:00:PM";
                    }
                    if (settimetxt.Text == "3 41 PM")
                    {
                        timereplacetxt.Text = "03:41:00:PM";
                    }
                    if (settimetxt.Text == "3 42 PM")
                    {
                        timereplacetxt.Text = "03:42:00:PM";
                    }
                    if (settimetxt.Text == "3 43 PM")
                    {
                        timereplacetxt.Text = "03:43:00:PM";
                    }
                    if (settimetxt.Text == "3 44 PM")
                    {
                        timereplacetxt.Text = "03:44:00:PM";
                    }
                    if (settimetxt.Text == "3 45 PM")
                    {
                        timereplacetxt.Text = "03:45:00:PM";
                    }
                    if (settimetxt.Text == "3 46 PM")
                    {
                        timereplacetxt.Text = "03:46:00:PM";
                    }
                    if (settimetxt.Text == "3 47 PM")
                    {
                        timereplacetxt.Text = "03:47:00:PM";
                    }
                    if (settimetxt.Text == "3 48 PM")
                    {
                        timereplacetxt.Text = "03:48:00:PM";
                    }
                    if (settimetxt.Text == "3 49 PM")
                    {
                        timereplacetxt.Text = "03:49:00:PM";
                    }
                    if (settimetxt.Text == "3 50 PM")
                    {
                        timereplacetxt.Text = "03:50:00:PM";
                    }
                    if (settimetxt.Text == "3 51 PM")
                    {
                        timereplacetxt.Text = "03:51:00:PM";
                    }
                    if (settimetxt.Text == "3 52 PM")
                    {
                        timereplacetxt.Text = "03:52:00:PM";
                    }
                    if (settimetxt.Text == "3 53 PM")
                    {
                        timereplacetxt.Text = "03:53:00:PM";
                    }
                    if (settimetxt.Text == "3 54 PM")
                    {
                        timereplacetxt.Text = "03:54:00:PM";
                    }
                    if (settimetxt.Text == "3 55 PM")
                    {
                        timereplacetxt.Text = "03:55:00:PM";
                    }
                    if (settimetxt.Text == "3 56 PM")
                    {
                        timereplacetxt.Text = "03:56:00:PM";
                    }
                    if (settimetxt.Text == "3 57 PM")
                    {
                        timereplacetxt.Text = "03:57:00:PM";
                    }
                    if (settimetxt.Text == "3 58 PM")
                    {
                        timereplacetxt.Text = "03:58:00:PM";
                    }
                    if (settimetxt.Text == "3 59 PM")
                    {
                        timereplacetxt.Text = "03:59:00:PM";
                    }
                    if (settimetxt.Text == "4 PM")
                    {
                        timereplacetxt.Text = "04:00:00:PM";
                    }
                    if (settimetxt.Text == "4 1 PM")
                    {
                        timereplacetxt.Text = "04:01:00:PM";
                    }
                    if (settimetxt.Text == "4 2 PM")
                    {
                        timereplacetxt.Text = "04:02:00:PM";
                    }
                    if (settimetxt.Text == "4 3 PM")
                    {
                        timereplacetxt.Text = "04:03:00:PM";
                    }
                    if (settimetxt.Text == "4 4 PM")
                    {
                        timereplacetxt.Text = "04:04:00:PM";
                    }
                    if (settimetxt.Text == "4 5 PM")
                    {
                        timereplacetxt.Text = "04:05:00:PM";
                    }
                    if (settimetxt.Text == "4 6 PM")
                    {
                        timereplacetxt.Text = "04:06:00:PM";
                    }
                    if (settimetxt.Text == "4 7 PM")
                    {
                        timereplacetxt.Text = "04:07:00:PM";
                    }
                    if (settimetxt.Text == "4 8 PM")
                    {
                        timereplacetxt.Text = "04:08:00:PM";
                    }
                    if (settimetxt.Text == "4 9 PM")
                    {
                        timereplacetxt.Text = "04:09:00:PM";
                    }
                    if (settimetxt.Text == "4 10 PM")
                    {
                        timereplacetxt.Text = "04:10:00:PM";
                    }
                    if (settimetxt.Text == "4 11 PM")
                    {
                        timereplacetxt.Text = "04:11:00:PM";
                    }
                    if (settimetxt.Text == "4 12 PM")
                    {
                        timereplacetxt.Text = "04:12:00:PM";
                    }
                    if (settimetxt.Text == "4 13 PM")
                    {
                        timereplacetxt.Text = "04:13:00:PM";
                    }
                    if (settimetxt.Text == "4 14 PM")
                    {
                        timereplacetxt.Text = "04:14:00:PM";
                    }
                    if (settimetxt.Text == "4 15 PM")
                    {
                        timereplacetxt.Text = "04:15:00:PM";
                    }
                    if (settimetxt.Text == "4 16 PM")
                    {
                        timereplacetxt.Text = "04:16:00:PM";
                    }
                    if (settimetxt.Text == "4 17 PM")
                    {
                        timereplacetxt.Text = "04:17:00:PM";
                    }
                    if (settimetxt.Text == "4 18 PM")
                    {
                        timereplacetxt.Text = "04:18:00:PM";
                    }
                    if (settimetxt.Text == "4 19 PM")
                    {
                        timereplacetxt.Text = "04:19:00:PM";
                    }
                    if (settimetxt.Text == "4 20 PM")
                    {
                        timereplacetxt.Text = "04:20:00:PM";
                    }
                    if (settimetxt.Text == "4 21 PM")
                    {
                        timereplacetxt.Text = "04:21:00:PM";
                    }
                    if (settimetxt.Text == "4 22 PM")
                    {
                        timereplacetxt.Text = "04:22:00:PM";
                    }
                    if (settimetxt.Text == "4 23 PM")
                    {
                        timereplacetxt.Text = "04:23:00:PM";
                    }
                    if (settimetxt.Text == "4 24 PM")
                    {
                        timereplacetxt.Text = "04:24:00:PM";
                    }
                    if (settimetxt.Text == "4 25 PM")
                    {
                        timereplacetxt.Text = "04:25:00:PM";
                    }
                    if (settimetxt.Text == "4 26 PM")
                    {
                        timereplacetxt.Text = "04:26:00:PM";
                    }
                    if (settimetxt.Text == "4 27 PM")
                    {
                        timereplacetxt.Text = "04:27:00:PM";
                    }
                    if (settimetxt.Text == "4 28 PM")
                    {
                        timereplacetxt.Text = "04:28:00:PM";
                    }
                    if (settimetxt.Text == "4 29 PM")
                    {
                        timereplacetxt.Text = "04:29:00:PM";
                    }
                    if (settimetxt.Text == "4 30 PM")
                    {
                        timereplacetxt.Text = "04:30:00:PM";
                    }
                    if (settimetxt.Text == "4 31 PM")
                    {
                        timereplacetxt.Text = "04:31:00:PM";
                    }
                    if (settimetxt.Text == "4 32 PM")
                    {
                        timereplacetxt.Text = "04:32:00:PM";
                    }
                    if (settimetxt.Text == "4 33 PM")
                    {
                        timereplacetxt.Text = "04:33:00:PM";
                    }
                    if (settimetxt.Text == "4 34 PM")
                    {
                        timereplacetxt.Text = "04:34:00:PM";
                    }
                    if (settimetxt.Text == "4 35 PM")
                    {
                        timereplacetxt.Text = "04:35:00:PM";
                    }
                    if (settimetxt.Text == "4 36 PM")
                    {
                        timereplacetxt.Text = "04:36:00:PM";
                    }
                    if (settimetxt.Text == "4 37 PM")
                    {
                        timereplacetxt.Text = "04:37:00:PM";
                    }
                    if (settimetxt.Text == "4 38 PM")
                    {
                        timereplacetxt.Text = "04:38:00:PM";
                    }
                    if (settimetxt.Text == "4 39 PM")
                    {
                        timereplacetxt.Text = "04:39:00:PM";
                    }
                    if (settimetxt.Text == "4 40 PM")
                    {
                        timereplacetxt.Text = "04:40:00:PM";
                    }
                    if (settimetxt.Text == "4 41 PM")
                    {
                        timereplacetxt.Text = "04:41:00:PM";
                    }
                    if (settimetxt.Text == "4 42 PM")
                    {
                        timereplacetxt.Text = "04:42:00:PM";
                    }
                    if (settimetxt.Text == "4 43 PM")
                    {
                        timereplacetxt.Text = "04:43:00:PM";
                    }
                    if (settimetxt.Text == "4 44 PM")
                    {
                        timereplacetxt.Text = "04:44:00:PM";
                    }
                    if (settimetxt.Text == "4 45 PM")
                    {
                        timereplacetxt.Text = "04:45:00:PM";
                    }
                    if (settimetxt.Text == "4 46 PM")
                    {
                        timereplacetxt.Text = "04:46:00:PM";
                    }
                    if (settimetxt.Text == "4 47 PM")
                    {
                        timereplacetxt.Text = "04:47:00:PM";
                    }
                    if (settimetxt.Text == "4 48 PM")
                    {
                        timereplacetxt.Text = "04:48:00:PM";
                    }
                    if (settimetxt.Text == "4 49 PM")
                    {
                        timereplacetxt.Text = "04:49:00:PM";
                    }
                    if (settimetxt.Text == "4 50 PM")
                    {
                        timereplacetxt.Text = "04:50:00:PM";
                    }
                    if (settimetxt.Text == "4 51 PM")
                    {
                        timereplacetxt.Text = "04:51:00:PM";
                    }
                    if (settimetxt.Text == "4 52 PM")
                    {
                        timereplacetxt.Text = "04:52:00:PM";
                    }
                    if (settimetxt.Text == "4 53 PM")
                    {
                        timereplacetxt.Text = "04:53:00:PM";
                    }
                    if (settimetxt.Text == "4 54 PM")
                    {
                        timereplacetxt.Text = "04:54:00:PM";
                    }
                    if (settimetxt.Text == "4 55 PM")
                    {
                        timereplacetxt.Text = "04:55:00:PM";
                    }
                    if (settimetxt.Text == "4 56 PM")
                    {
                        timereplacetxt.Text = "04:56:00:PM";
                    }
                    if (settimetxt.Text == "4 57 PM")
                    {
                        timereplacetxt.Text = "04:57:00:PM";
                    }
                    if (settimetxt.Text == "4 58 PM")
                    {
                        timereplacetxt.Text = "04:58:00:PM";
                    }
                    if (settimetxt.Text == "4 59 PM")
                    {
                        timereplacetxt.Text = "04:59:00:PM";
                    }
                    if (settimetxt.Text == "5 PM")
                    {
                        timereplacetxt.Text = "05:00:00:PM";
                    }
                    if (settimetxt.Text == "5 1 PM")
                    {
                        timereplacetxt.Text = "05:01:00:PM";
                    }
                    if (settimetxt.Text == "5 2 PM")
                    {
                        timereplacetxt.Text = "05:02:00:PM";
                    }
                    if (settimetxt.Text == "5 3 PM")
                    {
                        timereplacetxt.Text = "05:03:00:PM";
                    }
                    if (settimetxt.Text == "5 4 PM")
                    {
                        timereplacetxt.Text = "05:04:00:PM";
                    }
                    if (settimetxt.Text == "5 5 PM")
                    {
                        timereplacetxt.Text = "05:05:00:PM";
                    }
                    if (settimetxt.Text == "5 6 PM")
                    {
                        timereplacetxt.Text = "05:06:00:PM";
                    }
                    if (settimetxt.Text == "5 7 PM")
                    {
                        timereplacetxt.Text = "05:07:00:PM";
                    }
                    if (settimetxt.Text == "5 8 PM")
                    {
                        timereplacetxt.Text = "05:08:00:PM";
                    }
                    if (settimetxt.Text == "5 9 PM")
                    {
                        timereplacetxt.Text = "05:09:00:PM";
                    }
                    if (settimetxt.Text == "5 10 PM")
                    {
                        timereplacetxt.Text = "05:10:00:PM";
                    }
                    if (settimetxt.Text == "5 11 PM")
                    {
                        timereplacetxt.Text = "05:11:00:PM";
                    }
                    if (settimetxt.Text == "5 12 PM")
                    {
                        timereplacetxt.Text = "05:12:00:PM";
                    }
                    if (settimetxt.Text == "5 13 PM")
                    {
                        timereplacetxt.Text = "05:13:00:PM";
                    }
                    if (settimetxt.Text == "5 14 PM")
                    {
                        timereplacetxt.Text = "05:14:00:PM";
                    }
                    if (settimetxt.Text == "5 15 PM")
                    {
                        timereplacetxt.Text = "05:15:00:PM";
                    }
                    if (settimetxt.Text == "5 16 PM")
                    {
                        timereplacetxt.Text = "05:16:00:PM";
                    }
                    if (settimetxt.Text == "5 17 PM")
                    {
                        timereplacetxt.Text = "05:17:00:PM";
                    }
                    if (settimetxt.Text == "5 18 PM")
                    {
                        timereplacetxt.Text = "05:18:00:PM";
                    }
                    if (settimetxt.Text == "5 19 PM")
                    {
                        timereplacetxt.Text = "05:19:00:PM";
                    }
                    if (settimetxt.Text == "5 20 PM")
                    {
                        timereplacetxt.Text = "05:20:00:PM";
                    }
                    if (settimetxt.Text == "5 21 PM")
                    {
                        timereplacetxt.Text = "05:21:00:PM";
                    }
                    if (settimetxt.Text == "5 22 PM")
                    {
                        timereplacetxt.Text = "05:22:00:PM";
                    }
                    if (settimetxt.Text == "5 23 PM")
                    {
                        timereplacetxt.Text = "05:23:00:PM";
                    }
                    if (settimetxt.Text == "5 24 PM")
                    {
                        timereplacetxt.Text = "05:24:00:PM";
                    }
                    if (settimetxt.Text == "5 25 PM")
                    {
                        timereplacetxt.Text = "05:25:00:PM";
                    }
                    if (settimetxt.Text == "5 26 PM")
                    {
                        timereplacetxt.Text = "05:26:00:PM";
                    }
                    if (settimetxt.Text == "5 27 PM")
                    {
                        timereplacetxt.Text = "05:27:00:PM";
                    }
                    if (settimetxt.Text == "5 28 PM")
                    {
                        timereplacetxt.Text = "05:28:00:PM";
                    }
                    if (settimetxt.Text == "5 29 PM")
                    {
                        timereplacetxt.Text = "05:29:00:PM";
                    }
                    if (settimetxt.Text == "5 30 PM")
                    {
                        timereplacetxt.Text = "05:30:00:PM";
                    }
                    if (settimetxt.Text == "5 31 PM")
                    {
                        timereplacetxt.Text = "05:31:00:PM";
                    }
                    if (settimetxt.Text == "5 32 PM")
                    {
                        timereplacetxt.Text = "05:32:00:PM";
                    }
                    if (settimetxt.Text == "5 33 PM")
                    {
                        timereplacetxt.Text = "05:33:00:PM";
                    }
                    if (settimetxt.Text == "5 34 PM")
                    {
                        timereplacetxt.Text = "05:34:00:PM";
                    }
                    if (settimetxt.Text == "5 35 PM")
                    {
                        timereplacetxt.Text = "05:35:00:PM";
                    }
                    if (settimetxt.Text == "5 36 PM")
                    {
                        timereplacetxt.Text = "05:36:00:PM";
                    }
                    if (settimetxt.Text == "5 37 PM")
                    {
                        timereplacetxt.Text = "05:37:00:PM";
                    }
                    if (settimetxt.Text == "5 38 PM")
                    {
                        timereplacetxt.Text = "05:38:00:PM";
                    }
                    if (settimetxt.Text == "5 39 PM")
                    {
                        timereplacetxt.Text = "05:39:00:PM";
                    }
                    if (settimetxt.Text == "5 40 PM")
                    {
                        timereplacetxt.Text = "05:40:00:PM";
                    }
                    if (settimetxt.Text == "5 41 PM")
                    {
                        timereplacetxt.Text = "05:41:00:PM";
                    }
                    if (settimetxt.Text == "5 42 PM")
                    {
                        timereplacetxt.Text = "05:42:00:PM";
                    }
                    if (settimetxt.Text == "5 43 PM")
                    {
                        timereplacetxt.Text = "05:43:00:PM";
                    }
                    if (settimetxt.Text == "5 44 PM")
                    {
                        timereplacetxt.Text = "05:44:00:PM";
                    }
                    if (settimetxt.Text == "5 45 PM")
                    {
                        timereplacetxt.Text = "05:45:00:PM";
                    }
                    if (settimetxt.Text == "5 46 PM")
                    {
                        timereplacetxt.Text = "05:46:00:PM";
                    }
                    if (settimetxt.Text == "5 47 PM")
                    {
                        timereplacetxt.Text = "05:47:00:PM";
                    }
                    if (settimetxt.Text == "5 48 PM")
                    {
                        timereplacetxt.Text = "05:48:00:PM";
                    }
                    if (settimetxt.Text == "5 49 PM")
                    {
                        timereplacetxt.Text = "05:49:00:PM";
                    }
                    if (settimetxt.Text == "5 50 PM")
                    {
                        timereplacetxt.Text = "05:50:00:PM";
                    }
                    if (settimetxt.Text == "5 51 PM")
                    {
                        timereplacetxt.Text = "05:51:00:PM";
                    }
                    if (settimetxt.Text == "5 52 PM")
                    {
                        timereplacetxt.Text = "05:52:00:PM";
                    }
                    if (settimetxt.Text == "5 53 PM")
                    {
                        timereplacetxt.Text = "05:53:00:PM";
                    }
                    if (settimetxt.Text == "5 54 PM")
                    {
                        timereplacetxt.Text = "05:54:00:PM";
                    }
                    if (settimetxt.Text == "5 55 PM")
                    {
                        timereplacetxt.Text = "05:55:00:PM";
                    }
                    if (settimetxt.Text == "5 56 PM")
                    {
                        timereplacetxt.Text = "05:56:00:PM";
                    }
                    if (settimetxt.Text == "5 57 PM")
                    {
                        timereplacetxt.Text = "05:57:00:PM";
                    }
                    if (settimetxt.Text == "5 58 PM")
                    {
                        timereplacetxt.Text = "05:58:00:PM";
                    }
                    if (settimetxt.Text == "5 59 PM")
                    {
                        timereplacetxt.Text = "05:59:00:PM";
                    }
                    if (settimetxt.Text == "6 PM")
                    {
                        timereplacetxt.Text = "06:00:00:PM";
                    }
                    if (settimetxt.Text == "6 1 PM")
                    {
                        timereplacetxt.Text = "06:01:00:PM";
                    }
                    if (settimetxt.Text == "6 2 PM")
                    {
                        timereplacetxt.Text = "06:02:00:PM";
                    }
                    if (settimetxt.Text == "6 3 PM")
                    {
                        timereplacetxt.Text = "06:03:00:PM";
                    }
                    if (settimetxt.Text == "6 4 PM")
                    {
                        timereplacetxt.Text = "06:04:00:PM";
                    }
                    if (settimetxt.Text == "6 5 PM")
                    {
                        timereplacetxt.Text = "06:05:00:PM";
                    }
                    if (settimetxt.Text == "6 6 PM")
                    {
                        timereplacetxt.Text = "06:06:00:PM";
                    }
                    if (settimetxt.Text == "6 7 PM")
                    {
                        timereplacetxt.Text = "06:07:00:PM";
                    }
                    if (settimetxt.Text == "6 8 PM")
                    {
                        timereplacetxt.Text = "06:08:00:PM";
                    }
                    if (settimetxt.Text == "6 9 PM")
                    {
                        timereplacetxt.Text = "06:09:00:PM";
                    }
                    if (settimetxt.Text == "6 10 PM")
                    {
                        timereplacetxt.Text = "06:10:00:PM";
                    }
                    if (settimetxt.Text == "6 11 PM")
                    {
                        timereplacetxt.Text = "06:11:00:PM";
                    }
                    if (settimetxt.Text == "6 12 PM")
                    {
                        timereplacetxt.Text = "06:12:00:PM";
                    }
                    if (settimetxt.Text == "6 13 PM")
                    {
                        timereplacetxt.Text = "06:13:00:PM";
                    }
                    if (settimetxt.Text == "6 14 PM")
                    {
                        timereplacetxt.Text = "06:14:00:PM";
                    }
                    if (settimetxt.Text == "6 15 PM")
                    {
                        timereplacetxt.Text = "06:15:00:PM";
                    }
                    if (settimetxt.Text == "6 16 PM")
                    {
                        timereplacetxt.Text = "06:16:00:PM";
                    }
                    if (settimetxt.Text == "6 17 PM")
                    {
                        timereplacetxt.Text = "06:17:00:PM";
                    }
                    if (settimetxt.Text == "6 18 PM")
                    {
                        timereplacetxt.Text = "06:18:00:PM";
                    }
                    if (settimetxt.Text == "6 19 PM")
                    {
                        timereplacetxt.Text = "06:19:00:PM";
                    }
                    if (settimetxt.Text == "6 20 PM")
                    {
                        timereplacetxt.Text = "06:20:00:PM";
                    }
                    if (settimetxt.Text == "6 21 PM")
                    {
                        timereplacetxt.Text = "06:21:00:PM";
                    }
                    if (settimetxt.Text == "6 22 PM")
                    {
                        timereplacetxt.Text = "06:22:00:PM";
                    }
                    if (settimetxt.Text == "6 23 PM")
                    {
                        timereplacetxt.Text = "06:23:00:PM";
                    }
                    if (settimetxt.Text == "6 24 PM")
                    {
                        timereplacetxt.Text = "06:24:00:PM";
                    }
                    if (settimetxt.Text == "6 25 PM")
                    {
                        timereplacetxt.Text = "06:25:00:PM";
                    }
                    if (settimetxt.Text == "6 26 PM")
                    {
                        timereplacetxt.Text = "06:26:00:PM";
                    }
                    if (settimetxt.Text == "6 27 PM")
                    {
                        timereplacetxt.Text = "06:27:00:PM";
                    }
                    if (settimetxt.Text == "6 28 PM")
                    {
                        timereplacetxt.Text = "06:28:00:PM";
                    }
                    if (settimetxt.Text == "6 29 PM")
                    {
                        timereplacetxt.Text = "06:29:00:PM";
                    }
                    if (settimetxt.Text == "6 30 PM")
                    {
                        timereplacetxt.Text = "06:30:00:PM";
                    }
                    if (settimetxt.Text == "6 31 PM")
                    {
                        timereplacetxt.Text = "06:31:00:PM";
                    }
                    if (settimetxt.Text == "6 32 PM")
                    {
                        timereplacetxt.Text = "06:32:00:PM";
                    }
                    if (settimetxt.Text == "6 33 PM")
                    {
                        timereplacetxt.Text = "06:33:00:PM";
                    }
                    if (settimetxt.Text == "6 34 PM")
                    {
                        timereplacetxt.Text = "06:34:00:PM";
                    }
                    if (settimetxt.Text == "6 35 PM")
                    {
                        timereplacetxt.Text = "06:35:00:PM";
                    }
                    if (settimetxt.Text == "6 36 PM")
                    {
                        timereplacetxt.Text = "06:36:00:PM";
                    }
                    if (settimetxt.Text == "6 37 PM")
                    {
                        timereplacetxt.Text = "06:37:00:PM";
                    }
                    if (settimetxt.Text == "6 38 PM")
                    {
                        timereplacetxt.Text = "06:38:00:PM";
                    }
                    if (settimetxt.Text == "6 39 PM")
                    {
                        timereplacetxt.Text = "06:39:00:PM";
                    }
                    if (settimetxt.Text == "6 40 PM")
                    {
                        timereplacetxt.Text = "06:40:00:PM";
                    }
                    if (settimetxt.Text == "6 41 PM")
                    {
                        timereplacetxt.Text = "06:41:00:PM";
                    }
                    if (settimetxt.Text == "6 42 PM")
                    {
                        timereplacetxt.Text = "06:42:00:PM";
                    }
                    if (settimetxt.Text == "6 43 PM")
                    {
                        timereplacetxt.Text = "06:43:00:PM";
                    }
                    if (settimetxt.Text == "6 44 PM")
                    {
                        timereplacetxt.Text = "06:44:00:PM";
                    }
                    if (settimetxt.Text == "6 45 PM")
                    {
                        timereplacetxt.Text = "06:45:00:PM";
                    }
                    if (settimetxt.Text == "6 46 PM")
                    {
                        timereplacetxt.Text = "06:46:00:PM";
                    }
                    if (settimetxt.Text == "6 47 PM")
                    {
                        timereplacetxt.Text = "06:47:00:PM";
                    }
                    if (settimetxt.Text == "6 48 PM")
                    {
                        timereplacetxt.Text = "06:48:00:PM";
                    }
                    if (settimetxt.Text == "6 49 PM")
                    {
                        timereplacetxt.Text = "06:49:00:PM";
                    }
                    if (settimetxt.Text == "6 50 PM")
                    {
                        timereplacetxt.Text = "06:50:00:PM";
                    }
                    if (settimetxt.Text == "6 51 PM")
                    {
                        timereplacetxt.Text = "06:51:00:PM";
                    }
                    if (settimetxt.Text == "6 52 PM")
                    {
                        timereplacetxt.Text = "06:52:00:PM";
                    }
                    if (settimetxt.Text == "6 53 PM")
                    {
                        timereplacetxt.Text = "06:53:00:PM";
                    }
                    if (settimetxt.Text == "6 54 PM")
                    {
                        timereplacetxt.Text = "06:54:00:PM";
                    }
                    if (settimetxt.Text == "6 55 PM")
                    {
                        timereplacetxt.Text = "06:55:00:PM";
                    }
                    if (settimetxt.Text == "6 56 PM")
                    {
                        timereplacetxt.Text = "06:56:00:PM";
                    }
                    if (settimetxt.Text == "6 57 PM")
                    {
                        timereplacetxt.Text = "06:57:00:PM";
                    }
                    if (settimetxt.Text == "6 58 PM")
                    {
                        timereplacetxt.Text = "06:58:00:PM";
                    }
                    if (settimetxt.Text == "6 59 PM")
                    {
                        timereplacetxt.Text = "06:59:00:PM";
                    }
                    if (settimetxt.Text == "7 PM")
                    {
                        timereplacetxt.Text = "07:00:00:PM";
                    }
                    if (settimetxt.Text == "7 1 PM")
                    {
                        timereplacetxt.Text = "07:01:00:PM";
                    }
                    if (settimetxt.Text == "7 2 PM")
                    {
                        timereplacetxt.Text = "07:02:00:PM";
                    }
                    if (settimetxt.Text == "7 3 PM")
                    {
                        timereplacetxt.Text = "07:03:00:PM";
                    }
                    if (settimetxt.Text == "7 4 PM")
                    {
                        timereplacetxt.Text = "07:04:00:PM";
                    }
                    if (settimetxt.Text == "7 5 PM")
                    {
                        timereplacetxt.Text = "07:05:00:PM";
                    }
                    if (settimetxt.Text == "7 6 PM")
                    {
                        timereplacetxt.Text = "07:06:00:PM";
                    }
                    if (settimetxt.Text == "7 7 PM")
                    {
                        timereplacetxt.Text = "07:07:00:PM";
                    }
                    if (settimetxt.Text == "7 8 PM")
                    {
                        timereplacetxt.Text = "07:08:00:PM";
                    }
                    if (settimetxt.Text == "7 9 PM")
                    {
                        timereplacetxt.Text = "07:09:00:PM";
                    }
                    if (settimetxt.Text == "7 10 PM")
                    {
                        timereplacetxt.Text = "07:10:00:PM";
                    }
                    if (settimetxt.Text == "7 11 PM")
                    {
                        timereplacetxt.Text = "07:11:00:PM";
                    }
                    if (settimetxt.Text == "7 12 PM")
                    {
                        timereplacetxt.Text = "07:12:00:PM";
                    }
                    if (settimetxt.Text == "7 13 PM")
                    {
                        timereplacetxt.Text = "07:13:00:PM";
                    }
                    if (settimetxt.Text == "7 14 PM")
                    {
                        timereplacetxt.Text = "07:14:00:PM";
                    }
                    if (settimetxt.Text == "7 15 PM")
                    {
                        timereplacetxt.Text = "07:15:00:PM";
                    }
                    if (settimetxt.Text == "7 16 PM")
                    {
                        timereplacetxt.Text = "07:16:00:PM";
                    }
                    if (settimetxt.Text == "7 17 PM")
                    {
                        timereplacetxt.Text = "07:17:00:PM";
                    }
                    if (settimetxt.Text == "7 18 PM")
                    {
                        timereplacetxt.Text = "07:18:00:PM";
                    }
                    if (settimetxt.Text == "7 19 PM")
                    {
                        timereplacetxt.Text = "07:19:00:PM";
                    }
                    if (settimetxt.Text == "7 20 PM")
                    {
                        timereplacetxt.Text = "07:20:00:PM";
                    }
                    if (settimetxt.Text == "7 21 PM")
                    {
                        timereplacetxt.Text = "07:21:00:PM";
                    }
                    if (settimetxt.Text == "7 22 PM")
                    {
                        timereplacetxt.Text = "07:22:00:PM";
                    }
                    if (settimetxt.Text == "7 23 PM")
                    {
                        timereplacetxt.Text = "07:23:00:PM";
                    }
                    if (settimetxt.Text == "7 24 PM")
                    {
                        timereplacetxt.Text = "07:24:00:PM";
                    }
                    if (settimetxt.Text == "7 25 PM")
                    {
                        timereplacetxt.Text = "07:25:00:PM";
                    }
                    if (settimetxt.Text == "7 26 PM")
                    {
                        timereplacetxt.Text = "07:26:00:PM";
                    }
                    if (settimetxt.Text == "7 27 PM")
                    {
                        timereplacetxt.Text = "07:27:00:PM";
                    }
                    if (settimetxt.Text == "7 28 PM")
                    {
                        timereplacetxt.Text = "07:28:00:PM";
                    }
                    if (settimetxt.Text == "7 29 PM")
                    {
                        timereplacetxt.Text = "07:29:00:PM";
                    }
                    if (settimetxt.Text == "7 30 PM")
                    {
                        timereplacetxt.Text = "07:30:00:PM";
                    }
                    if (settimetxt.Text == "7 31 PM")
                    {
                        timereplacetxt.Text = "07:31:00:PM";
                    }
                    if (settimetxt.Text == "7 32 PM")
                    {
                        timereplacetxt.Text = "07:32:00:PM";
                    }
                    if (settimetxt.Text == "7 33 PM")
                    {
                        timereplacetxt.Text = "07:33:00:PM";
                    }
                    if (settimetxt.Text == "7 34 PM")
                    {
                        timereplacetxt.Text = "07:34:00:PM";
                    }
                    if (settimetxt.Text == "7 35 PM")
                    {
                        timereplacetxt.Text = "07:35:00:PM";
                    }
                    if (settimetxt.Text == "7 36 PM")
                    {
                        timereplacetxt.Text = "07:36:00:PM";
                    }
                    if (settimetxt.Text == "7 37 PM")
                    {
                        timereplacetxt.Text = "07:37:00:PM";
                    }
                    if (settimetxt.Text == "7 38 PM")
                    {
                        timereplacetxt.Text = "07:38:00:PM";
                    }
                    if (settimetxt.Text == "7 39 PM")
                    {
                        timereplacetxt.Text = "07:39:00:PM";
                    }
                    if (settimetxt.Text == "7 40 PM")
                    {
                        timereplacetxt.Text = "07:40:00:PM";
                    }
                    if (settimetxt.Text == "7 41 PM")
                    {
                        timereplacetxt.Text = "07:41:00:PM";
                    }
                    if (settimetxt.Text == "7 42 PM")
                    {
                        timereplacetxt.Text = "07:42:00:PM";
                    }
                    if (settimetxt.Text == "7 43 PM")
                    {
                        timereplacetxt.Text = "07:43:00:PM";
                    }
                    if (settimetxt.Text == "7 44 PM")
                    {
                        timereplacetxt.Text = "07:44:00:PM";
                    }
                    if (settimetxt.Text == "7 45 PM")
                    {
                        timereplacetxt.Text = "07:45:00:PM";
                    }
                    if (settimetxt.Text == "7 46 PM")
                    {
                        timereplacetxt.Text = "07:46:00:PM";
                    }
                    if (settimetxt.Text == "7 47 PM")
                    {
                        timereplacetxt.Text = "07:47:00:PM";
                    }
                    if (settimetxt.Text == "7 48 PM")
                    {
                        timereplacetxt.Text = "07:48:00:PM";
                    }
                    if (settimetxt.Text == "7 49 PM")
                    {
                        timereplacetxt.Text = "07:49:00:PM";
                    }
                    if (settimetxt.Text == "7 50 PM")
                    {
                        timereplacetxt.Text = "07:50:00:PM";
                    }
                    if (settimetxt.Text == "7 51 PM")
                    {
                        timereplacetxt.Text = "07:51:00:PM";
                    }
                    if (settimetxt.Text == "7 52 PM")
                    {
                        timereplacetxt.Text = "07:52:00:PM";
                    }
                    if (settimetxt.Text == "7 53 PM")
                    {
                        timereplacetxt.Text = "07:53:00:PM";
                    }
                    if (settimetxt.Text == "7 54 PM")
                    {
                        timereplacetxt.Text = "07:54:00:PM";
                    }
                    if (settimetxt.Text == "7 55 PM")
                    {
                        timereplacetxt.Text = "07:55:00:PM";
                    }
                    if (settimetxt.Text == "7 56 PM")
                    {
                        timereplacetxt.Text = "07:56:00:PM";
                    }
                    if (settimetxt.Text == "7 57 PM")
                    {
                        timereplacetxt.Text = "07:57:00:PM";
                    }
                    if (settimetxt.Text == "7 58 PM")
                    {
                        timereplacetxt.Text = "07:58:00:PM";
                    }
                    if (settimetxt.Text == "7 59 PM")
                    {
                        timereplacetxt.Text = "07:59:00:PM";
                    }
                    if (settimetxt.Text == "8 PM")
                    {
                        timereplacetxt.Text = "08:00:00:PM";
                    }
                    if (settimetxt.Text == "8 1 PM")
                    {
                        timereplacetxt.Text = "08:01:00:PM";
                    }
                    if (settimetxt.Text == "8 2 PM")
                    {
                        timereplacetxt.Text = "08:02:00:PM";
                    }
                    if (settimetxt.Text == "8 3 PM")
                    {
                        timereplacetxt.Text = "08:03:00:PM";
                    }
                    if (settimetxt.Text == "8 4 PM")
                    {
                        timereplacetxt.Text = "08:04:00:PM";
                    }
                    if (settimetxt.Text == "8 5 PM")
                    {
                        timereplacetxt.Text = "08:05:00:PM";
                    }
                    if (settimetxt.Text == "8 6 PM")
                    {
                        timereplacetxt.Text = "08:06:00:PM";
                    }
                    if (settimetxt.Text == "8 7 PM")
                    {
                        timereplacetxt.Text = "08:07:00:PM";
                    }
                    if (settimetxt.Text == "8 8 PM")
                    {
                        timereplacetxt.Text = "08:08:00:PM";
                    }
                    if (settimetxt.Text == "8 9 PM")
                    {
                        timereplacetxt.Text = "08:09:00:PM";
                    }
                    if (settimetxt.Text == "8 10 PM")
                    {
                        timereplacetxt.Text = "08:10:00:PM";
                    }
                    if (settimetxt.Text == "8 11 PM")
                    {
                        timereplacetxt.Text = "08:11:00:PM";
                    }
                    if (settimetxt.Text == "8 12 PM")
                    {
                        timereplacetxt.Text = "08:12:00:PM";
                    }
                    if (settimetxt.Text == "8 13 PM")
                    {
                        timereplacetxt.Text = "08:13:00:PM";
                    }
                    if (settimetxt.Text == "8 14 PM")
                    {
                        timereplacetxt.Text = "08:14:00:PM";
                    }
                    if (settimetxt.Text == "8 15 PM")
                    {
                        timereplacetxt.Text = "08:15:00:PM";
                    }
                    if (settimetxt.Text == "8 16 PM")
                    {
                        timereplacetxt.Text = "08:16:00:PM";
                    }
                    if (settimetxt.Text == "8 17 PM")
                    {
                        timereplacetxt.Text = "08:17:00:PM";
                    }
                    if (settimetxt.Text == "8 18 PM")
                    {
                        timereplacetxt.Text = "08:18:00:PM";
                    }
                    if (settimetxt.Text == "8 19 PM")
                    {
                        timereplacetxt.Text = "08:19:00:PM";
                    }
                    if (settimetxt.Text == "8 20 PM")
                    {
                        timereplacetxt.Text = "08:20:00:PM";
                    }
                    if (settimetxt.Text == "8 21 PM")
                    {
                        timereplacetxt.Text = "08:21:00:PM";
                    }
                    if (settimetxt.Text == "8 22 PM")
                    {
                        timereplacetxt.Text = "08:22:00:PM";
                    }
                    if (settimetxt.Text == "8 23 PM")
                    {
                        timereplacetxt.Text = "08:23:00:PM";
                    }
                    if (settimetxt.Text == "8 24 PM")
                    {
                        timereplacetxt.Text = "08:24:00:PM";
                    }
                    if (settimetxt.Text == "8 25 PM")
                    {
                        timereplacetxt.Text = "08:25:00:PM";
                    }
                    if (settimetxt.Text == "8 26 PM")
                    {
                        timereplacetxt.Text = "08:26:00:PM";
                    }
                    if (settimetxt.Text == "8 27 PM")
                    {
                        timereplacetxt.Text = "08:27:00:PM";
                    }
                    if (settimetxt.Text == "8 28 PM")
                    {
                        timereplacetxt.Text = "08:28:00:PM";
                    }
                    if (settimetxt.Text == "8 29 PM")
                    {
                        timereplacetxt.Text = "08:29:00:PM";
                    }
                    if (settimetxt.Text == "8 30 PM")
                    {
                        timereplacetxt.Text = "08:30:00:PM";
                    }
                    if (settimetxt.Text == "8 31 PM")
                    {
                        timereplacetxt.Text = "08:31:00:PM";
                    }
                    if (settimetxt.Text == "8 32 PM")
                    {
                        timereplacetxt.Text = "08:32:00:PM";
                    }
                    if (settimetxt.Text == "8 33 PM")
                    {
                        timereplacetxt.Text = "08:33:00:PM";
                    }
                    if (settimetxt.Text == "8 34 PM")
                    {
                        timereplacetxt.Text = "08:34:00:PM";
                    }
                    if (settimetxt.Text == "8 35 PM")
                    {
                        timereplacetxt.Text = "08:35:00:PM";
                    }
                    if (settimetxt.Text == "8 36 PM")
                    {
                        timereplacetxt.Text = "08:36:00:PM";
                    }
                    if (settimetxt.Text == "8 37 PM")
                    {
                        timereplacetxt.Text = "08:37:00:PM";
                    }
                    if (settimetxt.Text == "8 38 PM")
                    {
                        timereplacetxt.Text = "08:38:00:PM";
                    }
                    if (settimetxt.Text == "8 39 PM")
                    {
                        timereplacetxt.Text = "08:39:00:PM";
                    }
                    if (settimetxt.Text == "8 40 PM")
                    {
                        timereplacetxt.Text = "08:40:00:PM";
                    }
                    if (settimetxt.Text == "8 41 PM")
                    {
                        timereplacetxt.Text = "08:41:00:PM";
                    }
                    if (settimetxt.Text == "8 42 PM")
                    {
                        timereplacetxt.Text = "08:42:00:PM";
                    }
                    if (settimetxt.Text == "8 43 PM")
                    {
                        timereplacetxt.Text = "08:43:00:PM";
                    }
                    if (settimetxt.Text == "8 44 PM")
                    {
                        timereplacetxt.Text = "08:44:00:PM";
                    }
                    if (settimetxt.Text == "8 45 PM")
                    {
                        timereplacetxt.Text = "08:45:00:PM";
                    }
                    if (settimetxt.Text == "8 46 PM")
                    {
                        timereplacetxt.Text = "08:46:00:PM";
                    }
                    if (settimetxt.Text == "8 47 PM")
                    {
                        timereplacetxt.Text = "08:47:00:PM";
                    }
                    if (settimetxt.Text == "8 48 PM")
                    {
                        timereplacetxt.Text = "08:48:00:PM";
                    }
                    if (settimetxt.Text == "8 49 PM")
                    {
                        timereplacetxt.Text = "08:49:00:PM";
                    }
                    if (settimetxt.Text == "8 50 PM")
                    {
                        timereplacetxt.Text = "08:50:00:PM";
                    }
                    if (settimetxt.Text == "8 51 PM")
                    {
                        timereplacetxt.Text = "08:51:00:PM";
                    }
                    if (settimetxt.Text == "8 52 PM")
                    {
                        timereplacetxt.Text = "08:52:00:PM";
                    }
                    if (settimetxt.Text == "8 53 PM")
                    {
                        timereplacetxt.Text = "08:53:00:PM";
                    }
                    if (settimetxt.Text == "8 54 PM")
                    {
                        timereplacetxt.Text = "08:54:00:PM";
                    }
                    if (settimetxt.Text == "8 55 PM")
                    {
                        timereplacetxt.Text = "08:55:00:PM";
                    }
                    if (settimetxt.Text == "8 56 PM")
                    {
                        timereplacetxt.Text = "08:56:00:PM";
                    }
                    if (settimetxt.Text == "8 57 PM")
                    {
                        timereplacetxt.Text = "08:57:00:PM";
                    }
                    if (settimetxt.Text == "8 58 PM")
                    {
                        timereplacetxt.Text = "08:58:00:PM";
                    }
                    if (settimetxt.Text == "8 59 PM")
                    {
                        timereplacetxt.Text = "08:59:00:PM";
                    }
                    if (settimetxt.Text == "9 PM")
                    {
                        timereplacetxt.Text = "09:00:00:PM";
                    }
                    if (settimetxt.Text == "9 1 PM")
                    {
                        timereplacetxt.Text = "09:01:00:PM";
                    }
                    if (settimetxt.Text == "9 2 PM")
                    {
                        timereplacetxt.Text = "09:02:00:PM";
                    }
                    if (settimetxt.Text == "9 3 PM")
                    {
                        timereplacetxt.Text = "09:03:00:PM";
                    }
                    if (settimetxt.Text == "9 4 PM")
                    {
                        timereplacetxt.Text = "09:04:00:PM";
                    }
                    if (settimetxt.Text == "9 5 PM")
                    {
                        timereplacetxt.Text = "09:05:00:PM";
                    }
                    if (settimetxt.Text == "9 6 PM")
                    {
                        timereplacetxt.Text = "09:06:00:PM";
                    }
                    if (settimetxt.Text == "9 7 PM")
                    {
                        timereplacetxt.Text = "09:07:00:PM";
                    }
                    if (settimetxt.Text == "9 8 PM")
                    {
                        timereplacetxt.Text = "09:08:00:PM";
                    }
                    if (settimetxt.Text == "9 9 PM")
                    {
                        timereplacetxt.Text = "09:09:00:PM";
                    }
                    if (settimetxt.Text == "9 10 PM")
                    {
                        timereplacetxt.Text = "09:10:00:PM";
                    }
                    if (settimetxt.Text == "9 11 PM")
                    {
                        timereplacetxt.Text = "09:11:00:PM";
                    }
                    if (settimetxt.Text == "9 12 PM")
                    {
                        timereplacetxt.Text = "09:12:00:PM";
                    }
                    if (settimetxt.Text == "9 13 PM")
                    {
                        timereplacetxt.Text = "09:13:00:PM";
                    }
                    if (settimetxt.Text == "9 14 PM")
                    {
                        timereplacetxt.Text = "09:14:00:PM";
                    }
                    if (settimetxt.Text == "9 15 PM")
                    {
                        timereplacetxt.Text = "09:15:00:PM";
                    }
                    if (settimetxt.Text == "9 16 PM")
                    {
                        timereplacetxt.Text = "09:16:00:PM";
                    }
                    if (settimetxt.Text == "9 17 PM")
                    {
                        timereplacetxt.Text = "09:17:00:PM";
                    }
                    if (settimetxt.Text == "9 18 PM")
                    {
                        timereplacetxt.Text = "09:18:00:PM";
                    }
                    if (settimetxt.Text == "9 19 PM")
                    {
                        timereplacetxt.Text = "09:19:00:PM";
                    }
                    if (settimetxt.Text == "9 20 PM")
                    {
                        timereplacetxt.Text = "09:20:00:PM";
                    }
                    if (settimetxt.Text == "9 21 PM")
                    {
                        timereplacetxt.Text = "09:21:00:PM";
                    }
                    if (settimetxt.Text == "9 22 PM")
                    {
                        timereplacetxt.Text = "09:22:00:PM";
                    }
                    if (settimetxt.Text == "9 23 PM")
                    {
                        timereplacetxt.Text = "09:23:00:PM";
                    }
                    if (settimetxt.Text == "9 24 PM")
                    {
                        timereplacetxt.Text = "09:24:00:PM";
                    }
                    if (settimetxt.Text == "9 25 PM")
                    {
                        timereplacetxt.Text = "09:25:00:PM";
                    }
                    if (settimetxt.Text == "9 26 PM")
                    {
                        timereplacetxt.Text = "09:26:00:PM";
                    }
                    if (settimetxt.Text == "9 27 PM")
                    {
                        timereplacetxt.Text = "09:27:00:PM";
                    }
                    if (settimetxt.Text == "9 28 PM")
                    {
                        timereplacetxt.Text = "09:28:00:PM";
                    }
                    if (settimetxt.Text == "9 29 PM")
                    {
                        timereplacetxt.Text = "09:29:00:PM";
                    }
                    if (settimetxt.Text == "9 30 PM")
                    {
                        timereplacetxt.Text = "09:30:00:PM";
                    }
                    if (settimetxt.Text == "9 31 PM")
                    {
                        timereplacetxt.Text = "09:31:00:PM";
                    }
                    if (settimetxt.Text == "9 32 PM")
                    {
                        timereplacetxt.Text = "09:32:00:PM";
                    }
                    if (settimetxt.Text == "9 33 PM")
                    {
                        timereplacetxt.Text = "09:33:00:PM";
                    }
                    if (settimetxt.Text == "9 34 PM")
                    {
                        timereplacetxt.Text = "09:34:00:PM";
                    }
                    if (settimetxt.Text == "9 35 PM")
                    {
                        timereplacetxt.Text = "09:35:00:PM";
                    }
                    if (settimetxt.Text == "9 36 PM")
                    {
                        timereplacetxt.Text = "09:36:00:PM";
                    }
                    if (settimetxt.Text == "9 37 PM")
                    {
                        timereplacetxt.Text = "09:37:00:PM";
                    }
                    if (settimetxt.Text == "9 38 PM")
                    {
                        timereplacetxt.Text = "09:38:00:PM";
                    }
                    if (settimetxt.Text == "9 39 PM")
                    {
                        timereplacetxt.Text = "09:39:00:PM";
                    }
                    if (settimetxt.Text == "9 40 PM")
                    {
                        timereplacetxt.Text = "09:40:00:PM";
                    }
                    if (settimetxt.Text == "9 41 PM")
                    {
                        timereplacetxt.Text = "09:41:00:PM";
                    }
                    if (settimetxt.Text == "9 42 PM")
                    {
                        timereplacetxt.Text = "09:42:00:PM";
                    }
                    if (settimetxt.Text == "9 43 PM")
                    {
                        timereplacetxt.Text = "09:43:00:PM";
                    }
                    if (settimetxt.Text == "9 44 PM")
                    {
                        timereplacetxt.Text = "09:44:00:PM";
                    }
                    if (settimetxt.Text == "9 45 PM")
                    {
                        timereplacetxt.Text = "09:45:00:PM";
                    }
                    if (settimetxt.Text == "9 46 PM")
                    {
                        timereplacetxt.Text = "09:46:00:PM";
                    }
                    if (settimetxt.Text == "9 47 PM")
                    {
                        timereplacetxt.Text = "09:47:00:PM";
                    }
                    if (settimetxt.Text == "9 48 PM")
                    {
                        timereplacetxt.Text = "09:48:00:PM";
                    }
                    if (settimetxt.Text == "9 49 PM")
                    {
                        timereplacetxt.Text = "09:49:00:PM";
                    }
                    if (settimetxt.Text == "9 50 PM")
                    {
                        timereplacetxt.Text = "09:50:00:PM";
                    }
                    if (settimetxt.Text == "9 51 PM")
                    {
                        timereplacetxt.Text = "09:51:00:PM";
                    }
                    if (settimetxt.Text == "9 52 PM")
                    {
                        timereplacetxt.Text = "09:52:00:PM";
                    }
                    if (settimetxt.Text == "9 53 PM")
                    {
                        timereplacetxt.Text = "09:53:00:PM";
                    }
                    if (settimetxt.Text == "9 54 PM")
                    {
                        timereplacetxt.Text = "09:54:00:PM";
                    }
                    if (settimetxt.Text == "9 55 PM")
                    {
                        timereplacetxt.Text = "09:55:00:PM";
                    }
                    if (settimetxt.Text == "9 56 PM")
                    {
                        timereplacetxt.Text = "09:56:00:PM";
                    }
                    if (settimetxt.Text == "9 57 PM")
                    {
                        timereplacetxt.Text = "09:57:00:PM";
                    }
                    if (settimetxt.Text == "9 58 PM")
                    {
                        timereplacetxt.Text = "09:58:00:PM";
                    }
                    if (settimetxt.Text == "9 59 PM")
                    {
                        timereplacetxt.Text = "09:59:00:PM";
                    }
                    if (settimetxt.Text == "10 PM")
                    {
                        timereplacetxt.Text = "10:00:00:PM";
                    }
                    if (settimetxt.Text == "10 1 PM")
                    {
                        timereplacetxt.Text = "10:01:00:PM";
                    }
                    if (settimetxt.Text == "10 2 PM")
                    {
                        timereplacetxt.Text = "10:02:00:PM";
                    }
                    if (settimetxt.Text == "10 3 PM")
                    {
                        timereplacetxt.Text = "10:03:00:PM";
                    }
                    if (settimetxt.Text == "10 4 PM")
                    {
                        timereplacetxt.Text = "10:04:00:PM";
                    }
                    if (settimetxt.Text == "10 5 PM")
                    {
                        timereplacetxt.Text = "10:05:00:PM";
                    }
                    if (settimetxt.Text == "10 6 PM")
                    {
                        timereplacetxt.Text = "10:06:00:PM";
                    }
                    if (settimetxt.Text == "10 7 PM")
                    {
                        timereplacetxt.Text = "10:07:00:PM";
                    }
                    if (settimetxt.Text == "10 8 PM")
                    {
                        timereplacetxt.Text = "10:08:00:PM";
                    }
                    if (settimetxt.Text == "10 9 PM")
                    {
                        timereplacetxt.Text = "10:09:00:PM";
                    }
                    if (settimetxt.Text == "10 10 PM")
                    {
                        timereplacetxt.Text = "10:10:00:PM";
                    }
                    if (settimetxt.Text == "10 11 PM")
                    {
                        timereplacetxt.Text = "10:11:00:PM";
                    }
                    if (settimetxt.Text == "10 12 PM")
                    {
                        timereplacetxt.Text = "10:12:00:PM";
                    }
                    if (settimetxt.Text == "10 13 PM")
                    {
                        timereplacetxt.Text = "10:13:00:PM";
                    }
                    if (settimetxt.Text == "10 14 PM")
                    {
                        timereplacetxt.Text = "10:14:00:PM";
                    }
                    if (settimetxt.Text == "10 15 PM")
                    {
                        timereplacetxt.Text = "10:15:00:PM";
                    }
                    if (settimetxt.Text == "10 16 PM")
                    {
                        timereplacetxt.Text = "10:16:00:PM";
                    }
                    if (settimetxt.Text == "10 17 PM")
                    {
                        timereplacetxt.Text = "10:17:00:PM";
                    }
                    if (settimetxt.Text == "10 18 PM")
                    {
                        timereplacetxt.Text = "10:18:00:PM";
                    }
                    if (settimetxt.Text == "10 19 PM")
                    {
                        timereplacetxt.Text = "10:19:00:PM";
                    }
                    if (settimetxt.Text == "10 20 PM")
                    {
                        timereplacetxt.Text = "10:20:00:PM";
                    }
                    if (settimetxt.Text == "10 21 PM")
                    {
                        timereplacetxt.Text = "10:21:00:PM";
                    }
                    if (settimetxt.Text == "10 22 PM")
                    {
                        timereplacetxt.Text = "10:22:00:PM";
                    }
                    if (settimetxt.Text == "10 23 PM")
                    {
                        timereplacetxt.Text = "10:23:00:PM";
                    }
                    if (settimetxt.Text == "10 24 PM")
                    {
                        timereplacetxt.Text = "10:24:00:PM";
                    }
                    if (settimetxt.Text == "10 25 PM")
                    {
                        timereplacetxt.Text = "10:25:00:PM";
                    }
                    if (settimetxt.Text == "10 26 PM")
                    {
                        timereplacetxt.Text = "10:26:00:PM";
                    }
                    if (settimetxt.Text == "10 27 PM")
                    {
                        timereplacetxt.Text = "10:27:00:PM";
                    }
                    if (settimetxt.Text == "10 28 PM")
                    {
                        timereplacetxt.Text = "10:28:00:PM";
                    }
                    if (settimetxt.Text == "10 29 PM")
                    {
                        timereplacetxt.Text = "10:29:00:PM";
                    }
                    if (settimetxt.Text == "10 30 PM")
                    {
                        timereplacetxt.Text = "10:30:00:PM";
                    }
                    if (settimetxt.Text == "10 31 PM")
                    {
                        timereplacetxt.Text = "10:31:00:PM";
                    }
                    if (settimetxt.Text == "10 32 PM")
                    {
                        timereplacetxt.Text = "10:32:00:PM";
                    }
                    if (settimetxt.Text == "10 33 PM")
                    {
                        timereplacetxt.Text = "10:33:00:PM";
                    }
                    if (settimetxt.Text == "10 34 PM")
                    {
                        timereplacetxt.Text = "10:34:00:PM";
                    }
                    if (settimetxt.Text == "10 35 PM")
                    {
                        timereplacetxt.Text = "10:35:00:PM";
                    }
                    if (settimetxt.Text == "10 36 PM")
                    {
                        timereplacetxt.Text = "10:36:00:PM";
                    }
                    if (settimetxt.Text == "10 37 PM")
                    {
                        timereplacetxt.Text = "10:37:00:PM";
                    }
                    if (settimetxt.Text == "10 38 PM")
                    {
                        timereplacetxt.Text = "10:38:00:PM";
                    }
                    if (settimetxt.Text == "10 39 PM")
                    {
                        timereplacetxt.Text = "10:39:00:PM";
                    }
                    if (settimetxt.Text == "10 40 PM")
                    {
                        timereplacetxt.Text = "10:40:00:PM";
                    }
                    if (settimetxt.Text == "10 41 PM")
                    {
                        timereplacetxt.Text = "10:41:00:PM";
                    }
                    if (settimetxt.Text == "10 42 PM")
                    {
                        timereplacetxt.Text = "10:42:00:PM";
                    }
                    if (settimetxt.Text == "10 43 PM")
                    {
                        timereplacetxt.Text = "10:43:00:PM";
                    }
                    if (settimetxt.Text == "10 44 PM")
                    {
                        timereplacetxt.Text = "10:44:00:PM";
                    }
                    if (settimetxt.Text == "10 45 PM")
                    {
                        timereplacetxt.Text = "10:45:00:PM";
                    }
                    if (settimetxt.Text == "10 46 PM")
                    {
                        timereplacetxt.Text = "10:46:00:PM";
                    }
                    if (settimetxt.Text == "10 47 PM")
                    {
                        timereplacetxt.Text = "10:47:00:PM";
                    }
                    if (settimetxt.Text == "10 48 PM")
                    {
                        timereplacetxt.Text = "10:48:00:PM";
                    }
                    if (settimetxt.Text == "10 49 PM")
                    {
                        timereplacetxt.Text = "10:49:00:PM";
                    }
                    if (settimetxt.Text == "10 50 PM")
                    {
                        timereplacetxt.Text = "10:50:00:PM";
                    }
                    if (settimetxt.Text == "10 51 PM")
                    {
                        timereplacetxt.Text = "10:51:00:PM";
                    }
                    if (settimetxt.Text == "10 52 PM")
                    {
                        timereplacetxt.Text = "10:52:00:PM";
                    }
                    if (settimetxt.Text == "10 53 PM")
                    {
                        timereplacetxt.Text = "10:53:00:PM";
                    }
                    if (settimetxt.Text == "10 54 PM")
                    {
                        timereplacetxt.Text = "10:54:00:PM";
                    }
                    if (settimetxt.Text == "10 55 PM")
                    {
                        timereplacetxt.Text = "10:55:00:PM";
                    }
                    if (settimetxt.Text == "10 56 PM")
                    {
                        timereplacetxt.Text = "10:56:00:PM";
                    }
                    if (settimetxt.Text == "10 57 PM")
                    {
                        timereplacetxt.Text = "10:57:00:PM";
                    }
                    if (settimetxt.Text == "10 58 PM")
                    {
                        timereplacetxt.Text = "10:58:00:PM";
                    }
                    if (settimetxt.Text == "10 59 PM")
                    {
                        timereplacetxt.Text = "10:59:00:PM";
                    }
                    if (settimetxt.Text == "11 PM")
                    {
                        timereplacetxt.Text = "11:00:00:PM";
                    }
                    if (settimetxt.Text == "11 1 PM")
                    {
                        timereplacetxt.Text = "11:01:00:PM";
                    }
                    if (settimetxt.Text == "11 2 PM")
                    {
                        timereplacetxt.Text = "11:02:00:PM";
                    }
                    if (settimetxt.Text == "11 3 PM")
                    {
                        timereplacetxt.Text = "11:03:00:PM";
                    }
                    if (settimetxt.Text == "11 4 PM")
                    {
                        timereplacetxt.Text = "11:04:00:PM";
                    }
                    if (settimetxt.Text == "11 5 PM")
                    {
                        timereplacetxt.Text = "11:05:00:PM";
                    }
                    if (settimetxt.Text == "11 6 PM")
                    {
                        timereplacetxt.Text = "11:06:00:PM";
                    }
                    if (settimetxt.Text == "11 7 PM")
                    {
                        timereplacetxt.Text = "11:07:00:PM";
                    }
                    if (settimetxt.Text == "11 8 PM")
                    {
                        timereplacetxt.Text = "11:08:00:PM";
                    }
                    if (settimetxt.Text == "11 9 PM")
                    {
                        timereplacetxt.Text = "11:09:00:PM";
                    }
                    if (settimetxt.Text == "11 10 PM")
                    {
                        timereplacetxt.Text = "11:10:00:PM";
                    }
                    if (settimetxt.Text == "11 11 PM")
                    {
                        timereplacetxt.Text = "11:11:00:PM";
                    }
                    if (settimetxt.Text == "11 12 PM")
                    {
                        timereplacetxt.Text = "11:12:00:PM";
                    }
                    if (settimetxt.Text == "11 13 PM")
                    {
                        timereplacetxt.Text = "11:13:00:PM";
                    }
                    if (settimetxt.Text == "11 14 PM")
                    {
                        timereplacetxt.Text = "11:14:00:PM";
                    }
                    if (settimetxt.Text == "11 15 PM")
                    {
                        timereplacetxt.Text = "11:15:00:PM";
                    }
                    if (settimetxt.Text == "11 16 PM")
                    {
                        timereplacetxt.Text = "11:16:00:PM";
                    }
                    if (settimetxt.Text == "11 17 PM")
                    {
                        timereplacetxt.Text = "11:17:00:PM";
                    }
                    if (settimetxt.Text == "11 18 PM")
                    {
                        timereplacetxt.Text = "11:18:00:PM";
                    }
                    if (settimetxt.Text == "11 19 PM")
                    {
                        timereplacetxt.Text = "11:19:00:PM";
                    }
                    if (settimetxt.Text == "11 20 PM")
                    {
                        timereplacetxt.Text = "11:20:00:PM";
                    }
                    if (settimetxt.Text == "11 21 PM")
                    {
                        timereplacetxt.Text = "11:21:00:PM";
                    }
                    if (settimetxt.Text == "11 22 PM")
                    {
                        timereplacetxt.Text = "11:22:00:PM";
                    }
                    if (settimetxt.Text == "11 23 PM")
                    {
                        timereplacetxt.Text = "11:23:00:PM";
                    }
                    if (settimetxt.Text == "11 24 PM")
                    {
                        timereplacetxt.Text = "11:24:00:PM";
                    }
                    if (settimetxt.Text == "11 25 PM")
                    {
                        timereplacetxt.Text = "11:25:00:PM";
                    }
                    if (settimetxt.Text == "11 26 PM")
                    {
                        timereplacetxt.Text = "11:26:00:PM";
                    }
                    if (settimetxt.Text == "11 27 PM")
                    {
                        timereplacetxt.Text = "11:27:00:PM";
                    }
                    if (settimetxt.Text == "11 28 PM")
                    {
                        timereplacetxt.Text = "11:28:00:PM";
                    }
                    if (settimetxt.Text == "11 29 PM")
                    {
                        timereplacetxt.Text = "11:29:00:PM";
                    }
                    if (settimetxt.Text == "11 30 PM")
                    {
                        timereplacetxt.Text = "11:30:00:PM";
                    }
                    if (settimetxt.Text == "11 31 PM")
                    {
                        timereplacetxt.Text = "11:31:00:PM";
                    }
                    if (settimetxt.Text == "11 32 PM")
                    {
                        timereplacetxt.Text = "11:32:00:PM";
                    }
                    if (settimetxt.Text == "11 33 PM")
                    {
                        timereplacetxt.Text = "11:33:00:PM";
                    }
                    if (settimetxt.Text == "11 34 PM")
                    {
                        timereplacetxt.Text = "11:34:00:PM";
                    }
                    if (settimetxt.Text == "11 35 PM")
                    {
                        timereplacetxt.Text = "11:35:00:PM";
                    }
                    if (settimetxt.Text == "11 36 PM")
                    {
                        timereplacetxt.Text = "11:36:00:PM";
                    }
                    if (settimetxt.Text == "11 37 PM")
                    {
                        timereplacetxt.Text = "11:37:00:PM";
                    }
                    if (settimetxt.Text == "11 38 PM")
                    {
                        timereplacetxt.Text = "11:38:00:PM";
                    }
                    if (settimetxt.Text == "11 39 PM")
                    {
                        timereplacetxt.Text = "11:39:00:PM";
                    }
                    if (settimetxt.Text == "11 40 PM")
                    {
                        timereplacetxt.Text = "11:40:00:PM";
                    }
                    if (settimetxt.Text == "11 41 PM")
                    {
                        timereplacetxt.Text = "11:41:00:PM";
                    }
                    if (settimetxt.Text == "11 42 PM")
                    {
                        timereplacetxt.Text = "11:42:00:PM";
                    }
                    if (settimetxt.Text == "11 43 PM")
                    {
                        timereplacetxt.Text = "11:43:00:PM";
                    }
                    if (settimetxt.Text == "11 44 PM")
                    {
                        timereplacetxt.Text = "11:44:00:PM";
                    }
                    if (settimetxt.Text == "11 45 PM")
                    {
                        timereplacetxt.Text = "11:45:00:PM";
                    }
                    if (settimetxt.Text == "11 46 PM")
                    {
                        timereplacetxt.Text = "11:46:00:PM";
                    }
                    if (settimetxt.Text == "11 47 PM")
                    {
                        timereplacetxt.Text = "11:47:00:PM";
                    }
                    if (settimetxt.Text == "11 48 PM")
                    {
                        timereplacetxt.Text = "11:48:00:PM";
                    }
                    if (settimetxt.Text == "11 49 PM")
                    {
                        timereplacetxt.Text = "11:49:00:PM";
                    }
                    if (settimetxt.Text == "11 50 PM")
                    {
                        timereplacetxt.Text = "11:50:00:PM";
                    }
                    if (settimetxt.Text == "11 51 PM")
                    {
                        timereplacetxt.Text = "11:51:00:PM";
                    }
                    if (settimetxt.Text == "11 52 PM")
                    {
                        timereplacetxt.Text = "11:52:00:PM";
                    }
                    if (settimetxt.Text == "11 53 PM")
                    {
                        timereplacetxt.Text = "11:53:00:PM";
                    }
                    if (settimetxt.Text == "11 54 PM")
                    {
                        timereplacetxt.Text = "11:54:00:PM";
                    }
                    if (settimetxt.Text == "11 55 PM")
                    {
                        timereplacetxt.Text = "11:55:00:PM";
                    }
                    if (settimetxt.Text == "11 56 PM")
                    {
                        timereplacetxt.Text = "11:56:00:PM";
                    }
                    if (settimetxt.Text == "11 57 PM")
                    {
                        timereplacetxt.Text = "11:57:00:PM";
                    }
                    if (settimetxt.Text == "11 58 PM")
                    {
                        timereplacetxt.Text = "11:58:00:PM";
                    }
                    if (settimetxt.Text == "11 59 PM")
                    {
                        timereplacetxt.Text = "11:59:00:PM";
                    }
                    if (settimetxt.Text == "12 PM")
                    {
                        timereplacetxt.Text = "12:00:00:PM";
                    }
                    if (settimetxt.Text == "12 1 PM")
                    {
                        timereplacetxt.Text = "12:01:00:PM";
                    }
                    if (settimetxt.Text == "12 2 PM")
                    {
                        timereplacetxt.Text = "12:02:00:PM";
                    }
                    if (settimetxt.Text == "12 3 PM")
                    {
                        timereplacetxt.Text = "12:03:00:PM";
                    }
                    if (settimetxt.Text == "12 4 PM")
                    {
                        timereplacetxt.Text = "12:04:00:PM";
                    }
                    if (settimetxt.Text == "12 5 PM")
                    {
                        timereplacetxt.Text = "12:05:00:PM";
                    }
                    if (settimetxt.Text == "12 6 PM")
                    {
                        timereplacetxt.Text = "12:06:00:PM";
                    }
                    if (settimetxt.Text == "12 7 PM")
                    {
                        timereplacetxt.Text = "12:07:00:PM";
                    }
                    if (settimetxt.Text == "12 8 PM")
                    {
                        timereplacetxt.Text = "12:08:00:PM";
                    }
                    if (settimetxt.Text == "12 9 PM")
                    {
                        timereplacetxt.Text = "12:09:00:PM";
                    }
                    if (settimetxt.Text == "12 10 PM")
                    {
                        timereplacetxt.Text = "12:10:00:PM";
                    }
                    if (settimetxt.Text == "12 11 PM")
                    {
                        timereplacetxt.Text = "12:11:00:PM";
                    }
                    if (settimetxt.Text == "12 12 PM")
                    {
                        timereplacetxt.Text = "12:12:00:PM";
                    }
                    if (settimetxt.Text == "12 13 PM")
                    {
                        timereplacetxt.Text = "12:13:00:PM";
                    }
                    if (settimetxt.Text == "12 14 PM")
                    {
                        timereplacetxt.Text = "12:14:00:PM";
                    }
                    if (settimetxt.Text == "12 15 PM")
                    {
                        timereplacetxt.Text = "12:15:00:PM";
                    }
                    if (settimetxt.Text == "12 16 PM")
                    {
                        timereplacetxt.Text = "12:16:00:PM";
                    }
                    if (settimetxt.Text == "12 17 PM")
                    {
                        timereplacetxt.Text = "12:17:00:PM";
                    }
                    if (settimetxt.Text == "12 18 PM")
                    {
                        timereplacetxt.Text = "12:18:00:PM";
                    }
                    if (settimetxt.Text == "12 19 PM")
                    {
                        timereplacetxt.Text = "12:19:00:PM";
                    }
                    if (settimetxt.Text == "12 20 PM")
                    {
                        timereplacetxt.Text = "12:20:00:PM";
                    }
                    if (settimetxt.Text == "12 21 PM")
                    {
                        timereplacetxt.Text = "12:21:00:PM";
                    }
                    if (settimetxt.Text == "12 22 PM")
                    {
                        timereplacetxt.Text = "12:22:00:PM";
                    }
                    if (settimetxt.Text == "12 23 PM")
                    {
                        timereplacetxt.Text = "12:23:00:PM";
                    }
                    if (settimetxt.Text == "12 24 PM")
                    {
                        timereplacetxt.Text = "12:24:00:PM";
                    }
                    if (settimetxt.Text == "12 25 PM")
                    {
                        timereplacetxt.Text = "12:25:00:PM";
                    }
                    if (settimetxt.Text == "12 26 PM")
                    {
                        timereplacetxt.Text = "12:26:00:PM";
                    }
                    if (settimetxt.Text == "12 27 PM")
                    {
                        timereplacetxt.Text = "12:27:00:PM";
                    }
                    if (settimetxt.Text == "12 28 PM")
                    {
                        timereplacetxt.Text = "12:28:00:PM";
                    }
                    if (settimetxt.Text == "12 29 PM")
                    {
                        timereplacetxt.Text = "12:29:00:PM";
                    }
                    if (settimetxt.Text == "12 30 PM")
                    {
                        timereplacetxt.Text = "12:30:00:PM";
                    }
                    if (settimetxt.Text == "12 31 PM")
                    {
                        timereplacetxt.Text = "12:31:00:PM";
                    }
                    if (settimetxt.Text == "12 32 PM")
                    {
                        timereplacetxt.Text = "12:32:00:PM";
                    }
                    if (settimetxt.Text == "12 33 PM")
                    {
                        timereplacetxt.Text = "12:33:00:PM";
                    }
                    if (settimetxt.Text == "12 34 PM")
                    {
                        timereplacetxt.Text = "12:34:00:PM";
                    }
                    if (settimetxt.Text == "12 35 PM")
                    {
                        timereplacetxt.Text = "12:35:00:PM";
                    }
                    if (settimetxt.Text == "12 36 PM")
                    {
                        timereplacetxt.Text = "12:36:00:PM";
                    }
                    if (settimetxt.Text == "12 37 PM")
                    {
                        timereplacetxt.Text = "12:37:00:PM";
                    }
                    if (settimetxt.Text == "12 38 PM")
                    {
                        timereplacetxt.Text = "12:38:00:PM";
                    }
                    if (settimetxt.Text == "12 39 PM")
                    {
                        timereplacetxt.Text = "12:39:00:PM";
                    }
                    if (settimetxt.Text == "12 40 PM")
                    {
                        timereplacetxt.Text = "12:40:00:PM";
                    }
                    if (settimetxt.Text == "12 41 PM")
                    {
                        timereplacetxt.Text = "12:41:00:PM";
                    }
                    if (settimetxt.Text == "12 42 PM")
                    {
                        timereplacetxt.Text = "12:42:00:PM";
                    }
                    if (settimetxt.Text == "12 43 PM")
                    {
                        timereplacetxt.Text = "12:43:00:PM";
                    }
                    if (settimetxt.Text == "12 44 PM")
                    {
                        timereplacetxt.Text = "12:44:00:PM";
                    }
                    if (settimetxt.Text == "12 45 PM")
                    {
                        timereplacetxt.Text = "12:45:00:PM";
                    }
                    if (settimetxt.Text == "12 46 PM")
                    {
                        timereplacetxt.Text = "12:46:00:PM";
                    }
                    if (settimetxt.Text == "12 47 PM")
                    {
                        timereplacetxt.Text = "12:47:00:PM";
                    }
                    if (settimetxt.Text == "12 48 PM")
                    {
                        timereplacetxt.Text = "12:48:00:PM";
                    }
                    if (settimetxt.Text == "12 49 PM")
                    {
                        timereplacetxt.Text = "12:49:00:PM";
                    }
                    if (settimetxt.Text == "12 50 PM")
                    {
                        timereplacetxt.Text = "12:50:00:PM";
                    }
                    if (settimetxt.Text == "12 51 PM")
                    {
                        timereplacetxt.Text = "12:51:00:PM";
                    }
                    if (settimetxt.Text == "12 52 PM")
                    {
                        timereplacetxt.Text = "12:52:00:PM";
                    }
                    if (settimetxt.Text == "12 53 PM")
                    {
                        timereplacetxt.Text = "12:53:00:PM";
                    }
                    if (settimetxt.Text == "12 54 PM")
                    {
                        timereplacetxt.Text = "12:54:00:PM";
                    }
                    if (settimetxt.Text == "12 55 PM")
                    {
                        timereplacetxt.Text = "12:55:00:PM";
                    }
                    if (settimetxt.Text == "12 56 PM")
                    {
                        timereplacetxt.Text = "12:56:00:PM";
                    }
                    if (settimetxt.Text == "12 57 PM")
                    {
                        timereplacetxt.Text = "12:57:00:PM";
                    }
                    if (settimetxt.Text == "12 58 PM")
                    {
                        timereplacetxt.Text = "12:58:00:PM";
                    }
                    if (settimetxt.Text == "12 59 PM")
                    {
                        timereplacetxt.Text = "12:59:00:PM";
                    }
                    speechRecognitionEngine.UnloadAllGrammars();
                    UnloadGrammarAndCommands();
                    btnset.PerformClick();
                    break;
                case "stop talking":
                    if (Marvel.State == SynthesizerState.Paused)
                        Marvel.Resume();
                    Marvel.SpeakAsyncCancelAll();
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
        string music;
        private void btnset_Click(object sender, EventArgs e)
        {
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw0 = new StreamWriter(Environment.CurrentDirectory + "\\remindertimeset.txt");

                //Write a line of text


                sw0.WriteLine(settimetxt.Text + " " + reminderset.Text);

                //Write a second line of text
                //sw.WriteLine("From the StreamWriter class");

                //Close the file
                sw0.Close();
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "\\remindertxt.txt");

                //Write a line of text
                sw.WriteLine(remindermsgtxt.Text);

                //Write a second line of text
                //sw.WriteLine("From the StreamWriter class");

                //Close the file
                sw.Close();
            }
            catch (Exception)
            {
                Marvel.Speak("Try again");
            }
            reminderset.Text = daysofweek.Text;
            time_s.Text = timereplacetxt.Text;
            //dateTimePicker1.Text = "";
            time_set.Start();
            openmusicbtn.PerformClick();
        }
        public String GetWeather(String input)

        {
            //string put = "https://query.yahooapis.com/v1/public/yql?q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text='athens, gr')&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
            //string output = put.Replace("city", inputtxt.Text);
            //Console.WriteLine(output);
            String query = String.Format("https://query.yahooapis.com/v1/public/yql?q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text='city, state')&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");

            String lines;
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader(@"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Weather City.txt");

            //Read the first line of text
            lines = sr.ReadLine();
            string output = query.Replace("city", lines);
            //close the file
            XmlDocument wData = new XmlDocument();

            wData.Load(output);
            XmlNamespaceManager manager = new XmlNamespaceManager(wData.NameTable);

            manager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");

            XmlNode channel = wData.SelectSingleNode("query").SelectSingleNode("results").SelectSingleNode("channel");

            XmlNodeList nodes = wData.SelectNodes("query/results/channel");

            try
            {
                temp = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["temp"].Value;

                condition = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["text"].Value;

                high = channel.SelectSingleNode("item").SelectSingleNode("yweather:forecast", manager).Attributes["high"].Value;

                low = channel.SelectSingleNode("item").SelectSingleNode("yweather:forecast", manager).Attributes["low"].Value;

                humidity = channel.SelectSingleNode("yweather:atmosphere", manager).Attributes["humidity"].Value;

                windspeed = channel.SelectSingleNode("yweather:wind", manager).Attributes["chill"].Value;

                sunrise = channel.SelectSingleNode("yweather:astronomy", manager).Attributes["sunrise"].Value;

                sunset = channel.SelectSingleNode("yweather:astronomy", manager).Attributes["sunset"].Value;

                cdata = channel.SelectSingleNode("item").SelectSingleNode("description").InnerText;

                if (input == "temp")
                {
                    return temp;
                }
                if (input == "high")
                {
                    return high;
                }
                if (input == "low")
                {
                    return low;
                }
                if (input == "cond")
                {
                    return condition;
                }
                if (input == "humidity")
                {
                    return humidity;
                }
                if (input == "chill")
                {
                    return windspeed;
                }
                if (input == "sunrise")
                {
                    return sunrise;
                }
                if (input == "sunset")
                {
                    return sunset;
                }
                sr.Close();
                Console.ReadLine();
            }

            catch
            {
                return "Error Reciving data";

            }
            return "error";
        }
        private void btnstop_Click(object sender, EventArgs e)
        {
            Media.Ctlcontrols.stop();
            time_set.Stop();
            this.Hide();
            String lines;
            String msglines;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(Environment.CurrentDirectory + "\\name.txt");
                //Read the first line of text
                lines = sr.ReadLine();
                if (time_n.Text != "PM")
                {
                    spokentxt.Text = "Good morning, " + lines;
                    Marvel.SpeakAsync(spokentxt.Text);
                    spokentxt.Text = "what a pleasant, day";
                    Marvel.SpeakAsync(spokentxt.Text);
                }

                else
                {
                    spokentxt.Text = "Good evening, " + lines;
                    Marvel.SpeakAsync(spokentxt.Text);
                    spokentxt.Text = "what a pleasant, evening";
                    Marvel.SpeakAsync(spokentxt.Text);
                }
                WakeUpAlarm wua = new WakeUpAlarm();
                wua.Show();
                wua.TopMost = true;
                System.DateTime now = System.DateTime.Now;
                string time = now.GetDateTimeFormats('t')[0];
                spokentxt.Text = "The time is, " + time;
                Marvel.SpeakAsync(spokentxt.Text);
                StreamReader sr2 = new StreamReader(Environment.CurrentDirectory + "\\remindertxt.txt");
                msglines = sr2.ReadLine();
                spokentxt.Text = "Today you have" + msglines;
                Marvel.SpeakAsync(spokentxt.Text);
                //close the file
                sr.Close();
                sr2.Close();
                Console.ReadLine();

            }
            catch (Exception)
            {
                Marvel.Speak("");

            }
            finally
            {
                Marvel.Speak("");
            }
            labelnet.Text = NetworkInterface.GetIsNetworkAvailable().ToString();
            if (labelnet.Text != "True")
            {
                spokentxt.Text = "Please check your internet connection, before to get weather and news report";
                Marvel.SpeakAsync(spokentxt.Text);
                this.Close();
            }
            else
            {
                spokentxt.Text = "Today weather forecast is,";
                Marvel.SpeakAsync(spokentxt.Text);
                spokentxt.Text = "The temperature is, " + GetWeather("temp");
                Marvel.SpeakAsync(spokentxt.Text);
                spokentxt.Text = "The condition is, " + GetWeather("cond");
                Marvel.SpeakAsync(spokentxt.Text);
                spokentxt.Text = "The high is, " + GetWeather("high");
                Marvel.SpeakAsync(spokentxt.Text);
                spokentxt.Text = "The low is, " + GetWeather("low");
                Marvel.SpeakAsync(spokentxt.Text);
                spokentxt.Text = "The humidity is, " + GetWeather("humidity");
                Marvel.SpeakAsync(spokentxt.Text);
                spokentxt.Text = "wind speed is, " + GetWeather("chill") + " miles per hour";
                Marvel.SpeakAsync(spokentxt.Text);
                spokentxt.Text = "sun rise at, " + GetWeather("sunrise");
                Marvel.SpeakAsync(spokentxt.Text);
                spokentxt.Text = "sun set at, " + GetWeather("sunset");
                Marvel.SpeakAsync(spokentxt.Text);
                spokentxt.Text = "Now, here is news report";
                Marvel.SpeakAsync(spokentxt.Text);
                WebClient webc = new WebClient();
                string page = webc.DownloadString("http://www.bing.com/news/search?q=&p1=%5bNewsVertical+Category%3d%22rt_World%22%5d&FORM=NWBTCB");
                //string news = "<span name=\"desc\" class=\"snippet\">(.*?)</span>";
                //string news = "<span class=\"st\">(.*?)</span>";
                string news = "<span class=\"sn_snip\">(.*?)</span>";
                news = "<div class=\"snippet\">(.*?)</div>";
                foreach (Match match in Regex.Matches(page, news))
                    newstxt.Items.Add(match.Groups[1].Value);
                foreach (string s in newstxt.Items)
                {
                    string name = s;
                    Marvel.SpeakAsync(s);

                }
                Marvel.SpeakAsync("Here is google news report");
                WebClient webc2 = new WebClient();
                string page2 = webc.DownloadString("https://news.google.com/?edchanged=1&ned=us&authuser=0");
                //string news = "<span name=\"desc\" class=\"snippet\">(.*?)</span>";
                //string news = "<span class=\"st\">(.*?)</span>";
                string news2 = "<div class=\"esc-lead-snippet-wrapper\">(.*?)</div>";
                foreach (Match match in Regex.Matches(page2, news2))
                    newstxt.Items.Add(match.Groups[1].Value);
                foreach (string s in newstxt.Items)
                {
                    string name = s;
                    Marvel.SpeakAsync(s);

                }
                this.Close();
            }
        }

        private void openmusicbtn_Click(object sender, EventArgs e)
        {
            Marvel.SpeakAsync("Alarm and Reminder added, successfully");
            Marvel.SpeakAsync("Do you wanna add music for reminder");
            Marvel.SpeakAsync("please select, audio music file from your drive for wake up alarm ");
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                music = open.FileName;
                musicnamebox.Text = open.SafeFileName;
                Marvel.SpeakAsync("your reminder is fixed, what else can i do for you, master");
                Thread.Sleep(1500);
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Minimized;
                TopMost = false;
                UnloadGrammarAndCommands();
            }
            else
            {
                Marvel.SpeakAsync("fix the error and try again");
            }
        }

        private void time_now_Tick(object sender, EventArgs e)
        {
            string tline = time_n.Text;
            //time_n.Text = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
            time_n.Text = System.DateTime.Now.ToString("hh:mm:ss:tt");
        }
        private void Media_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 8)
            {
                Media.settings.setMode("loop", true);
            }
            // MediaEnded
            // call function to play the video again     
        }
        private void time_set_Tick(object sender, EventArgs e)
        {
            reminderset.Text = daysofweek.Text;
            if (time_n.Text == time_s.Text)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Normal;
                TopMost = true;
                Media.URL = music;
                loadGrammarAndCommands();
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            speechRecognitionEngine.RecognizeAsyncStop();
            //    // clean references
            speechRecognitionEngine.Dispose();
            this.Close();
        }

        private void Reminder_Load(object sender, EventArgs e)
        {
            Media.PlayStateChange += Media_PlayStateChange;
            Media.uiMode = "none";
            Reminder ra = new Reminder();
            ra.TopMost = true;
            Marvel.Speak("I am ready master, if you need help, please check, voice commands list for more info");
            btnset.Enabled = false;
        }

        private void resetbtn_Click(object sender, EventArgs e)
        {
            loadGrammarAndCommands();
            time_s.Text = String.Empty;
            musicnamebox.Text = String.Empty;
            daysofweek.Text = String.Empty;
            reminderset.Text = String.Empty;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            TopMost = true;
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter swriter = new StreamWriter(Environment.CurrentDirectory + "\\remindertimeset.txt");

                //Write a line of text


                swriter.WriteLine(String.Empty);

                //Write a second line of text
                //sw.WriteLine("From the StreamWriter class");

                //Close the file
                swriter.Close();
            }
            catch (Exception)
            {
                Marvel.Speak("Try again");
            }
            //speechRecognitionEngine.UnloadAllGrammars();

        }

        private void speakbtn_Click(object sender, EventArgs e)
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