using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace ConsoleApp_open_win_NTR
{
    class IcmpPing
    {
        public PingReply IcmpRequest(string hosname)
        {
            Ping ping = new Ping();
            PingReply res = ping.Send(hosname, 1000);
            return res;
        }

    }
}
