using System;
using Venier.Data;

namespace Venier.Email
{
    class Program
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

            //Console.WriteLine("\n\nEmail: "message.email + "\nObject: " + message.obj + "\nMessage:\n" + message.text);

            Console.ReadLine();
        }
    }
}
