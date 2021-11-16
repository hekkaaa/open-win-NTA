using ConsoleApp_open_win_NTR.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_open_win_NTR.View
{
    internal class ViewConsole
    {
        public ViewConsole(List<BaseIp> view)
        {
            Console.Clear(); // Очищаем предыдущую информацию в консоле.
            Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет
            Console.WriteLine("№\tIP\t\tAll\tFail\tLoss\tmaxPing\tmiddle\tminPing"); //Hostname\t
            Console.ResetColor(); // сбрасываем в стандартный
           
            for (int i = 0; i < view.Count; i++)
            {   
                if(view[i].percentLoss <= 2 && view[i].percentLoss > 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow; // устанавливаем цвет
                    Console.Write($"{i + 1}\t");
                    Console.Write($"{view[i].Ip}\t");
                    //Console.Write($"{view[i].HostName}\t");
                    Console.Write($"{view[i].counterPacket}\t");
                    Console.Write($"{view[i].counterLossPacket}\t");
                    Console.Write($"{view[i].percentLoss:f2}%\t");
                    Console.Write($"{view[i].maxPing}\t");
                    Console.Write($"{view[i].middlePing}\t");
                    Console.Write($"{view[i].minPing}\t");
                    Console.WriteLine();
                    Console.ResetColor(); // сбрасываем в стандартный
                }
                else if(view[i].percentLoss > 10)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed; // устанавливаем цвет
                    Console.Write($"{i + 1}\t");
                    Console.Write($"{view[i].Ip}\t");
                    //Console.Write($"{view[i].HostName}\t");
                    Console.Write($"{view[i].counterPacket}\t");
                    Console.Write($"{view[i].counterLossPacket}\t");
                    Console.Write($"{view[i].percentLoss:f2}%\t");
                    Console.Write($"{view[i].maxPing}\t");
                    Console.Write($"{view[i].middlePing}\t");
                    Console.Write($"{view[i].minPing}\t");
                    Console.WriteLine();
                    Console.ResetColor(); // сбрасываем в стандартный
                }
                else if(view[i].percentLoss > 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // устанавливаем цвет
                    Console.Write($"{i + 1}\t");
                    Console.Write($"{view[i].Ip}\t");
                    //Console.Write($"{view[i].HostName}\t");
                    Console.Write($"{view[i].counterPacket}\t");
                    Console.Write($"{view[i].counterLossPacket}\t");
                    Console.Write($"{view[i].percentLoss:f2}%\t");
                    Console.Write($"{view[i].maxPing}\t");
                    Console.Write($"{view[i].middlePing}\t");
                    Console.Write($"{view[i].minPing}\t");
                    Console.WriteLine();
                    Console.ResetColor(); // сбрасываем в стандартный
                }
                else
                {
                    Console.Write($"{i + 1}\t");
                    Console.Write($"{view[i].Ip}\t");
                    //Console.Write($"{view[i].HostName}\t");
                    Console.Write($"{view[i].counterPacket}\t");
                    Console.Write($"{view[i].counterLossPacket}\t");
                    Console.Write($"{view[i].percentLoss:f2}%\t");
                    Console.Write($"{view[i].maxPing}\t");
                    Console.Write($"{view[i].middlePing}\t");
                    Console.Write($"{view[i].minPing}\t");
                    Console.WriteLine();
                }
               
           
                
            }
             
        }

    }
}
