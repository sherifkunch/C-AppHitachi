using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ApplicationInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter name of file:");
            string fileName = Console.ReadLine();
            Console.WriteLine("Please enter sender:");
            string sender = Console.ReadLine();
            Console.WriteLine("Please enter password: ");
            var password = EmailSender.maskInputString();
            Console.WriteLine();
            Console.WriteLine("Please enter receiver:");
            string receiver = Console.ReadLine();

            var inputFileName = @"C:\Users\sheri\source\repos\ApplicationInterview\ApplicationInterview\appdata\" + fileName;

            if (!File.Exists(inputFileName))
            {
                WriteLine($"ERROR: file \"{fileName}\" does not exist.");
                Console.ReadLine();
                return;
            }

            if (!EmailSender.IsValidEmail(sender))
            {
                Console.WriteLine("Invalid Email adress");
                Console.ReadLine();
                return;
            }

            if (!EmailSender.IsValidEmail(receiver))
            {
                Console.WriteLine("Invalid Email adress");
                Console.ReadLine();
                return;
            }

            var outputFileName = @"C:\Users\sheri\source\repos\ApplicationInterview\ApplicationInterview\appdata\ReportByCountry.csv";

            CsvFileProcessor csvFileProcessor = new CsvFileProcessor(inputFileName, outputFileName);

            csvFileProcessor.Process();

            EmailSender.SendEmail(sender,password,receiver,outputFileName);
           
        }

        
    }
}
