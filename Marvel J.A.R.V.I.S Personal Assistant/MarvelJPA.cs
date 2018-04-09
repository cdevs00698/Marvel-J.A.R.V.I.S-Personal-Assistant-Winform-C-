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
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    public partial class MarvelJPA : Form
    {
        int Top;
        int MoveX;
        int MoveY;
        SpeechRecognitionEngine speechRecognitionEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer Marvel = new SpeechSynthesizer();
        Grammar shellcommandgrammar; //Grammar variables allow us to load and unload words into Jarvis's vocabulary and update them during runtime without the need to restart the program
        Grammar webcommandgrammar, socialcommandgrammar, MusicGrammar, VideoGrammar;
        /// <summary>
        /// list of predefined commands
        /// </summary>
        string QEvent;
        string ProcWindow;
        double timer = 10;
        int count = 1;
        int i = 0;
        bool jarvis = false;
        Random rnd = new Random();
        StreamWriter sw;
        Form slfrm;
        System.Windows.Forms.ComboBox sl;
        /// <summary>
        /// ////////////////Shell Commands /////////////
        String[] ArrayShellCommands; //These arrays will be loaded with custom commands, responses, and File Locations
        String[] ArrayShellResponse;
        String[] ArrayShellLocation;
        String[] ArraySocialCommands;
        String[] ArraySocialResponse;
        public static string shellcpath; //These strings will be used to refer to the Shell Command text document
        public static string shellrespath; //These strings will be used to refer to the Shell Response text document
        public static string shellocpath; //These strings will be used to refer to the Shell Location text documen
        public static string socmdpath; //These strings will be used to refer to the Social Command text document
        public static string sorespath;
        public static string webcpath;
        public static string webrespath;
        public static string websearchpath; //These strings will be used to refer to the Web Command text document
        public static string websearchkeypath; //These strings will be used to refer to the Web Response text document
        public static string weatherchpath; //These strings will be used to refer to the Web Command text document
        public static string weathercitypath; //These strings will be used to refer to the Web Response text document
        public static string youtubecpath; //These strings will be used to refer to the Web Command text document
        public static string youtubeurlpath; //These strings will be used to refer to the Web Response text document
        public static String userName = Environment.UserName;
        /// </summary>
        ///////////////////master volume ///////////////////////
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;
        private const int APPCOMMAND_MEDIA_PLAY_PAUSE = 0xE0000;
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        /////////////////////end here //////////////////////////
        private static WebReader _webreader = null;
        private static Commandlist _cl = null;
        private static MailReader _mr = null;
        private static MediaPlayer _mp = null;
        private static Name _name = null;
        private static Reminder _reminder = null;
        private static Settings _settings = null;
        private static TextReading _textreader = null;
        private static Todaynews _todaynews = null;
        private static WeatherReport _weatherreport = null;
        private static Websearch _websearch = null;
        private static YoutubePlayer _yt = null;
        public MarvelJPA()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Language == String.Empty)
            {
                AskForACountry();
                Marvel.SpeakAsyncCancelAll();
            }
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Properties.Settings.Default.Language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
                speechRecognitionEngine = new SpeechRecognitionEngine(new CultureInfo(Properties.Settings.Default.Language));
                speechRecognitionEngine = new SpeechRecognitionEngine(new CultureInfo(Properties.Settings.Default.Language));
            }
            catch (Exception ex) { ErrorLog(ex.ToString()); AskForACountry(); }
            lblLanguage.Text = Properties.Settings.Default.Language;
            if (Properties.Settings.Default.User.ToString() == String.Empty) //Checks for user info. If none is available it sets to default
            { Properties.Settings.Default.User = userName; Properties.Settings.Default.Save(); Marvel.SpeakAsync("It is nice to make your acquaintance " + Properties.Settings.Default.User + ", my name is JARVIS and I will be your personal digital assistant"); }
            else
            { Marvel.Speak("Hello " + Properties.Settings.Default.User + ", currently loading the necessary files"); }
            //string[] defaultcommands = (File.ReadAllLines(@"Default Commands.txt"));
            try
            {
                // create the engine
                //speechRecognitionEngine = createSpeechEngine("en-US");

                // hook to events
                //speechRecognitionEngine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(engine_AudioLevelUpdated);

                Choices chVolume = new Choices();
                for (int inNum = 0; inNum <= 100; inNum++)
                {
                    chVolume.Add(Convert.ToString("Set the volume at " + inNum + " percent"));
                }
                speechRecognitionEngine.LoadGrammarAsync(new Grammar(new GrammarBuilder(chVolume)));
                speechRecognitionEngine.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(Environment.CurrentDirectory + "\\Mainform.txt"))))); //Load our Default Commands text document so we have commands to start with
                speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Shell_SpeechRecognized); //These are event handlers that are responsible for carrying out all necessary tasks if a speech event is recognized
                speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Social_SpeechRecognized);
                //speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Web_SpeechRecognized);
                //speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Default_SpeechRecognized);
                speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);
                speechRecognitionEngine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(engine_AudioLevelUpdated);
                // load dictionary
                //loadGrammarAndCommands();
                //UnloadGrammarAndCommands();
                // use the system's default microphone
                speechRecognitionEngine.SetInputToDefaultAudioDevice();

                // start listening
                speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                Marvel.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(jarvis_SpeakCompleted);

                Directory.CreateDirectory(@"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands"); //We create 'Jarvis Custom Commands' folder in the My Documents folder so we have a place to store our text documents
                Properties.Settings.Default.ShellC = @"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Shell Commands.txt";
                Properties.Settings.Default.ShellR = @"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Shell Response.txt";
                Properties.Settings.Default.ShellL = @"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Shell Location.txt";
                Properties.Settings.Default.SocC = @"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Social Commands.txt";
                Properties.Settings.Default.SocR = @"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Social Response.txt";
                Properties.Settings.Default.WebS = @"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Web Search Commands.txt";
                Properties.Settings.Default.WebSK = @"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Web Search Keywords.txt";
                Properties.Settings.Default.WebC = @"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Web Commands.txt";
                Properties.Settings.Default.WebR = @"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Web Response.txt";
                Properties.Settings.Default.WCMD = @"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Weather Commands.txt";
                Properties.Settings.Default.WCN = @"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Weather City.txt";
                Properties.Settings.Default.YTC = @"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Youtube Commands.txt";
                Properties.Settings.Default.YTA = @"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\Youtube Address.txt";
                Properties.Settings.Default.Save();


                shellcpath = Properties.Settings.Default.ShellC; //The text document file locations are passed on to these variables because they are easier to refer to but admittedly is an unnecessary step
                shellrespath = Properties.Settings.Default.ShellR;
                shellocpath = Properties.Settings.Default.ShellL;
                socmdpath = Properties.Settings.Default.SocC;
                sorespath = Properties.Settings.Default.SocR;
                webcpath = Properties.Settings.Default.WebC;
                webrespath = Properties.Settings.Default.WebR;
                websearchpath = Properties.Settings.Default.WebS;
                websearchkeypath = Properties.Settings.Default.WebSK;
                weatherchpath = Properties.Settings.Default.WCMD;
                weathercitypath = Properties.Settings.Default.WCN;
                youtubecpath = Properties.Settings.Default.YTC;
                youtubeurlpath = Properties.Settings.Default.YTA;
                ////////////////////////// Write to the path //////////////////////////////////
                if (!File.Exists(shellcpath))
                {
                    sw = File.CreateText(shellcpath); sw.Write("My Documents"); sw.Close();
                }
                if (!File.Exists(shellrespath))
                {
                    sw = File.CreateText(shellrespath); sw.Write("Right away"); sw.Close();
                }
                if (!File.Exists(shellocpath))
                {
                    sw = File.CreateText(shellocpath); sw.Write(@"C:\Users\" + userName + "\\Documents"); sw.Close();
                }
                if (!File.Exists(socmdpath))
                {
                    sw = File.CreateText(socmdpath); sw.Write("How are you"); sw.Close();
                }
                if (!File.Exists(sorespath))
                {
                    sw = File.CreateText(sorespath); sw.Write("I'm doing well thanks for asking"); sw.Close();
                }
                if (!File.Exists(webcpath))
                {
                    sw = File.CreateText(webcpath); sw.Write("tell me about microsoft"); sw.Close();
                }
                if (!File.Exists(webrespath))
                {
                    sw = File.CreateText(webrespath); sw.Write("microsoft"); sw.Close();
                }
                if (!File.Exists(websearchpath))
                {
                    sw = File.CreateText(websearchpath); sw.Write("search bing"); sw.Close();
                }
                if (!File.Exists(websearchkeypath))
                {
                    sw = File.CreateText(websearchkeypath); sw.Write("bing"); sw.Close();
                }
                if (!File.Exists(weatherchpath))
                {
                    sw = File.CreateText(weatherchpath); sw.Write("Get weather report of athens greece"); sw.Close();
                }
                if (!File.Exists(weathercitypath))
                {
                    sw = File.CreateText(weathercitypath); sw.Write("locations gose here"); sw.Close();
                }
                if (!File.Exists(youtubecpath))
                {
                    sw = File.CreateText(youtubecpath); sw.Write("Play song no promises"); sw.Close();
                }
                if (!File.Exists(youtubeurlpath))
                {
                    sw = File.CreateText(youtubeurlpath); sw.Write("http://www.youtube.com/watch?v=HLphrgQFHUQ"); sw.Close();
                }

                ArrayShellCommands = File.ReadAllLines(shellcpath); //This loads all written commands in our Custom Commands text documents into arrays so they can be loaded into our grammars
                ArrayShellResponse = File.ReadAllLines(shellrespath);
                ArrayShellLocation = File.ReadAllLines(shellocpath);
                ArraySocialCommands = File.ReadAllLines(socmdpath); //This loads all written commands in our Custom Commands text documents into arrays so they can be loaded into our grammars
                ArraySocialResponse = File.ReadAllLines(sorespath);
                //The following try catch blocks load our custom commands into our grammars. The catch block is in case of any blank lines or other unforseeable errors
                // jarvis main sound //////////

                //////ends here ///////////////
                if (Marvel.State == SynthesizerState.Speaking)
                    Marvel.SpeakAsyncCancelAll();
                foreach (InstalledVoice voice in Marvel.GetInstalledVoices())
                {
                    cbVoice.Items.Add(voice.VoiceInfo.Name.ToString());
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Voice recognition failed");
            }
        }
        protected void btnSetLang_Click(object sender, EventArgs e)
        {
            LoadSpeechRecognizerLanguage();
        }
        void AskForACountry()
        {
            slfrm = new Form();
            slfrm.Text = "               S e l e c t   L a n g u a g e";
            slfrm.ShowIcon = false;
            sl = new System.Windows.Forms.ComboBox();
            slfrm.Size = new Size(380, 180);
            slfrm.BackColor = System.Drawing.Color.Black;
            sl.Size = new Size(200, 60);
            //slfrm.ForeColor = System.Drawing.Color.Green;
            System.Windows.Forms.Button btnSetLang = new System.Windows.Forms.Button();
            btnSetLang.Size = new Size(88, 23);
            btnSetLang.Text = "Set Language";
            btnSetLang.ForeColor = System.Drawing.Color.Gray;
            btnSetLang.Location = new Point(136, 90);
            sl.Location = new Point(82, 40);
            sl.DropDownStyle = ComboBoxStyle.DropDownList;
            String[] arrayCountry = { "Catalan - Spain", "Chinese - China", "Chinese - Hong Kong", "Chinese - Taiwan", "Danish - Denmark", "Dutch - Netherlands", "English - Australia", "English - Canada", "English - Great Britain", "English - US", "Finnish - Finland", "French - Canada", "French - France", "German - Germany", "Italian - Italy", "Japanese - Japan", "Korean - Korea", "Norwegian - Norway", "Polish - Poland", "Portuguese - Brazil", "Portuguese - Portugal", "Russian - Russia", "Spanish - Mexico", "Spanish - Spain", "Swedish - Sweden" };
            sl.DataSource = arrayCountry.ToList();
            btnSetLang.Click += new EventHandler(btnSetLang_Click);
            slfrm.Controls.Add(sl);
            slfrm.Controls.Add(btnSetLang);
            Marvel.SpeakAsync("Please select a country that fits your computer's default language and dialect.");
            System.Windows.Forms.MessageBox.Show("Please select a country that fits your computer's default language and dialect.", "Select A Country");
            slfrm.StartPosition = FormStartPosition.CenterScreen;
            slfrm.AcceptButton = btnSetLang;
            Marvel.SpeakAsyncCancelAll();
            slfrm.ShowDialog();
        }
        private void lblLanguage_Click(object sender, EventArgs e)
        {
            AskForACountry();
        }
        void LoadSpeechRecognizerLanguage()
        {
            try
            {
                Marvel.SpeakAsyncCancelAll();
                String[] arrayCountry = { "Catalan - Spain", "Chinese - China", "Chinese - Hong Kong", "Chinese - Taiwan", "Danish - Denmark", "Dutch - Netherlands", "English - Australia", "English - Canada", "English - Great Britain", "English - United States", "Finnish - Finland", "French - Canada", "French - France", "German - Germany", "Italian - Italy", "Japanese - Japan", "Korean - Korea", "Norwegian - Norway", "Polish - Poland", "Portuguese - Brazil", "Portuguese - Portugal", "Russian - Russia", "Spanish - Mexico", "Spanish - Spain", "Swedish - Sweden" };
                String[] arrayLang = { "ca-ES", "zh-CN", "zh-HK", "zh-TW", "da-DK", "nl-NL", "en-AU", "en-CA", "en-GB", "en-US", "fi-FI", "fr-CA", "fr-FR", "de-DE", "it-IT", "ja-JP", "ko-KR", "nb-NO", "pl-PL", "pt-BR", "pt-PT", "ru-RU", "es-MX", "es-ES", "sv-SE" };
                Properties.Settings.Default.Language = arrayLang[sl.SelectedIndex];
                Properties.Settings.Default.Save();
                lblLanguage.Text = Properties.Settings.Default.Language;
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Properties.Settings.Default.Language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
                // = new SpeechRecognitionEngine(new CultureInfo(Settings.Default.Language));
                //startlistening = new SpeechRecognitionEngine(new CultureInfo(Settings.Default.Language));
                Marvel.SpeakAsync("You have selected " + arrayCountry[sl.SelectedIndex] + " as your default language. It is recommended that you restart the program any time you change the Speech Recognition Language.");
                System.Windows.Forms.MessageBox.Show("You have selected " + Properties.Settings.Default.Language + " as your default language. It is recommended that you restart the program any time you change the Speech Recognition Language.", "Thank You");
                lblLanguage.Text = Properties.Settings.Default.Language;
                Marvel.SpeakAsyncCancelAll();
                slfrm.Dispose();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.ToString());
                Marvel.SpeakAsyncCancelAll();
                Properties.Settings.Default.Language = string.Empty;
                Marvel.SpeakAsync("It seems the language you have selected does not match your system's language.");
                System.Windows.Forms.MessageBox.Show("It seems the language you have selected does not match your system's language.", "Invalid Selection");
                slfrm.Dispose();
                slfrm.Visible = false;
                AskForACountry();
            }
        }
        private void jarvis_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            jarvis = false;
        }

        private void engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            int ranNum = rnd.Next(1, 10);
            string checkinternet = NetworkInterface.GetIsNetworkAvailable().ToString();
            checkinternet.Replace("True", "Connected");
            checkinternet.Replace("False", "Disconnected");
            string speech = (e.Result.Text);
            if(e.Result.Text == "jarvis"){
              jarvis = true;
            }
            switch (speech)
            {
                //GREETINGS
                case "hey marvel":
                case "hey jarvis":
                    loadGrammarAndCommands();
                    break;
                case "open text reader":
                    Marvel.SpeakAsync("My Pleasure Loadding");
                    TextReading readg = new TextReading();
                    readg.Show();
                    readg.TopMost = true;
                    break;
                //GREETINGS
                case "hello":
                case "how are you":
                case "good morning":
                case "good afternoon":
                case "good evening":
                    String line;
                    try
                    {
                        //Pass the file path and file name to the StreamReader constructor
                        StreamReader sr = new StreamReader(@"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\name.txt");
                        //Read the first line of text
                        line = sr.ReadLine();
                        if (ranNum < 6)
                        {
                            Marvel.SpeakAsync("Hello sir, " + line);
                        }
                        else if (ranNum > 5)
                        {
                            Marvel.SpeakAsync("Hey, " + line);
                        }
                        //Continue to read until you reach end of file
                        while (line != null)
                        {
                            //write the lie to console window
                            Console.WriteLine(line);
                            //Read the next line
                            line = sr.ReadLine();

                        }
                        //close the file
                        sr.Close();
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
                    HeyJarvis();
                    break;
                case "goodbye":
                    Marvel.SpeakAsync("Until next time, ");
                    Thread.Sleep(1800);
                    Close();
                    break;
                case "change voice to microsoft devid":
                    Marvel.SelectVoice("Microsoft David Desktop");
                    Marvel.SpeakAsync("voice is changed, ");
                    HeyJarvis();
                    break;
                case "change voice to microsoft zira":
                    Marvel.SelectVoice("Microsoft Zira Desktop");
                    Marvel.SpeakAsync("voice is changed, ");
                    HeyJarvis();
                    break;
                case "change voice to brian":
                    if (cbVoice.Text != "IVONA 2 Brian OEM")
                    {
                        Marvel.SelectVoice("IVONA 2 Brian OEM");
                        Marvel.Speak("Here is Ivona, Brian at, your service master");
                    }
                    else
                    {
                        cbVoice.SelectedItem = "Microsoft David Desktop";
                        Marvel.SelectVoice("Microsoft David Desktop");
                        Marvel.Speak("Here is Microsoft David Desktop at, your service master ivona brian, voice is not installed");
                    }
                    HeyJarvis();
                    break;
                case "change voice to salli":
                    if (cbVoice.Text != "IVONA 2 Salli OEM")
                    {
                        cbVoice.SelectedItem = "IVONA 2 Salli OEM";
                        Marvel.SelectVoice("IVONA 2 Salli OEM");
                        Marvel.Speak("Here is Ivona, IVONA, Salli at, your service master");
                    }
                    else
                    {
                        cbVoice.SelectedItem = "Microsoft Zira Desktop";
                        Marvel.SelectVoice("Microsoft Zira Desktop");
                        Marvel.Speak("Here is Microsoft Zira Desktop at, your service master IVONA 2 Salli, voice is not installed in your computer");
                    }
                    HeyJarvis();
                    break;
                case "change voice to amy":
                    if (cbVoice.Text != "IVONA 2 Amy OEM")
                    {
                        cbVoice.SelectedItem = "IVONA 2 Amy OEM";
                        Marvel.SelectVoice("IVONA 2 Amy OEM");
                        Marvel.Speak("Here is IVONA 2 Amy at, your service master");
                    }
                    else
                    {
                        cbVoice.SelectedItem = "Microsoft Zira Desktop";
                        Marvel.SelectVoice("Microsoft Zira Desktop");
                        Marvel.Speak("Here is Microsoft Zira Desktop at, your service master IVONA 2 Salli, voice is not installed in your computer");
                    }
                    HeyJarvis();
                    break;
                case "activate yourself jarvis":
                    Marvel.Volume = 100;
                    speechRecognitionEngine.UnloadAllGrammars();
                    loadGrammarAndCommands();
                    Marvel.SpeakAsync("i am here online, and ready");
                    break;
                case "deactivate your self jarvis":
                    Marvel.SpeakAsync("I am here but, in silent mode");
                    speechRecognitionEngine.UnloadAllGrammars();
                    UnloadGrammarAndCommands();
                    Marvel.Volume = 0;

                    break;
                //CLOSE PROGRAMS
                case "close program":
                    Process[] AllProcesses = Process.GetProcesses();
                    foreach (var process in AllProcesses)
                    {
                        if (process.MainWindowTitle != "")
                        {
                            string s = process.ProcessName.ToLower();
                            if (s == "iexplore" || s == "iexplorer" || s == "msascui" || s == "chrome" || s == "firefox" || s == "skype" || s == "taskmgr" || s == "control panel" || s == "GoogleMap")
                                process.Kill();
                        }
                    }
                    Marvel.SpeakAsync("Program is closed");
                    HeyJarvis();
                    break;
                //CONDITION OF DAY
                case "what time is it":
                    System.DateTime now = System.DateTime.Now;
                    string time = now.GetDateTimeFormats('t')[0];
                    Marvel.SpeakAsync(time);
                    HeyJarvis();
                    break;
                case "what day is it":
                    string dayis;
                    dayis = "Today is," + System.DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("en-US"));
                    Marvel.SpeakAsync(dayis);
                    HeyJarvis();
                    break;
                case "what is the date":
                case "what is todays date":
                    string date;
                    date = "the date is, " + System.DateTime.Now.ToString("dd MMM", new System.Globalization.CultureInfo("en-US"));
                    Marvel.SpeakAsync(date);
                    date = "" + System.DateTime.Today.ToString(" yyyy");
                    Marvel.SpeakAsync(date);
                    HeyJarvis();
                    break;
                case "stay a side":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Minimized;
                    TopMost = false;
                    Marvel.SpeakAsync("my pleasure , master");
                    HeyJarvis();
                    break;
                case "come back":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Normal;
                    TopMost = true;
                    Marvel.SpeakAsync("on your, way");
                    HeyJarvis();
                    break;
                case "show voice commands panel":
                case "what should i ask":
                    Marvel.SpeakAsync("Very well loading... Succeed ");
                    HeyJarvis();
                    Commandlist comnd = new Commandlist();
                    comnd.Show();
                    comnd.TopMost = true;
                    break;
                case "shutdown your system":
                    Marvel.SpeakAsync("My pleasure, bye from now,  see you soon master");
                    Application.Exit();
                    this.Close();
                    break;
                case "who are you":
                    Marvel.SpeakAsync("i am your personal assistant");
                    Marvel.SpeakAsync("i can read email, weather report, i can search web for you, i can fix and tell you about your appointments, anything that you need like a personal assistant do, you can ask me question i will reply to you");
                    HeyJarvis();
                    break;
                case "who is your creator":
                    Marvel.SpeakAsync("Mr, saleem raza he created me with c sharp programming language, now he is working on my face and, speaker recognition project");
                    HeyJarvis();
                    break;
                case "add my name":
                case "note my name":
                    Marvel.Speak("ok master, ready, say your name");
                    HeyJarvis();
                    Name domain = new Name();
                    domain.Show();
                    domain.TopMost = true;
                    break;
                case "what is my name":
                    String lines;
                    try
                    {
                        //Pass the file path and file name to the StreamReader constructor
                        StreamReader sr = new StreamReader(@"C:\Users\" + userName + "\\Documents\\Jarvis Custom Commands\\name.txt");
                        //Read the first line of text
                        lines = sr.ReadLine();
                        Marvel.SpeakAsync("Your name is, " + lines);
                        //close the file
                        sr.Close();
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
                    HeyJarvis();
                    break;
                case "open media player":
                    Marvel.SpeakAsync("my pleasure, here is media player");
                    HeyJarvis();
                    MediaPlayer mp = new MediaPlayer();
                    mp.Show();
                    mp.TopMost = true;
                    break;
                case "open weather reader":
                case "today weather":
                case "tell me about the weather":
                case "what is the weather today":
                    Marvel.SpeakAsync("My pleasure loading weather report");
                    HeyJarvis();
                    WeatherReport wreport = new WeatherReport();
                    wreport.Show();
                    wreport.TopMost = true;
                    break;
                case "check internet status":
                    Marvel.SpeakAsync("Your are now, " + checkinternet + " to internet");
                    HeyJarvis();
                    break;
                case "back to desktop":
                    SendKeys.Send("{Esc}");
                    Marvel.SpeakAsync("Here is desktop");
                    HeyJarvis();
                    break;
                case "show reminder mode":
                case "fix alarm":
                case "fix reminder":
                case "i want to add reminder":
                    HeyJarvis();
                    Reminder ar = new Reminder();
                    ar.Show();
                    ar.TopMost = true;
                    break;
                case "do i have any apointment for today":
                case "any apointments for today":
                case "any apointment for today":
                case "tell me about my apointments":
                case "tell me about my apointment":
                    string msglines;
                    string timesetat;
                    try
                    {
                        StreamReader sr2 = new StreamReader(Environment.CurrentDirectory + "\\remindertxt.txt");
                        msglines = sr2.ReadLine();
                        Marvel.SpeakAsync("Your apointment is, " + msglines);
                        //close the file
                        sr2.Close();
                        Console.ReadLine();
                        StreamReader sr1 = new StreamReader(Environment.CurrentDirectory + "\\remindertimeset.txt");
                        timesetat = sr1.ReadLine();
                        Marvel.SpeakAsync("Apointment time fixed at, " + timesetat);
                        //close the file
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
                    HeyJarvis();
                    break;
                case "master volume mute":
                    SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_MUTE);
                    HeyJarvis();
                    break;
                case "master volume up":
                    SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_UP);
                    APPCOMMAND_VOLUME_UP.ToString();
                    HeyJarvis();
                    break;
                case "master volume down":
                    SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_DOWN);
                    HeyJarvis();
                    break;
                case "open email inbox":
                case "check email inbox":
                case "check email":
                case "check my email":
                    MailReader mailreader = new MailReader();
                    mailreader.Show();
                    HeyJarvis();
                    break;
                case "what is your name":
                    if (cbVoice.SelectedItem == null)
                    {
                        cbVoice.SelectedItem = "Microsoft David Desktop";
                        Marvel.SpeakAsync("My Name is, Microsoft David Desktop");
                    }

                    if (cbVoice.Text == "Microsoft David Desktop")
                    {
                        Marvel.SpeakAsync("My Name is, Microsoft David Desktop");
                    }
                    if (cbVoice.Text == "IVONA 2 Brian OEM")
                    {
                        Marvel.SpeakAsync("My Name is, IVONA 2 Brian");
                    }
                    if (cbVoice.Text == "IVONA 2 Salli OEM")
                    {
                        Marvel.SpeakAsync("My Name is, IVONA 2 Salli");
                    }
                    if (cbVoice.Text == "IVONA 2 Amy OEM")
                    {
                        Marvel.SpeakAsync("My Name is, IVONA 2 Amy");
                    }
                    if (cbVoice.Text == "Microsoft Hazel Desktop")
                    {
                        Marvel.SpeakAsync("My Name is, Microsoft Hazel Desktop");
                    }
                    if (cbVoice.Text == "Microsoft Zira Desktop")
                    {
                        Marvel.SpeakAsync("My Name is, Microsoft Zira Desktop");
                    }
                    HeyJarvis();
                    break;
                case "open website reader":
                case "search website":
                case "search website for me":
                    HeyJarvis();
                    Websearch ws = new Websearch();
                    ws.Show();
                    break;
                case "open facebook":
                    Marvel.SpeakAsync("Ok, master, loading");
                    System.Diagnostics.Process.Start("https://www.facebook.com");
                    HeyJarvis();
                    break;
                case "show website reader":
                    Marvel.SpeakAsync("Ok, master, loading");
                    HeyJarvis();
                    WebReader webr = new WebReader();
                    webr.Show();
                    webr.TopMost = true;
                    break;
                case "please compile some reports for me":
                    WebReader webreader = new WebReader();
                    webreader.Show();
                    webreader.TopMost = true;
                    HeyJarvis();
                    break;
                case "get today news report":
                case "get today news":
                case "today news":
                    Marvel.Speak("sure master,");
                    HeyJarvis();
                    Todaynews tdn = new Todaynews();
                    tdn.ShowDialog();
                    tdn.TopMost = true;
                    break;
                case "activate to male version":
                    if (cbVoice.Text != "IVONA 2 Brian OEM")
                    {
                        cbVoice.SelectedItem = "IVONA 2 Brian OEM";
                        Marvel.SelectVoice("IVONA 2 Brian OEM");
                        Marvel.SpeakAsync("ok, if this is your wish, than your wish, is my command, here is IVONA, Brian ");
                    }
                    else
                    {
                        cbVoice.SelectedItem = "Microsoft David Desktop";
                        Marvel.SelectVoice("Microsoft David Desktop");
                        Marvel.SpeakAsync("ok, if this is your wish, than your wish, is my command, here is microsoft, david desktop");
                    }
                    HeyJarvis();
                    break;
                case "activate to female version":
                    if (cbVoice.Text != "IVONA 2 Salli OEM")
                    {
                        cbVoice.SelectedItem = "IVONA 2 Salli OEM";
                        Marvel.SelectVoice("IVONA 2 Salli OEM");
                        Marvel.SpeakAsync("ok, here is IVONA, Salli what else you are, expecting, right now master");
                    }
                    else
                    {
                        cbVoice.SelectedItem = "Microsoft Zira Desktop";
                        Marvel.SelectVoice("Microsoft Zira Desktop");
                        Marvel.SpeakAsync("ok, here is microsoft, zira desktop, what else you are, expecting, right now");
                    }
                    HeyJarvis();
                    break;

                case "who is your favorite digital personal assistant":
                case "do you know siri":
                case "do you know cortana":
                    Marvel.SpeakAsync("There are several digital assistant exist, but from my point of view, cortana and siri is the best");
                    HeyJarvis();
                    break;
                case "i wanna talk to you":
                case "i want talk to you":
                case "start conversation":
                case "can we talk":
                case "start conversation mode":
                    try
                    { socialcommandgrammar = new Grammar(new GrammarBuilder(new Choices(ArraySocialCommands))); speechRecognitionEngine.LoadGrammar(socialcommandgrammar); }
                    catch
                    { Marvel.SpeakAsync("I've detected an in valid entry in your social commands, possibly a blank line. Social commands will cease to work until it is fixed."); }
                    speechRecognitionEngine.UnloadGrammar(socialcommandgrammar);
                    ArraySocialCommands = File.ReadAllLines(socmdpath); //This loads all written commands in our Custom Commands text documents into arrays so they can be loaded into our grammars
                    ArraySocialResponse = File.ReadAllLines(sorespath);
                    break;
                case "back to control":
                case "end the conversation":
                case "activate control mood":
                case "stop conversation mode":
                    Marvel.SpeakAsync("all, right sir");
                    HeyJarvis();
                    break;
                case "Update commands":
                    update();
                    HeyJarvis();
                    break;
                case "show settings":
                    Settings settings = new Settings();
                    settings.Show();
                    settings.TopMost = true;
                    HeyJarvis();
                    break;
                case "open alarm clock":
                    Reminder rm = new Reminder();
                    rm.Show();
                    rm.TopMost = true;
                    HeyJarvis();
                    break;
                case "stop":
                case "stop talking":
                    if (Marvel.State == SynthesizerState.Paused)
                        Marvel.Resume();
                    Marvel.SpeakAsyncCancelAll();
                    HeyJarvis();
                    break;
                case "open youtube":
                    YoutubePlayer yt = new YoutubePlayer();
                    yt.Show();
                    yt.TopMost = true;
                    HeyJarvis();
                    break;
            }
        }
        private void update()
        {
            Marvel.SpeakAsync("This may take a few seconds");
            speechRecognitionEngine.UnloadGrammar(socialcommandgrammar);
            ArraySocialCommands = File.ReadAllLines(socmdpath); //This loads all written commands in our Custom Commands text documents into arrays so they can be loaded into our grammars
            ArraySocialResponse = File.ReadAllLines(sorespath);
            try
            { socialcommandgrammar = new Grammar(new GrammarBuilder(new Choices(ArraySocialCommands))); speechRecognitionEngine.LoadGrammar(socialcommandgrammar); }
            catch
            { Marvel.SpeakAsync("I've detected an in valid entry in your social commands, possibly a blank line. Social commands will cease to work until it is fixed."); }
            speechRecognitionEngine.UnloadGrammar(shellcommandgrammar);
            ArrayShellCommands = File.ReadAllLines(shellcpath); //This loads all written commands in our Custom Commands text documents into arrays so they can be loaded into our grammars
            ArrayShellResponse = File.ReadAllLines(shellrespath);
            ArrayShellLocation = File.ReadAllLines(shellocpath);
            try
            { shellcommandgrammar = new Grammar(new GrammarBuilder(new Choices(ArrayShellCommands))); speechRecognitionEngine.LoadGrammar(shellcommandgrammar); }
            catch
            { Marvel.SpeakAsync("I've detected an in valid entry in your shell commands, possibly a blank line. shell commands will cease to work until it is fixed."); }
            Marvel.SpeakAsync("All commands updated");
        }

        private void Social_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            i = 0;
            try
            {
                foreach (string line in ArraySocialCommands)
                {
                    if (line == speech)
                    {
                        Marvel.SpeakAsync(ArraySocialResponse[i]);
                    }
                    i += 1;
                }
            }
            catch
            {
                i += 1;
                Marvel.SpeakAsync("Please check the " + speech + " social command on line " + i + ". It appears to be missing a proper response");
            }
        }

        private void Shell_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text; //Sets the SpeechRecognized event variable to a string variable called speech
            i = 0; //Ensures "i" is = to 0 so we can start our loop from the beginning of our arrays
            try
            {
                foreach (string line in ArrayShellCommands)
                {
                    if (line == speech) //If line == speech it will open the corresponding program/file
                    {
                        Marvel.SpeakAsync(ArrayShellResponse[i]); //Gives the response of the same elemental position as the ArrayShellCommands command that was equal to speech
                        System.Diagnostics.Process.Start(ArrayShellLocation[i]); //Opens the program/file of the same elemental position as the ArrayShellCommands command that was equal to speech
                    }
                    i += 1; //if the line in ArrayShellCommands does not equal speech it will add 1 to "i" and go through the loop until it finds a match between the line and spoken event
                }
            }
            catch
            {
                i += 1;
                Marvel.SpeakAsync("Im sorry it appears the shell command " + speech + " on line " + i + " is accompanied by either a blank line or an incorrect file location");
            }
        }
        private void HeyJarvis()
        {
            speechRecognitionEngine.UnloadAllGrammars();
            UnloadGrammarAndCommands();

        }
        private void loadGrammarAndCommands()
        {
            try
            {
                Choices texts = new Choices();
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\Mainform.txt");
                texts.Add(lines);
                Grammar wordsList = new Grammar(new GrammarBuilder(texts));
                speechRecognitionEngine.LoadGrammar(wordsList);
                try
                { shellcommandgrammar = new Grammar(new GrammarBuilder(new Choices(ArrayShellCommands))); speechRecognitionEngine.LoadGrammarAsync(shellcommandgrammar); }
                catch
                { Marvel.SpeakAsync("I've detected an in valid entry in your shell commands, possibly a blank line. Shell commands will cease to work until it is fixed."); }
                try
                { socialcommandgrammar = new Grammar(new GrammarBuilder(new Choices(ArraySocialCommands))); speechRecognitionEngine.LoadGrammarAsync(socialcommandgrammar); }
                catch
                { Marvel.SpeakAsync("I've detected an in valid entry in your social commands, possibly a blank line. Social commands will cease to work until it is fixed."); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UnloadGrammarAndCommands()
        {
            try
            {
                Choices textz = new Choices();
                string[] linez = File.ReadAllLines(Environment.CurrentDirectory + "\\RestartCommands.txt");
                textz.Add(linez);
                Grammar wordsListz = new Grammar(new GrammarBuilder(textz));
                speechRecognitionEngine.LoadGrammar(wordsListz);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void instanceHasBeenClosed(object sender, FormClosedEventArgs e)
        {
            _webreader = null;
            _cl = null;
            _mr = null;
            _mp = null;
            _name = null;
            _reminder = null;
            _settings = null;
            _textreader = null;
            _todaynews = null;
            _weatherreport = null;
            _websearch = null;
            _yt = null;
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Top = 1;
            MoveX = e.X;
            MoveY = e.Y;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Top = 0;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Top == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MoveX, MousePosition.Y - MoveY);
            }
        }

        private void cbVoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            JarvisMenu jm = new JarvisMenu();
            jm.Show();
            MJLeftMenu mjlm = new MJLeftMenu();
            mjlm.Show();
            MJRightMenu mjrm = new MJRightMenu();
            mjrm.Show();
            DateTimeForm dtf = new DateTimeForm();
            dtf.Show();
            //////////////////////////////Time AM and Pm Checker //////////////////////////////

            string welcome = System.Environment.UserName;
            System.DateTime timenow = System.DateTime.Now;
            if (timenow.Hour >= 5 && timenow.Hour < 12)
            { Marvel.SpeakAsync("Goodmorning " + welcome); }
            if (timenow.Hour >= 12 && timenow.Hour < 18)
            { Marvel.SpeakAsync("Good afternoon " + welcome); }
            if (timenow.Hour >= 18 && timenow.Hour < 24)
            { Marvel.SpeakAsync("Good evening " + welcome); }
            if (timenow.Hour < 5)
            { Marvel.SpeakAsync("Hello " + welcome + ", you are still awake you should go to sleep, it's getting late"); }
            ///////////////////// End here ///////////////////////////////////////////////////
            /////////////////////Pick Month label ////////////////////////////////////////////

        }
        private void engine_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {
            verticleProgressBar1.Value = e.AudioLevel;
            //verticleProgressBar2.Value = e.AudioLevel;
            verticleProgressBar3.Value = e.AudioLevel;
            //verticleProgressBar4.Value = e.AudioLevel;
            //verticleProgressBar5.Value = e.AudioLevel;
            verticleProgressBar6.Value = e.AudioLevel;
            //verticleProgressBar7.Value = e.AudioLevel;
            //verticleProgressBar8.Value = e.AudioLevel;
            //verticleProgressBar9.Value = e.AudioLevel;
            //verticleProgressBar10.Value = e.AudioLevel;
            //verticleProgressBar11.Value = e.AudioLevel;
            //verticleProgressBar12.Value = e.AudioLevel;
            //verticleProgressBar13.Value = e.AudioLevel;
            //verticleProgressBar14.Value = e.AudioLevel;
            //verticleProgressBar15.Value = e.AudioLevel;
            //verticleProgressBar16.Value = e.AudioLevel;
        }
        public void ErrorLog(string error)
        {
            MessageBox.Show(error);
            string DTL = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(DTL + "\\Error Report.txt"))
            {
                sw.WriteLine("~~~" + System.DateTime.Now + "~~~");
                sw.WriteLine(error.ToString());
                sw.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~");
                sw.WriteLine("");
                sw.Close();

            }
        }
        public void ErrorReport(string error)
        {
            string DTL = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(DTL + "\\Error Report.txt"))
            {
                sw.WriteLine("~~~" + System.DateTime.Now + "~~~");
                sw.WriteLine(error.ToString());
                sw.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~");
                sw.WriteLine("");
                sw.Close();
            }
        }

    }
}
