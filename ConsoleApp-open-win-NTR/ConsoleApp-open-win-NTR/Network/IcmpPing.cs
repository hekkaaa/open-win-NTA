using System;
using System.Net.NetworkInformation;

namespace ConsoleApp_open_win_NTR.Network
{
    public class IcmpPing
    {
        public PingReply IcmpRequest(string hosname)
        {
            return IcmpRequestMethod(hosname);
        }

        public PingReply IcmpRequest(string hosname, int timeout)
        {
            return IcmpRequestMethod(hosname, timeout);

        }

        internal void IcmpRequest()
        {
            throw new NotImplementedException();
        }


        private PingReply IcmpRequestMethod(string hosname, int timeout)
        {
            Ping ping = new Ping();

            try
            {
                PingReply res = ping.Send(hosname, timeout);
                return res;
            }
            catch (PingException)
            {   // тут нужно доработать обработку exception
                throw new PingException("host unreachable");
            }
            catch (Exception ex)
            {
                throw new Exception("New Error" + ex.Message);
            }
        }

        private PingReply IcmpRequestMethod(string hosname)
        {
            Ping ping = new Ping();
            try
            {
                PingReply res = ping.Send(hosname, 1000);
                return res;
            }
            catch (PingException)
            {   // тут нужно доработать обработку exception
                throw new PingException("host unreachable");
            }
            catch (Exception ex)
            {
                throw new Exception("New Error" + ex.Message);
            }
        }
    }
}
