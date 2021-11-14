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
            const string host = "192.168.0.102";
            Base.BaseLoop t1 = new Base.BaseLoop(host);
        }
    }
}
