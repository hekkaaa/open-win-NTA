using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace ConsoleApp_open_win_NTR
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start

            const string host = "vk.com";
            Console.WriteLine("Please stand by");

            var tabletracert = TraceRoute.GetTraceRoute(host);
            List<string> reTabletracert = new List<string>(); // Делаем свой list что-бы знать длину+ меням тип.

            foreach (IPAddress i in tabletracert)
            {
                string ip = Convert.ToString(i); // convert type Ipaddres(TraceRoute) for String.
                reTabletracert.Add(ip); // Add for list ip
                Console.WriteLine(ip);
            }

            // Create a list for RoundtripTime.
            // Создание списка для RoundtripTime по количеству хопов.
            var multiDynamicRoundtripTimeList = new List<List<string>> { };
            for (int i = 0; i < reTabletracert.Count; i++)
            {
                multiDynamicRoundtripTimeList.Add(new List<string> { });
            }

            // Цикл для постоянного пинга по очереди. async будет потом следующий версиях.
            while (true)
            {   int count = 0;
                foreach (string i in reTabletracert)
                {
                    IcmpPing ping = new IcmpPing();
                    PingReply result = ping.IcmpRequest(i);
                    multiDynamicRoundtripTimeList[count].Add($"{result.RoundtripTime}");

                    Console.Clear(); // Clear CMD
                    Console.WriteLine(result.Address);
                    Console.WriteLine($"Задержка: {result.RoundtripTime}");
                    Console.WriteLine("++++++++++++++++++++++++");

                    // Перебор для вывода list с задержками ping
                    Console.WriteLine("History RoundtripTime: ");
                    for(int j = 0; j<multiDynamicRoundtripTimeList[count].Count; j++)
                    {
                        Console.Write($"{multiDynamicRoundtripTimeList[count][j]}ms ");
                    }

                    count++; // count
                    Thread.Sleep(2000); // сделал задержу в 2сек специально пока что.

                }
            }

            // End
        }
    }
}
