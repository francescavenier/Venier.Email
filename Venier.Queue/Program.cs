using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Threading;
using Venier.Data;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Venier.Queue
{
    partial class Program
    {
        static void Main(string[] args)
        {
            int wait = 2000;
            Message message = new Message { };

            // Connection to Azure
            var storageAccount = CloudStorageAccount.Parse(StorageConnectionString);
            var queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("emailqueue");
            queue.CreateIfNotExists();

            while (true)
            { 
                var queueMessage = queue.GetMessage();
                if(queueMessage!=null)
                { 
                    Console.WriteLine(queueMessage.AsString);
                    // Deserialize message
                    message = JsonConvert.DeserializeObject<Message>(queueMessage.AsString);

                    // Send email
                    try
                    {
                        //From Address    
                        string FromAddress = AuthEmail;
                        string FromAdressTitle = "Francesca Venier";
                        //To Address    
                        string ToAddress = message.email;
                        string ToAdressTitle = "Microsoft ASP.NET Core";
                        string Subject = message.obj;
                        string BodyContent = message.text;

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

                            // Delete message from queue
                            queue.DeleteMessage(queueMessage);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                else 
                {
                    Console.WriteLine("No message.");
                    wait = wait + 1000;
                }
                Thread.Sleep(wait);
            }
        }
    }
}
