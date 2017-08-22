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
using System.Xml;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Net.NetworkInformation;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;

namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    public partial class WakeUpAlarm : Form
    {
        SpeechRecognitionEngine speechRecognitionEngine = null;
        SpeechSynthesizer Marvel = new SpeechSynthesizer();
        string userName = Environment.UserName;
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
        public WakeUpAlarm()
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
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\stopalarmcommands.txt");
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
            System.DayOfWeek today = System.DateTime.Today.DayOfWeek;
            //scvText.ScrollToEnd();
            string speech = (e.Result.Text);
            switch (speech)
            {
                //GREETINGS

                case "close wakeup alarm":
                case "close alarm":
                    this.Close();
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
        private void WakeUpAlarm_Load(object sender, EventArgs e)
        {
            //int h = Screen.PrimaryScreen.WorkingArea.Height;
            //int w = Screen.PrimaryScreen.WorkingArea.Width;
            //this.ClientSize = new Size(w, h);

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
            labelnet.Text = NetworkInterface.GetIsNetworkAvailable().ToString();

            if (labelnet.Text == "True")
            {
                weatherbg.Navigate("https://www.msn.com/en-us/weather");
                dayofweeklbl.Text = System.DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("en-US"));
                compeletdatelbl.Text = System.DateTime.Now.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("en-US"));
                templbl.Text = GetWeather("temp");
                conditionlbl.Text = GetWeather("cond");
                highlbl.Text = GetWeather("high");
                lowlbl.Text = GetWeather("low");
                humiditylbl.Text = GetWeather("humidity");
                windspeedlbl.Text = GetWeather("chill") + " miles per hour";
                sunriselbl.Text = GetWeather("sunrise");
                sunsetlbl.Text = GetWeather("sunset");


                if (conditionlbl.Text == "Tornado")
                {
                    Image image = Properties.Resources.tornado;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Tropical Storm")
                {
                    Image image = Properties.Resources.thunderstorms;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Hurricane")
                {
                    Image image = Properties.Resources.thunderstorms;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Severe Thunderstorms")
                {
                    Image image = Properties.Resources.severe_thunderstorms;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Thunderstorms")
                {
                    Image image = Properties.Resources.thunderstorms;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Mixed Rain And Snow")
                {
                    Image image = Properties.Resources.mixed_rain_and_snow;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Mixed Rain And Sleet")
                {
                    Image image = Properties.Resources.mixed_rain_and_sleet;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Freezing Drizzle")
                {
                    Image image = Properties.Resources.freezing_drizzle;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Drizzle")
                {
                    Image image = Properties.Resources.drizzle;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Freezing Rain")
                {
                    Image image = Properties.Resources.freezing_rain;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Showers")
                {
                    Image image = Properties.Resources.showers;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Snow Flurries")
                {
                    Image image = Properties.Resources.snow_flurries;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Light Snow Showers")
                {
                    Image image = Properties.Resources.light_snow_showers;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Blowing Snow")
                {
                    Image image = Properties.Resources.blowing_snow;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Snow")
                {
                    Image image = Properties.Resources.snow;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Hail")
                {
                    Image image = Properties.Resources.hail;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Sleet")
                {
                    Image image = Properties.Resources.sleet;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Dust")
                {
                    Image image = Properties.Resources.dust;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Foggy")
                {
                    Image image = Properties.Resources.foggy;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Haze")
                {
                    Image image = Properties.Resources.haze;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Smoky")
                {
                    Image image = Properties.Resources.smoky;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Blustery")
                {
                    Image image = Properties.Resources.blustery;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Windy")
                {
                    Image image = Properties.Resources.windy;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Cold")
                {
                    Image image = Properties.Resources.cold;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Cloudy")
                {
                    Image image = Properties.Resources.cloudy;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Mostly Cloudy Night")
                {
                    Image image = Properties.Resources.mostly_cloudy;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Mostly Cloudy")
                {
                    Image image = Properties.Resources.mostly_cloudy;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Partly Cloudy Night")
                {
                    Image image = Properties.Resources.mostly_cloudy;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Partly Cloudy")
                {
                    Image image = Properties.Resources.mostly_cloudy;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Clear")
                {
                    System.DateTime timenow = System.DateTime.Now;
                    if (timenow.Hour >= 5 && timenow.Hour < 12)
                    {
                        Image images = Properties.Resources.hot;
                        // Set the PictureBox image property to this image.
                        // ... Then, adjust its height and width properties.
                        weatherimg.Image = images;
                        weatherimg.Height = images.Height;
                        weatherimg.Width = images.Width;
                    }
                    if (timenow.Hour >= 12 && timenow.Hour < 18)
                    {
                        Image images = Properties.Resources.hot;
                        // Set the PictureBox image property to this image.
                        // ... Then, adjust its height and width properties.
                        weatherimg.Image = images;
                        weatherimg.Height = images.Height;
                        weatherimg.Width = images.Width;
                    }
                    if (timenow.Hour >= 18 && timenow.Hour < 24)
                    {
                        Image image = Properties.Resources.clear_night;
                        // Set the PictureBox image property to this image.
                        // ... Then, adjust its height and width properties.
                        weatherimg.Image = image;
                        weatherimg.Height = image.Height;
                        weatherimg.Width = image.Width;
                    }
                    if (timenow.Hour < 5)
                    {
                        Image image = Properties.Resources.clear_night;
                        // Set the PictureBox image property to this image.
                        // ... Then, adjust its height and width properties.
                        weatherimg.Image = image;
                        weatherimg.Height = image.Height;
                        weatherimg.Width = image.Width;
                    }
                }
                if (conditionlbl.Text == "Mostly Clear")
                {
                    System.DateTime timenow = System.DateTime.Now;
                    if (timenow.Hour >= 5 && timenow.Hour < 12)
                    {
                        Image images = Properties.Resources.hot;
                        // Set the PictureBox image property to this image.
                        // ... Then, adjust its height and width properties.
                        weatherimg.Image = images;
                        weatherimg.Height = images.Height;
                        weatherimg.Width = images.Width;
                    }
                    if (timenow.Hour >= 12 && timenow.Hour < 18)
                    {
                        Image images = Properties.Resources.hot;
                        // Set the PictureBox image property to this image.
                        // ... Then, adjust its height and width properties.
                        weatherimg.Image = images;
                        weatherimg.Height = images.Height;
                        weatherimg.Width = images.Width;
                    }
                    if (timenow.Hour >= 18 && timenow.Hour < 24)
                    {
                        Image image = Properties.Resources.clear_night;
                        // Set the PictureBox image property to this image.
                        // ... Then, adjust its height and width properties.
                        weatherimg.Image = image;
                        weatherimg.Height = image.Height;
                        weatherimg.Width = image.Width;
                    }
                    if (timenow.Hour < 5)
                    {
                        Image image = Properties.Resources.clear_night;
                        // Set the PictureBox image property to this image.
                        // ... Then, adjust its height and width properties.
                        weatherimg.Image = image;
                        weatherimg.Height = image.Height;
                        weatherimg.Width = image.Width;
                    }
                }
                if (conditionlbl.Text == "Sunny")
                {
                    Image image = Properties.Resources.sunny;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Fair Night")
                {
                    Image image = Properties.Resources.fair_night;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Fair")
                {
                    Image image = Properties.Resources.fair_day;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Mixed Rain And Hail")
                {
                    Image image = Properties.Resources.mixed_rain_and_hail;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Hot")
                {
                    Image image = Properties.Resources.hot;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Isolated Thunderstorms")
                {
                    Image image = Properties.Resources.isolated_thundershowers;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Scattered Thunderstorms")
                {
                    Image image = Properties.Resources.scattered_snow_showers;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }

                if (conditionlbl.Text == "Scattered Showers")
                {
                    Image image = Properties.Resources.scattered_snow_showers;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Heavy Snow")
                {
                    Image image = Properties.Resources.heavy_snow;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Scattered Snow Showers")
                {
                    Image image = Properties.Resources.scattered_snow_showers;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Partly Cloudy")
                {
                    Image image = Properties.Resources.partly_cloudy;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Thundershowers")
                {
                    Image image = Properties.Resources.thunderstorms;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Snow Showers")
                {
                    Image image = Properties.Resources.snow_showers;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
                if (conditionlbl.Text == "Isolated Thundershowers")
                {
                    Image image = Properties.Resources.isolated_thundershowers;
                    // Set the PictureBox image property to this image.
                    // ... Then, adjust its height and width properties.
                    weatherimg.Image = image;
                    weatherimg.Height = image.Height;
                    weatherimg.Width = image.Width;
                }
            }
            if (labelnet.Text == "False")
            {


            }
            ////////////////////////
            //tomorrowforcast.Text = "weather forcast for tomorrow :" + GetWeather("description");
            HtmlToText convert = new HtmlToText();
            tomorrowforcast.Text = convert.Convert(GetWeather("description"));
            string textreplace = tomorrowforcast.Text;
            string text = String.Format(tomorrowforcast.Text);
            text = text.Replace("Sat", "Saturday");
            //computer.SpeakAsync("now weather forcast for whole week, " + text);
            ////////////////////////////////////////////
        }


        private void label3_Click(object sender, EventArgs e)
        {


        }

        private void timetimer_Tick(object sender, EventArgs e)
        {
            screentimelbl.Text = System.DateTime.Now.ToString("hh:mm:ss");
            ampmlbl.Text = System.DateTime.Now.ToString("tt");
        }

        private void speakbtn_Click(object sender, EventArgs e)
        {


        }
    }
}
#endregion