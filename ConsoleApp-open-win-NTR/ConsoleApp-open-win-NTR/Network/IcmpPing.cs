using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace ConsoleApp_open_win_NTR
{
    public class IcmpPing
    {
        public PingReply IcmpRequest(string hosname)
        {
            Ping ping = new Ping();
            PingReply res = ping.Send(hosname, 1000);
            return res;
        }

        public PingReply IcmpRequest(string hosname, int timeout)
        {
            Ping ping = new Ping();
            PingReply res = ping.Send(hosname, timeout);
            try { return res; }
            catch (PingException)
            {
                throw new PingException("host unreachable");
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        internal void IcmpRequest()
        {
            throw new NotImplementedException();
        }
    }
}
