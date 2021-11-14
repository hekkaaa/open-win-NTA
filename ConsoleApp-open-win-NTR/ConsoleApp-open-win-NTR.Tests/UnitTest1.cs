using NUnit.Framework;

namespace ConsoleApp_open_win_NTR.Tests
{
    public class CheckAvailability
    {
        [SetUp]
        public void Setup()
        {

        }
   
        [TestCase(true, "ya.ru")]
        [TestCase(false, "192.168.0.22")]
        [TestCase(true, "vk.com")]
        [TestCase(true, "8.8.8.8")] 
        [TestCase(false, "noaccessurl.com")]

        public void CheckStatus_Test(bool expected , string host)
        {
            Network.CheckAvailability test = new Network.CheckAvailability(host);
            Assert.AreEqual(expected, test.CheckStatus());
        }


        [TestCase(20, 100, 20)]
        [TestCase(0, 100, 0)]
        [TestCase(70, 10, 7)]
        [TestCase(8.4699453551912569, 366, 31)]

        public void RateLosses_Tests(double expected, int a, int b)
        {
            Assert.AreEqual(expected, Algorithm.StatisticAlgorithm.RateLosses(a, b));
        }
    }
}