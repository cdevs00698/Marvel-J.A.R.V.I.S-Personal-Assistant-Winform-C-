using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using mshtml;
using System.Net;

namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    public partial class WeatherReport : Form
    {
        int Top;
        int MoveX;
        int MoveY;
        SpeechRecognitionEngine speechRecognitionEngine = null;
        SpeechSynthesizer Marvel = new SpeechSynthesizer();
        Grammar Weathergrammar;
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
        int i = 0;
        String[] ArrayWeatherCommands;
        String[] ArrayWeatherCity;
        public static string weatherchpath; //These strings will be used to refer to the Web Command text document
        public static string weathercitypath; //These strings will be used to refer to the Web Response text document
        public static String userName = Environment.UserName;
        StreamWriter sw;
        public WeatherReport()
        {
            InitializeComponent();
            try
            {
                // hook to events
                speechRecognitionEngine = createSpeechEngine("en-US");
                speechRecognitionEngine.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(Environment.CurrentDirectory + "\\weathercommands.txt")))));
                //speechRecognitionEngine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(engine_AudioLevelUpdated);
                speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Weather_SpeechRecognized);
                speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);
                // use the system's default microphone
                speechRecognitionEngine.SetInputToDefaultAudioDevice();
                Properties.Settings.Default.WCMD = @"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Weather Commands.txt";
                Properties.Settings.Default.WCN = @"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Weather City.txt";
                // start listening
                speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                Properties.Settings.Default.Save();
                weatherchpath = Properties.Settings.Default.WCMD;
                weathercitypath = Properties.Settings.Default.WCN;
                if (!File.Exists(MarvelJPA.weatherchpath))
                {
                    sw = File.CreateText(MarvelJPA.weatherchpath); sw.Write("Get weather report from athens greece"); sw.Close();
                }
                if (!File.Exists(MarvelJPA.weathercitypath))
                {
                    sw = File.CreateText(MarvelJPA.weathercitypath); sw.Write("athens greece"); sw.Close();
                }
                ArrayWeatherCommands = File.ReadAllLines(MarvelJPA.weatherchpath); //This loads all written commands in our Custom Commands text documents into arrays so they can be loaded into our grammars
                ArrayWeatherCity = File.ReadAllLines(MarvelJPA.weathercitypath);

                // load dictionary
                loadGrammarAndCommands();
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
        private void Weather_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;

            i = 0;
            try
            {
                foreach (string line in ArrayWeatherCommands)
                {
                    if (line == speech)
                    {
                        Marvel.SpeakAsync(ArrayWeatherCity[i]);
                        inputtxt.Text = ArrayWeatherCity[i];
                        yahooweatherbtn.PerformClick();
                    }
                    i += 1;
                }
            }
            catch
            {
                i += 1;
                Marvel.SpeakAsync("Please check the " + speech + "weather command on line " + i + ". It appears to be missing a proper response or web key words");
            }
        }
        private void loadGrammarAndCommands()
        {
            Choices texts = new Choices();
            string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\weathercommands.txt");
            // add the text to the known choices of speechengine
            texts.Add(lines);
            Grammar wordsList = new Grammar(new GrammarBuilder(texts));
            speechRecognitionEngine.LoadGrammar(wordsList);
            try
            {
                Weathergrammar = new Grammar(new GrammarBuilder(new Choices(ArrayWeatherCommands)));
                speechRecognitionEngine.LoadGrammarAsync(Weathergrammar);
            }
            catch (Exception ex)
            {
                Marvel.SpeakAsync("I've detected an in valid entry in your weather commands, possibly a blank line. web commands will cease to work until it is fixed.");
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
                //Get weather report
                case "get weather report":
                    Marvel.Speak("ok master, before your get with weather report");
                    Marvel.Speak("if your city name is in my directory, city name list, then i will search for you ");
                    Marvel.Speak("if it is not in my directory then type it on text box and press get weather report button, ");
                    Marvel.Speak("ok ready, tell me your city name");
                    break;
                case "hide weather report":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Minimized;
                    TopMost = false;
                    break;
                case "show weather report":
                case "show weather report again":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Normal;
                    TopMost = true;
                    break;
                case "get weather forcast for whole week":
                    WeeklyWeather();
                    break;
                case "pause":
                    Marvel.Pause();
                    break;
                case "resume":
                    Marvel.Resume();
                    break;
                case "stop":
                    Marvel.Pause();
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
        public String GetWeather(String input)

        {
            //string put = "https://query.yahooapis.com/v1/public/yql?q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text='athens, gr')&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
            //string output = put.Replace("city", inputtxt.Text);
            //Console.WriteLine(output);
            String query = String.Format("https://query.yahooapis.com/v1/public/yql?q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text='city, state')&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");

            String lines;
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader(Environment.CurrentDirectory + "\\weatherreport.txt");

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
                if (input == "description")
                {
                    return cdata;
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
        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void weatherbrow_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }

        private void WeatherReport_Load(object sender, EventArgs e)
        {
            WeatherReport wr = new WeatherReport();
            wr.TopMost = true;
            weatherbrow.Navigate("http://www.msn.com/en-gb/weather");
            labelnet.Text = NetworkInterface.GetIsNetworkAvailable().ToString();
            if (labelnet.Text != "True")
            {
                Marvel.Speak("Please check your internet connection, before to get weather report");
                Marvel.SpeakAsyncCancelAll();
                speechRecognitionEngine.Dispose();
                this.Close();
            }
            else
            {
                Marvel.Speak("Please wait");
                //computer.SelectVoice("IVONA 2 Brian OEM");
            }
        }
        private void yahooweatherbtn_Click(object sender, EventArgs e)
        {
            try
            {

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "\\weatherreport.txt");

                //Write a line of text
                sw.WriteLine(inputtxt.Text);

                //Write a second line of text
                //sw.WriteLine("From the StreamWriter class");

                //Close the file
                sw.Close();
            }
            catch (Exception)
            {
                //computer.Speak("Your name is added");
            }
            finally
            {
                Marvel.Speak("");
            }
            temptxt.Text = "The temperature is :" + GetWeather("temp");
            Marvel.SpeakAsync(temptxt.Text);
            condtxt.Text = "The condition is :" + GetWeather("cond");
            Marvel.SpeakAsync(condtxt.Text);
            hightxt.Text = "The high is :" + GetWeather("high");
            Marvel.SpeakAsync(hightxt.Text);
            lowtxt.Text = "The low is :" + GetWeather("low");
            Marvel.SpeakAsync(lowtxt.Text);
            humiditytxt.Text = "The humidity is :" + GetWeather("humidity");
            Marvel.SpeakAsync(humiditytxt.Text);
            windspeedtxt.Text = "wind speed is :" + GetWeather("chill");
            Marvel.SpeakAsync(windspeedtxt.Text + " miles per hour");
            sunrisetxt.Text = "sun rise at :" + GetWeather("sunrise");
            Marvel.SpeakAsync(sunrisetxt.Text);
            sunsetxt.Text = "sun set at :" + GetWeather("sunset");
            Marvel.SpeakAsync(sunsetxt.Text);
        }
        private void WeeklyWeather()
        {
            ////////////////////////
            //tomorrowforcast.Text = "weather forcast for tomorrow :" + GetWeather("description");
            HtmlToText convert = new HtmlToText();
            tomorrowforcast.Text = convert.Convert(GetWeather("description"));
            string textreplace = tomorrowforcast.Text;
            string text = String.Format(tomorrowforcast.Text);
            text = text.Replace("Sat", "Saturday");
            text = text.Replace("Low", " Low");
            Marvel.SpeakAsync("now weather forcast for whole week, " + text.ToString());
            ////////////////////////////////////////////
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

