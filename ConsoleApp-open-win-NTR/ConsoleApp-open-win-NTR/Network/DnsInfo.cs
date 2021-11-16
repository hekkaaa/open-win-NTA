using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace ConsoleApp_open_win_NTR.Network
{
    public static class DnsInfo
    {
        public static string DnsHostname(string host)
        {
            // Info Exception Class https://docs.microsoft.com/ru-ru/dotnet/api/system.net.dns.gethostentry?view=net-5.0
            try { return Dns.GetHostEntry(host).HostName; }
            catch (ArgumentNullException ex)
            {
                Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | Host {host}: {ex.Message}");
                throw new ArgumentNullException();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | Host {host}: {ex.Message}");
                throw new ArgumentOutOfRangeException();
            }
            catch (SocketException ex)
            {
                Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | Host {host}: {ex.Message}");
                return "\t";
            }
            catch (ArgumentException ex)
            {
                Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | Host {host}: {ex.Message}");
                throw new ArgumentException();
            }
        }
        public static string[] DnsAliases(string host)
        {
            try { return Dns.GetHostEntry(host).Aliases; }
            catch (ArgumentNullException ex)
            {
                Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | Host {host}: {ex.Message}");
                throw new ArgumentException();
            }
            catch (Exception ex)
            {
                Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | Host {host}: {ex.Message}");
                throw new Exception($"Error {ex.Message}");
            }
        }
        public static IPAddress[] DnsAddressList(string host)
        {
            try { return Dns.GetHostEntry(host).AddressList; }
            catch (SocketException ex)
            {
                Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | Host {host}: {ex.Message} + {ex.Source}");
                throw new SocketException();
            }
            catch (ArgumentNullException ex)
            {
                Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | Host {host}: {ex.Message} + {ex.Source}");
                throw new ArgumentNullException();
            }
            catch (Exception ex)
            {
                Logs.LogAppend.Log($"{Assembly.GetExecutingAssembly().Location} | Host {host}: {ex.Message} + {ex.Source}");
                throw new Exception();
            }
        }
    }
}
