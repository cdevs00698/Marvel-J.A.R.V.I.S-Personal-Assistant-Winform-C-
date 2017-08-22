using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Speech.Synthesis;

namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    class Emails
    {
        public static void CheckForEmails()
        {
            SpeechSynthesizer Marvel = new SpeechSynthesizer();
            string GmailAtomUrl = "https://mail.google.com/mail/feed/atom";

            XmlUrlResolver xmlResolver = new XmlUrlResolver();
            xmlResolver.Credentials = new NetworkCredential(Properties.Settings.Default.GmailUser, Properties.Settings.Default.GmailPassword);
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
                        Marvel.SpeakAsync("You have 1 new email");
                    }
                    else { Marvel.SpeakAsync("You have " + emailItems.Count() + " new emails"); }
                }
                else if (MailReader.QEvent == "Checkfornewemails" && emailItems.Count() == 0)
                { Marvel.SpeakAsync("You have no new emails"); MailReader.QEvent = String.Empty; }
            }
            catch { Marvel.SpeakAsync("You have submitted invalid log in information"); }
        }
    }
}
