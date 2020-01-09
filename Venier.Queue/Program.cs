using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Threading;
using Venier.Data;

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
                    EmailSender send = new EmailSender { };
                    send.SendEmail(message);

                    // Delete message from queue
                    queue.DeleteMessage(queueMessage);
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
