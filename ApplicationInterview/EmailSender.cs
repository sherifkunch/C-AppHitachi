using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace ApplicationInterview
{
    public class EmailSender
    {
        public string FromEmail { get; set; }
        public SecureString Password { get; set; }
        public string ToEmail { get; set; }
        public string Attachment { get; set; }

        public EmailSender(string fEmail, SecureString psw, string tEmail, string attachment)
        {
            FromEmail = fEmail;
            Password = psw;
            ToEmail = tEmail;
            Attachment = attachment;
        }
        public static SecureString maskInputString()
        {

            SecureString pass = new SecureString();

            //register what keys are being pressed 
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);
                if (!char.IsControl(keyInfo.KeyChar)) // checks if the current pressed key is not a control char
                {
                    pass.AppendChar(keyInfo.KeyChar);
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass.RemoveAt(pass.Length - 1);
                    Console.Write("\b \b");
                }
            } while (keyInfo.Key != ConsoleKey.Enter); // loop until we press the enter console key
            {
                return pass;
            }
        }
        public static bool IsValidEmail(string emailToCheck)
        {
            try
            {
                MailAddress mail = new MailAddress(emailToCheck);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void SendEmail(string fromEmail, SecureString password, string toEmail, string attachment)
        {
            try
            {
                SmtpClient mailServer = new SmtpClient("smtp.gmail.com", 587);
                mailServer.EnableSsl = true;

                mailServer.Credentials = new System.Net.NetworkCredential(fromEmail, password);

                MailMessage msg = new MailMessage(fromEmail, toEmail);
                msg.Subject = "C# Application Attachment";
                msg.Body = "This is attachment with data processed in the C# App.";
                msg.Attachments.Add(new Attachment(attachment));
                Console.WriteLine("Sending email...");
                mailServer.Send(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to send email. Error : " + ex);
            }

        }
    }
}
