using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_open_win_NTR.Base
{
    internal class BaseIp
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        
        public string HostName { get; set; }

        List<string> _pingDelay = new List<string>();

        public int counterPacket { get; set; }

        public int counterLossPacket { get; set; }


        public List<string> PingDelay
        {
            get { return this._pingDelay; }
            set { this._pingDelay = value; }
        }
    }
}
