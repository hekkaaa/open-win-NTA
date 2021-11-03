using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace ConsoleApp_open_win_NTR
{
    class Program
    {
        static void Main(string[] args)
        {   // "192.168.0.22" // "ya.ru" // "nocodeurl.com"
            //Network.CheckAvailability test = new Network.CheckAvailability("192.168.0.22");
            //Console.WriteLine(test.CheckStatus());


            // Start

            const string host = "ya.ru";

            Console.WriteLine("Please stand by");

            List<IPAddress> tabletracert = (List<IPAddress>)TraceRoute.GetTraceRoute(host);
            List<string> reTabletracert = tabletracert.ConvertAll(s => s.ToString()); // Делаем свой list что-бы знать длинну +меням тип.


            // Create a list for RoundtripTime.
            // Создание списка для RoundtripTime по количеству хопов.
            List<List<string>> multiDynamicRoundtripTimeList = new List<List<string>> { };
            for (int i = 0; i < reTabletracert.Count; i++)
            {
                multiDynamicRoundtripTimeList.Add(new List<string> { });
            }

            // Намеренная изоляция в ReadOnly.
            var multiDynamicRoundtripTimeList_ReadOnly = multiDynamicRoundtripTimeList.AsReadOnly();

            // Цикл для постоянного пинга по очереди. async будет потом следующий версиях.
            while (true)
            {
                int count = 0;
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
                    for (int j = 0; j < multiDynamicRoundtripTimeList_ReadOnly[count].Count; j++)
                    {
                        Console.Write($"{multiDynamicRoundtripTimeList_ReadOnly[count][j]}ms ");
                    }

                    count++; // count
                    Thread.Sleep(2000); // сделал задержу в 2сек специально пока что.

                }
            }

            // End
        }
    }
}
