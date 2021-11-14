using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_open_win_NTR.Base
{
    internal class BaseLoop
    {
        List<string> _ipList;

        public BaseLoop()
        {
            throw new NotImplementedException();
        }

        public BaseLoop(string host)
        {
            Console.WriteLine("Please stand by");

            Network.CheckAvailability startLoop = new Network.CheckAvailability(host);
            if (startLoop.CheckStatus())
            {
                var list = CreateBaseIpList(host);
                FillingPropertyBaseIp(list);
                LoopStatistics(list);

            }
            else
            {
                Console.WriteLine(startLoop.Info);
            } 
            
        }

        // Конверт формата для корректной работы.
        private List<string> ConvertTableFormat(string host)
        {
            List<IPAddress> tableTracert = (List<IPAddress>)TraceRoute.GetTraceRoute(host);
            List<string> reTabletracert = tableTracert.ConvertAll(s => s.ToString()); // Делаем свой list что-бы знать длинну + меням тип.
            return reTabletracert;   
        }

        // Создаем N экземпляров класса от числа хопов.
        private List<Base.BaseIp> CreateBaseIpList(string host)
        {
            List<string> reTabletracert = ConvertTableFormat(host);
            _ipList = reTabletracert;


            // Создание экземпляров класса для каждого хопа.
            List<Base.BaseIp> list = new List<Base.BaseIp>();
            for (int i = 0; i < reTabletracert.Count; i++)
            {
                list.Add(new Base.BaseIp());
            }
            return list;
        }

        // Заполням все экзепмляры класса через свойство Ip  
        private void FillingPropertyBaseIp(List<Base.BaseIp> list)
        {
            for(int i = 0; i < _ipList.Count; i++)
            {
                list[i].Ip = _ipList[i];
            }
        }


        private void LoopStatistics (List<Base.BaseIp> list)
        {
            while (true)
            {

          

                foreach (Base.BaseIp ip in list)
                {
                    IcmpPing ping = new IcmpPing();
                    PingReply result = ping.IcmpRequest(ip.Ip);

                    if (result.Status == IPStatus.Success)
                    {
                        ip.PingDelay.Add($"{result.RoundtripTime}");
                        ip.counterPacket++;
                    }
                    else if (result.Status == IPStatus.DestinationHostUnreachable)
                    {
                        ip.counterPacket++;
                        ip.counterLossPacket++;
                        /* throw new Exception($"Host {result.Address}: DestinationHostUnreachable") ;*/

                    }
                    else if (result.Status == IPStatus.TtlExpired) throw new Exception($"Host {result.Address}: TtlExpired");
                    else if (result.Status == IPStatus.TimedOut)
                    { /*throw new Exception($"Host {result.Address}: TimedOut")*/
                        // <= Основная ошибка когда хост перестает отвечать вдруг. Тут считать потери.
                        ip.counterPacket++;
                        ip.counterLossPacket++;

                    }
                    else Console.WriteLine($"FAIL: {result.Status}");

                    var algLosses = Algorithm.StatisticAlgorithm.RateLosses((double)ip.counterPacket, (double)ip.counterLossPacket);

                    //// Тестовый интерфейс

                    Console.Clear(); // Clear CMD
                    Console.WriteLine(result.Address);
                    Console.WriteLine($"Последняя Задержка: {result.RoundtripTime}");
                    Console.WriteLine($"Всего отправленных пакетов: {ip.counterPacket}");
                    Console.WriteLine($"Потерянных пакетов: {ip.counterLossPacket}");
                    Console.WriteLine($"процент потерь: {algLosses:f2}");
                    Console.WriteLine("++++++++++++++++++++++++");

                    // Перебор для вывода с задержками ping
                    Console.WriteLine("History RoundtripTime: ");
                    for (int j = 0; j < ip.PingDelay.Count; j++)
                    {
                        Console.Write($"{ip.PingDelay[j]}ms ");
                    }

                    Thread.Sleep(1000); // сделал задержу в 2сек специально пока что.
                }
            }
        }


        private void Statistic1( List<List<Base.BaseIp>> list, List<string> reTabletracert)
        {
            // Цикл для постоянного пинга по очереди. async будет потом следующий версиях.
            while (true)
            {   
                //int count = 0;
                //foreach (string ip in reTabletracert)
                //{
                //    IcmpPing ping = new IcmpPing();
                //    PingReply result = ping.IcmpRequest(ip);

                //    if (result.Status == IPStatus.Success) list[count].Add($"{result.RoundtripTime}");
                //    else if (result.Status == IPStatus.DestinationHostUnreachable) throw new Exception($"Host {result.Address}: DestinationHostUnreachable");
                //    else if (result.Status == IPStatus.TtlExpired) throw new Exception($"Host {result.Address}: TtlExpired");
                //    else if (result.Status == IPStatus.TimedOut) /*throw new Exception($"Host {result.Address}: TimedOut")*/; // <= Основная ошибка когда хост перестает отвечать вдруг. Тут считать потери.
                //    else Console.WriteLine($"FAIL: {result.Status}");

                //    Console.Clear(); // Clear CMD
                //    Console.WriteLine(result.Address);
                //    Console.WriteLine($"Задержка: {result.RoundtripTime}");
                //    Console.WriteLine("++++++++++++++++++++++++");

                //    // Намеренная изоляция в ReadOnly.
                //    var list_ReadOnly = list.AsReadOnly();

                //    // Перебор для вывода list с задержками ping
                //    Console.WriteLine("History RoundtripTime: ");
                //    for (int j = 0; j < list_ReadOnly[count].Count; j++)
                //    {
                //        Console.Write($"{list_ReadOnly[count][j]}ms ");
                //    }

                //    count++; // count
                //    Thread.Sleep(2000); // сделал задержу в 2сек специально пока что.

                //}
            }
        }

    }
}
