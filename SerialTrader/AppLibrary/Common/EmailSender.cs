using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLibrary.Common;
using System.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.Net.Mime;
using System.Text.RegularExpressions;

using AppLibrary.Model;
using AppLibrary.Entity;

namespace AppLibrary.Common
{  
    public class EmailSender
    {
        #region "Private Members"
        private string mailServer = null;
        private string useName = null;
        private string password = null;
        private string sendingAddress = null;
        private string mailSubject = null;
        private int portNumber = 0;
        #endregion

        #region "Public Methods"
        public EmailSender()
        { }

        static EmailSender()
        { }
        
        private SmtpClient GetSMTPClient()
        {
            SmtpClient sClient = null;
            
            if (Boolean.Parse(ConfigurationSettings.AppSettings.Get("WaterCons.Email")))
            {
                string mailSettings = ConfigurationSettings.AppSettings.Get("SMTPServer") ?? "host=smtp.gmail.com; port=587;  userName=engageclouds@gmail.com; password=Rememberme1! ; subject=WaterCons Application";

                if (mailSettings != null)
                {
                    String[] serverConfig = mailSettings.Split(';');
                    String[] valueArray;
                    if (serverConfig[0] != null)
                    {
                        valueArray = serverConfig[0].ToString().Split('=');
                        if (valueArray != null)
                        {
                            mailServer = valueArray[1].ToString();
                        }
                    }
                    if (serverConfig[1] != null)
                    {
                        valueArray = serverConfig[1].ToString().Split('=');
                        if (valueArray != null)
                        {
                            portNumber = System.Convert.ToInt32(valueArray[1].ToString());
                        }
                    }
                    if (serverConfig[2] != null)
                    {
                        valueArray = serverConfig[2].ToString().Split('=');
                        if (valueArray != null)
                        {
                            if (!string.IsNullOrEmpty(valueArray[1].ToString()))
                            {
                                useName = valueArray[1].ToString();
                            }
                        }
                    }
                    if (serverConfig[3] != null)
                    {
                        valueArray = serverConfig[3].ToString().Split('=');
                        if (valueArray != null)
                        {
                            if (!string.IsNullOrEmpty(valueArray[1].ToString()))
                            {
                                password = valueArray[1].ToString();
                            }
                        }
                    }
                    if (serverConfig[4] != null)
                    {
                        valueArray = serverConfig[4].ToString().Split('=');
                        if (valueArray != null)
                        {
                            mailSubject = valueArray[1].ToString();
                        }
                    }
                }
            }

            if ((mailServer != string.Empty) && (sendingAddress != string.Empty))
            {
                sClient = new System.Net.Mail.SmtpClient(mailServer, portNumber);
                if ((useName != null) && (password != null))
                {
                    sClient.Credentials = new System.Net.NetworkCredential(useName, password);
                    sClient.EnableSsl = true;
                }
                else
                {
                    sClient.EnableSsl = false;
                }
            }
            return sClient;
        }

        public static void SendEmail(string To = "Support@watercons.com", string Subject = "No Subject"
            , string Message = "No Body", bool IsBodyHtml = true,
            Attachment attachment = null)
        {
            string From = ConfigurationSettings.AppSettings.Get("MailFrom") ?? "no_reply@watercons.com";

            MailMessage email = new MailMessage(From, To)
            {
                Subject = Subject,
                IsBodyHtml = IsBodyHtml,
                Body = Message
            };
            if (attachment != null)
            {
                email.Attachments.Add(attachment);
            }

            EmailSender eSender = new EmailSender();
            SmtpClient sClient = eSender.GetSMTPClient();
            if (sClient != null)
            {
                try
                {
                    sClient.Send(email);
                }
                catch (Exception ex)
                {
                }
            }
        }
        #endregion
    }
}
