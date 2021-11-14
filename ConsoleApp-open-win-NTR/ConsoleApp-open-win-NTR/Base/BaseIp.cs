using System.Collections.Generic;

namespace ConsoleApp_open_win_NTR.Base
{
    public class BaseIp
    {
        public string Ip { get; set; }
        public int Port { get; set; }

        public int minPing { get; set; }
        public int middlePing { get; set; }
        public int maxPing { get; set; }

        public string HostName { get; set; }

        List<uint> _pingDelay = new List<uint>();

        public int counterPacket { get; set; }

        public int counterLossPacket { get; set; }


        public List<uint> PingDelay
        {
            get { return this._pingDelay; }
            set { this._pingDelay = value; }
        }
    }
}
