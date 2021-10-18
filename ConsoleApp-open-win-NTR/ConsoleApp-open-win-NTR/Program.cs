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

            var multiDimensionalList = new List<List<string>>{
                    new List<string>{"A","B","C"},
                    new List<string>{"D","E","F"},
                    new List<string>{"G","H","I"},
                };
            //var roMult = multiDimensionalList.AsReadOnly();
            Console.WriteLine(multiDimensionalList.Count);
            multiDimensionalList.Add(new List<string> { "Aa", "Bs", "Cd" });
            Console.WriteLine(multiDimensionalList.Count);
            Console.WriteLine("+++");
            Console.WriteLine(multiDimensionalList[0][1]);
            Console.WriteLine(multiDimensionalList[1][1]);
            Console.WriteLine(multiDimensionalList[2][1]);
            Console.WriteLine(multiDimensionalList[3][1]);


            // Начало

            //const string host = "vk.com";
            //Console.WriteLine("Please stand by");

            //var tabletracert = TraceRoute.GetTraceRoute(host);
            //List<string> reTabletracert = new List<string>(); // Делаем свой list что-бы знать длину+ меням тип.

            //foreach (IPAddress i in tabletracert)
            //{
            //    string ip = Convert.ToString(i); // convert type Ipaddres(TraceRoute) for String.
            //    reTabletracert.Add(ip); // Add for list ip
            //    Console.WriteLine(ip);
            //}

            //while (true)
            //{
            //    foreach (string i in reTabletracert)
            //    {
            //        IcmpPing ping = new IcmpPing();
            //        PingReply result = ping.IcmpRequest(i);
            //        Console.Clear();
            //        Console.WriteLine(result.Address);
            //        Console.WriteLine($"Задержка: {result.RoundtripTime}");
            //        Console.WriteLine("++++++++++++++++++++++++");
            //        Thread.Sleep(2000);

            //    }
            //}

            // Конец

        }
    }
}
