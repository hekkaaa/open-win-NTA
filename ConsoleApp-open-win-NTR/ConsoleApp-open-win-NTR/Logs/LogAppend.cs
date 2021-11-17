using System;
using System.IO;

namespace ConsoleApp_open_win_NTR.Logs
{
    internal class LogAppend
    {
        public static void Log(string logMessage)
        {
            //string path = @"..\..\..\..\ConsoleApp-open-win-NTR\Logfiles\Logs.txt"; // для отладки.
            string path = @"Logs.txt";
            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                    sw.WriteLine(logMessage);
                    sw.WriteLine("-------------------------------");
                }
            }
            // Appended text for Log
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine(logMessage);
                sw.WriteLine("-------------------------------");
            }
        }
       
    }
}
