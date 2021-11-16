using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading;

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
            List<IPAddress> tableTracert = (List<IPAddress>)Network.TraceRoute.GetTraceRoute(host);
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

        // Заполняет все экземпляры класса через свойство Ip  
        private void FillingPropertyBaseIp(List<Base.BaseIp> list)
        {
            for (int i = 0; i < _ipList.Count; i++)
            {
                list[i].Ip = _ipList[i];
                list[i].HostName = Network.DnsInfo.DnsHostname(_ipList[i]);
            }
        }

        private void LoopStatistics(List<Base.BaseIp> list)
        {
            while (true)
            {
                foreach (Base.BaseIp ip in list)
                {
                    Network.IcmpPing ping = new Network.IcmpPing();
                    PingReply result = ping.IcmpRequest(ip.Ip);

                    if (result.Status == IPStatus.Success)
                    {
                        uint tmpping = Convert.ToUInt32(result.RoundtripTime);
                        ip.PingDelay.Add(tmpping);
                        Algorithm.StatisticAlgorithm.UpdateMinMaxPing(ip, tmpping); // Обновляем min/max ping в экземпляре.
                        Algorithm.StatisticAlgorithm.MeanPing(ip);
                        ip.counterPacket++;
                    }
                    else if (result.Status == IPStatus.DestinationHostUnreachable)
                    {
                        Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | Host {result.Address} {result.Status}");
                        ip.counterPacket++;
                        ip.counterLossPacket++;

                    }
                    else if (result.Status == IPStatus.TtlExpired)
                    {
                        Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | Host {result.Address}: TtlExpired");
                        throw new Exception($"Host {result.Address}: TtlExpired");
                    }
                    else if (result.Status == IPStatus.TimedOut)
                    { /*throw new Exception($"Host {result.Address}: TimedOut")*/
                        // <= Основная ошибка когда хост перестает отвечать вдруг. Тут считать потери.
                        Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | Host {result.Address} {result.Status}");
                        ip.counterPacket++;
                        ip.counterLossPacket++;

                    }
                    else Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | Host {result.Address} {result.Status}");

                    // Вычисляем % потерь.
                    ip.percentLoss = Algorithm.StatisticAlgorithm.RateLosses(ip.counterPacket, ip.counterLossPacket);
                    Thread.Sleep(200); // сделал задержу в 0.2 сек специально пока что.
                }
                View.ViewConsole view = new View.ViewConsole(list);
               
            }
        }
    }
}
