using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Newtonsoft.Json;
using System;
using Venier.Data;

namespace Venier.Email
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Message message = new Message { };

            Console.WriteLine("Enter email address:");
            message.email = Console.ReadLine();

            Console.WriteLine("Enter object:");
            message.obj = Console.ReadLine();

            Console.WriteLine("Enter text:");
            message.text = Console.ReadLine();

            //Serialize message
            var JsonMessage = JsonConvert.SerializeObject(message);


            // Connection to Azure
            var storageAccount = CloudStorageAccount.Parse(StorageConnectionString);
            var queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("emailqueue");
            queue.CreateIfNotExists();

            //Insert message in queue
            CloudQueueMessage email = new CloudQueueMessage(JsonMessage);
            queue.AddMessage(email);
            Console.WriteLine("Email: Ok!");


            //Console.WriteLine("\n\nEmail: "message.email + "\nObject: " + message.obj + "\nMessage:\n" + message.text);
            Console.ReadLine();
        }
    }
}
