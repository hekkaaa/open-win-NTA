using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;


namespace ConsoleApp_open_win_NTR
{
    class Program
    {   
        static void Main(string[] args)
        {   // "192.168.0.22" // "ya.ru" // "nocodeurl.com"
            //const string host = "ya.ru";
            do
            {   
                Console.Clear();
                Console.Write("Введите IP или hostname: ");
                string host = Console.ReadLine();
                Base.BaseLoop start = new Base.BaseLoop(host);
            }
            while (true);
        }
    }
}
