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
        {   // "192.168.0.22" // "ya.ru" // "nocodeurl.com"
            const string host = "ya.ru";


            void Test_Main_Circle(string host)
            {
                // Start
                //int all_packages = 0;
                //int succes_packages = 0;
                //int fail_packages = 0;

                Console.WriteLine("Please stand by");

                List<IPAddress> tableTracert = (List<IPAddress>)TraceRoute.GetTraceRoute(host);
                List<string> reTabletracert = tableTracert.ConvertAll(s => s.ToString()); // Делаем свой list что-бы знать длинну + меням тип.


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
                    foreach (string ip in reTabletracert)
                    {
                        IcmpPing ping = new IcmpPing();
                        PingReply result = ping.IcmpRequest(ip);

                        if (result.Status == IPStatus.Success) multiDynamicRoundtripTimeList[count].Add($"{result.RoundtripTime}");
                        else if (result.Status == IPStatus.DestinationHostUnreachable) throw new Exception($"Host {result.Address}: DestinationHostUnreachable");
                        else if (result.Status == IPStatus.TtlExpired) throw new Exception($"Host {result.Address}: TtlExpired");
                        else if (result.Status == IPStatus.TimedOut) /*throw new Exception($"Host {result.Address}: TimedOut")*/; // <= Основная ошибка когда хост перестает отвечать вдруг. Тут считать потери.
                        else Console.WriteLine($"FAIL: {result.Status}");

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

            Base.BaseLoop t1 = new Base.BaseLoop(host);

        }
    }
}
