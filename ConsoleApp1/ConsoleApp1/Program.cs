using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Declare variables and then initialize to zero.
            int num1 = 0; int num2 = 0;

            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            // Ask the user to type the first number.
            Console.WriteLine("Type a number, and then press Enter");
            num1 = Convert.ToInt32(Console.ReadLine());

            // Ask the user to type the second number.
            Console.WriteLine("Type another number, and then press Enter");
            num2 = Convert.ToInt32(Console.ReadLine());

            // Ask the user to choose an option.
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            // Use a switch statement to do the math.
            switch (Console.ReadLine())
            {
                case "a":
                    Console.WriteLine($"Your result: {num1} + {num2} = " + (num1 + num2));
                    break;
                case "s":
                    Console.WriteLine($"Your result: {num1} - {num2} = " + (num1 - num2));
                    break;
                case "m":
                    Console.WriteLine($"Your result: {num1} * {num2} = " + (num1 * num2));
                    break;
                case "d":
                    Console.WriteLine($"Your result: {num1} / {num2} = " + (num1 / num2));
                    break;
            }

            ProcessStartInfo processInfo;
            Process process;


            //D: & cd D:/SEGGER/Jlink & JlinkRTTClient

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + "D: & cd D:/SEGGER/Jlink & JlinkRTTClient");
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            // Warning: This approach can lead to deadlocks, see Edit #2
            string output = process.StandardOutput.ReadToEnd();
            Console.Write (output);

            //Thread thread = new Thread(() =>
            //{
            //    // This is where you would put your webserver. In this example, the call to Thread.Sleep stands in for your webserver.
            //    Console.WriteLine("Working...");
            //    Thread.Sleep(5000);
            //});
            //thread.Start();

            //while (thread.IsAlive)
            //{
            //    Console.WriteLine("Still working...");
            //    Thread.Sleep(1000); // Let the main thread sleep for a bit before checking again, so it doesn't unnecessarily hog resources.
            //}

            //Console.WriteLine("Done!");

            Console.Write("Press any key to close the Calculator console app...");
            Console.ReadKey();
        }
    }
}
