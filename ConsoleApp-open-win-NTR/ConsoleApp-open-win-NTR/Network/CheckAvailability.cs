using System;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading;

namespace ConsoleApp_open_win_NTR.Network
{
    public class CheckAvailability
    {
        string host;
        string message = "OK";

        public CheckAvailability(string host)
        {
            this.host = host;
        }

        public bool CheckStatus()
        {
            // Подсчет сделан для отладочной информации в лог.
            // Counting is done for debug information in the log.
            int fail = 0;
            int successfully = 0;
            int count = 0;

            while (count <= 4)
            {
                IcmpPing ping = new IcmpPing();
                try
                {
                    PingReply result = ping.IcmpRequest(this.host, 3000);
                    if (result.Status.ToString().ToLower() == "Success".ToLower()) 
                    { 
                        count++;
                        successfully++;
                        Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | {host}: Success");
                    }
                    else { count++; fail++; Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | {host}: Failed"); }   // Сделать тут лог
                }
                catch (PingException ex)
                {
                    Console.WriteLine(ex.Message + "Отсутствует  подключение к интернету, либо адрес не существует");
                    this.message = ex.Message + "Отсутствует  подключение к интернету, либо адрес не существует";
                    Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | {host}: {this.message}");
                    break;
                }
                Thread.Sleep(1000);
            }

            if (successfully > 1)
            {
                Logs.LogAppend.Log($"{ Assembly.GetExecutingAssembly().Location} | { host}: Host available");
                return true;
            }
            else
            {
                this.message = "Отсутствует  подключение к интернету, либо адрес не существует";
                Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | {host}: {this.message}");
                return false;

            }
        }

        public string Info
        {
            get { return this.message; }
        }
    }
}
