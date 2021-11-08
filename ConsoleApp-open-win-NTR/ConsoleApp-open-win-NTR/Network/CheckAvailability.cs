using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
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
                    if (result.Status.ToString().ToLower() == "Success".ToLower()) { count++; successfully++; } // Сделать тут лог
                    else { count++; fail++; }   // Сделать тут лог
                }
                catch (PingException ex)
                {
                    Console.WriteLine(ex.Message + "Остуствует подключение к интернету, либо адрес не существует");
                    this.message = ex.Message + "Остуствует подключение к интернету, либо адрес не существует";
                    break;
                }
                Thread.Sleep(1000);
            }

            if (successfully > 1)
            {
                return true;
            }
            else
            {
                this.message = "Остуствует подключение к интернету, либо адрес не существует";
                return false;

            }
        }

        public string Info
        {
            get { return this.message; }
        }
    }
}
