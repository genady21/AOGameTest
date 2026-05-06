using NUnit.Framework;
using AltTester.AltTesterSDK.Driver;
using AOGameTest.Pages;

namespace AOGameTest.Tests
{
    public class NavigationTest
    {
        private AltDriver altDriver;
        private MainMenuPage mainMenuPage;

        [SetUp]
        public void Setup()
        {
            BasePage.SetupAdbReverse();
            altDriver = new AltDriver(host: "127.0.0.1", port: 13000, appName: "__default__");
            mainMenuPage = new MainMenuPage(altDriver);
        }

        [TearDown]
        public void TearDown()
        {
            altDriver.Stop();
        }

        [Test]
        public void TestOpenArenasAndBack()
        {
            mainMenuPage.OpenArenas();
        }
    }
}