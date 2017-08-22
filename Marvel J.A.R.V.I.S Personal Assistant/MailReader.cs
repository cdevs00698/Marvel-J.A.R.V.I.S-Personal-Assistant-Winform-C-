using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Xml;
using Microsoft.VisualBasic;
using System.Collections;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    public partial class MailReader : Form
    {
        int Top;
        int MoveX;
        int MoveY;
        string[] paths;
        string message_subject;
        string message_author;
        string tagline;
        string message_summary;
        SpeechRecognitionEngine speechRecognitionEngine = null;
        SpeechSynthesizer Marvel = new SpeechSynthesizer();
        public static List<string> MsgList = new List<string>();
        public static List<string> MsgLink = new List<string>();
        public static String QEvent;

        int EmailNum = 0;

        public MailReader()
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

                foreach (InstalledVoice voice in Marvel.GetInstalledVoices())
                {
                    cbVoice.Items.Add(voice.VoiceInfo.Name);
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
                }
            }
            catch (Exception)
            {
                Marvel.Speak("");
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
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\gmailreader.txt");
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
                case "login to gmail":
                case "login":
                    logbtn.PerformClick();
                    break;
                case "send my email":
                    sendemailbtn.PerformClick();
                    break;
                case "get all emails":
                case "get all inbox emails":
                    getmailbtn.PerformClick();
                    break;
                case "check for new emails":
                    QEvent = "Checkfornewemails";
                    Marvel.SpeakAsyncCancelAll();
                    EmailNum = 0;
                    CheckForEmails();
                    break;
                case "read the email":
                    Marvel.SpeakAsyncCancelAll();
                    try
                    {
                        Marvel.SpeakAsync(MsgList[EmailNum]);
                    }
                    catch { Marvel.SpeakAsync("There are no emails to read"); }
                    break;
                case "next email":
                    Marvel.SpeakAsyncCancelAll();
                    try
                    {
                        EmailNum += 1;
                        Marvel.SpeakAsync(MsgList[EmailNum]);
                    }
                    catch { EmailNum -= 1; Marvel.SpeakAsync("There are no further emails"); }
                    break;
                case "previous email":
                    Marvel.SpeakAsyncCancelAll();
                    try
                    {
                        EmailNum -= 1;
                        Marvel.SpeakAsync(MsgList[EmailNum]);
                    }
                    catch { EmailNum += 1; Marvel.SpeakAsync("There are no previous emails"); }
                    break;
                case "close gmail reader":
                    closebtn.PerformClick();
                    break;
                case "minimize":
                case "hide gmail reader":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Minimized;
                    TopMost = false;
                    break;
                case "show gmail reader":
                case "show gmail reader again":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Normal;
                    TopMost = true;
                    break;
                case "stop reading":
                case "stop talking":
                case "stop":
                    if (Marvel.State == SynthesizerState.Speaking)
                        Marvel.SpeakAsyncCancelAll();
                    break;
                case "start reading":
                    if (Marvel.State == SynthesizerState.Paused)
                        Marvel.Resume();
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


        private void getmailbtn_Click(object sender, EventArgs e)
        {
            try
            {
                System.Net.WebClient objClient = new System.Net.WebClient();
                string response;
                string title;
                string summary;
                string tagline;
                //Creating a new xml document
                XmlDocument doc = new XmlDocument();

                //Logging in Gmail server to get data
                objClient.Credentials = new System.Net.NetworkCredential(usernametxt.Text, passwordtxt.Text);
                //reading data and converting to string
                response = Encoding.UTF8.GetString(
                           objClient.DownloadData(@"https://mail.google.com/mail/feed/atom"));

                response = response.Replace(
                     @"<feed version=""0.3"" xmlns=""http://purl.org/atom/ns#"">", @"<feed>");

                //loading into an XML so we can get information easily
                doc.LoadXml(response);
                string nr;
                //nr of emails
                nr = doc.SelectSingleNode(@"/feed/fullcount").InnerText;
                totalemailstxt.Text = nr;
                Marvel.SpeakAsync("Total numbers of emails are, " + nr + "email is exist in gmail inbox");
                tagline = doc.SelectSingleNode("/feed/tagline").InnerText;
                taglinestxt.Text = tagline;
                Marvel.SpeakAsync("sir, you have " + tagline);
                //Reading the title and the summary for every email
                foreach (XmlNode node in doc.SelectNodes(@"/feed/entry"))
                {
                    authernametxt.Text = node.SelectSingleNode("author").SelectSingleNode("name").InnerText;
                    Marvel.SpeakAsync("email from, " + authernametxt.Text.ToString());
                    title = node.SelectSingleNode("title").InnerText;
                    inbox.Items.Add(node.SelectSingleNode("title").InnerText);
                    Marvel.SpeakAsync("sir, mail is about, " + title.ToString());
                    summary = node.SelectSingleNode("summary").InnerText;
                    mailsummery.Text = summary.ToString();
                    Marvel.SpeakAsync("and the summary is, " + summary.ToString());
                }
            }
            catch (Exception)
            {
                Marvel.Speak("Please login to your gmail account and turn on less secure apps before this get work");
                MessageBox.Show("Please Login to your gmail account and turn on less secure apps before this get work");
                System.Diagnostics.Process.Start("https://support.google.com/accounts/answer/6010255?hl=en");
            }
        }

        private void logbtn_Click(object sender, EventArgs e)
        {
            CheckForEmails();
            try
            {
                System.Net.WebClient objClient = new System.Net.WebClient();
                string response;
                string title;
                string summary;
                string tagline;
                //Creating a new xml document
                XmlDocument doc = new XmlDocument();

                //Logging in Gmail server to get data
                objClient.Credentials = new System.Net.NetworkCredential(usernametxt.Text, passwordtxt.Text);
                //reading data and converting to string
                response = Encoding.UTF8.GetString(
                           objClient.DownloadData(@"https://mail.google.com/mail/feed/atom"));

                response = response.Replace(
                     @"<feed version=""0.3"" xmlns=""http://purl.org/atom/ns#"">", @"<feed>");

                //loading into an XML so we can get information easily
                doc.LoadXml(response);
                string nr;
                //nr of emails
                nr = doc.SelectSingleNode(@"/feed/fullcount").InnerText;
                totalemailstxt.Text = nr;
                Marvel.SpeakAsync("Total numbers of emails are, " + nr + "email is exist in gmail inbox");
                tagline = doc.SelectSingleNode("/feed/tagline").InnerText;
                taglinestxt.Text = tagline;
                Marvel.SpeakAsync("sir, you have " + tagline);
                //Reading the title and the summary for every email
                foreach (XmlNode node in doc.SelectNodes(@"/feed/entry"))
                {
                    authernametxt.Text = node.SelectSingleNode("author").SelectSingleNode("name").InnerText;
                    Marvel.SpeakAsync("email from, " + authernametxt.Text.ToString());
                    title = node.SelectSingleNode("title").InnerText;
                    inbox.Items.Add(node.SelectSingleNode("title").InnerText);
                    Marvel.SpeakAsync("sir, mail is about, " + title.ToString());
                    summary = node.SelectSingleNode("summary").InnerText;
                    mailsummery.Text = summary.ToString();
                    Marvel.SpeakAsync("and the summary is, " + summary.ToString());
                }
            }
            catch (Exception)
            {
                Marvel.Speak("Please login to your gmail account and turn on less secure apps before this get work");
                MessageBox.Show("Login to your gmail account and turn on less secure apps before this get work");
                System.Diagnostics.Process.Start("https://support.google.com/accounts/answer/6010255?hl=en");
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

        private void MailReader_Load(object sender, EventArgs e)
        {
            MailReader mailreader = new MailReader();
            mailreader.TopMost = true;
            labelnet.Text = NetworkInterface.GetIsNetworkAvailable().ToString();
            if (labelnet.Text != "True")
            {
                labelnet.Text = "Not Connected";
                Marvel.Speak("Please check your internet connection, and try again");
                Marvel.SpeakAsyncCancelAll();
                speechRecognitionEngine.Dispose();
                this.Close();
            }
            else
            {
                Marvel.Speak("Please wait");
                Marvel.Speak("if you are not log in please, log in to your gmail account");
                labelnet.Text = "Connected";

            }
        }

        private void browsebtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                attachmentlbl.Text = openFileDialog1.FileName.ToString();
            }
        }

        private void sendemailbtn_Click(object sender, EventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                MailMessage message = new MailMessage();
                message.From = new MailAddress(usernametxt.Text);
                message.To.Add(sendtotxt.Text);
                message.Body = messagetxt.Text;
                message.Subject = subjecttxt.Text;
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                if (attachmentlbl.Text != "")
                {
                    message.Attachments.Add(new Attachment(attachmentlbl.Text));
                }
                client.Credentials = new System.Net.NetworkCredential(usernametxt.Text, passwordtxt.Text);
                client.Send(message);
                message = null;
                Marvel.Speak("Message sent successfully");
            }
            catch (Exception)
            {
                Marvel.Speak("Failed to send message");
            }
        }
        public void CheckForEmails()
        {
            SpeechSynthesizer computer = new SpeechSynthesizer();
            string GmailAtomUrl = "https://mail.google.com/mail/feed/atom";

            XmlUrlResolver xmlResolver = new XmlUrlResolver();
            xmlResolver.Credentials = new NetworkCredential(usernametxt.Text, passwordtxt.Text);
            XmlTextReader xmlReader = new XmlTextReader(GmailAtomUrl);
            xmlReader.XmlResolver = xmlResolver;
            try
            {
                XNamespace ns = XNamespace.Get("http://purl.org/atom/ns#");
                XDocument xmlFeed = XDocument.Load(xmlReader);


                var emailItems = from item in xmlFeed.Descendants(ns + "entry")
                                 select new
                                 {
                                     Author = item.Element(ns + "author").Element(ns + "name").Value,
                                     Title = item.Element(ns + "title").Value,
                                     Link = item.Element(ns + "link").Attribute("href").Value,
                                     Summary = item.Element(ns + "summary").Value
                                 };
                MailReader.MsgList.Clear(); MailReader.MsgLink.Clear();
                foreach (var item in emailItems)
                {
                    if (item.Title == String.Empty)
                    {
                        MailReader.MsgList.Add("Message from " + item.Author + ", There is no subject and the summary reads, " + item.Summary);
                        MailReader.MsgLink.Add(item.Link);
                    }
                    else
                    {
                        MailReader.MsgList.Add("Message from " + item.Author + ", The subject is " + item.Title + " and the summary reads, " + item.Summary);
                        MailReader.MsgLink.Add(item.Link);
                    }
                }

                if (emailItems.Count() > 0)
                {
                    if (emailItems.Count() == 1)
                    {
                        computer.SpeakAsync("You have 1 new email");
                    }
                    else { computer.SpeakAsync("You have " + emailItems.Count() + " new emails"); }
                }
                else if (MailReader.QEvent == "Checkfornewemails" && emailItems.Count() == 0)
                { computer.SpeakAsync("You have no new emails"); MailReader.QEvent = String.Empty; }
            }
            catch
            {
                computer.SpeakAsync("You have submitted invalid log in information");
                computer.Speak("Please login to your gmail account and turn on less secure apps before this get work");
            }
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
