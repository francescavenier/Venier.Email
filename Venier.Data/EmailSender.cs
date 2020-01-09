using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Venier.Data
{
    public partial class EmailSender
    {
        public void SendEmail(Message model)
        {
            try
            {
                //From Address    
                string FromAddress = AuthEmail;
                string FromAdressTitle = "Francesca Venier";
                //To Address    
                string ToAddress = model.email;
                string ToAdressTitle = "Microsoft ASP.NET Core";
                string Subject = model.obj;
                string BodyContent = model.text;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress
                                        (FromAdressTitle,
                                         FromAddress
                                         ));
                mimeMessage.To.Add(new MailboxAddress
                                         (ToAdressTitle,
                                         ToAddress
                                         ));
                mimeMessage.Subject = Subject; //Subject  
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = BodyContent
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber);
                    client.Authenticate(
                        AuthEmail,
                        AuthPassword
                        );
                    client.Send(mimeMessage);

                    Console.WriteLine("The mail has been sent successfully !!");
                    Console.ReadLine();
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
