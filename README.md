# Venier.Email
Sending email with console application and Azure queue
# Usage
All variable for connection are secret. 
If you want to test this program you must create a new file: Program.key.cs
```c#
    partial class Program
    {
        static public string StorageConnectionString = "***";
        static public string SmtpServer = "smtp.gmail.com";
        static public int SmtpPortNumber = 465;
        static public string AuthEmail = "***@tecnicosuperiorekennedy.it";
        static public string AuthPassword = "***";
    }
```
