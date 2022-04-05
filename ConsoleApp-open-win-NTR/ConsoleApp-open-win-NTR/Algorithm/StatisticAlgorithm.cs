namespace ConsoleApp_open_win_NTR.Algorithm
{
    public static class StatisticAlgorithm
    {


        public static double RateLosses(double a, double b)
        {
            double c = b * 100 / a;
            return c;
        }

        public static void UpdateMinMaxPing(Base.BaseIp ip, uint ping)
        {
            if (ip.minPing == 0) ip.minPing = (int)ping;
            if (ip.maxPing == 0) ip.maxPing = (int)ping;
            else
            {
                if (ip.minPing > ping)
                {
                    ip.minPing = (int)ping;
                }
                if (ip.maxPing < ping)
                {
                    ip.maxPing = (int)ping;
                }
            }
        }

        public static void MeanPing(Base.BaseIp ip)
        {
            int result = 0;
            for (int i = 0; i < ip.PingDelay.Count; i++)
            {
                result += (int)ip.PingDelay[i];
            }
            ip.middlePing = result / ip.PingDelay.Count;

        }
    }
}
