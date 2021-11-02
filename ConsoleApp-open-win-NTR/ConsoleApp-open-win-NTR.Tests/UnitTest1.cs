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

        public void CheckStatus2_Test(bool expected , string host)
        {
            Network.CheckAvailability test = new Network.CheckAvailability(host);
            Assert.AreEqual(expected, test.CheckStatus());
        }
    }
}