using AltTester.AltTesterSDK.Driver;

namespace AOGameTest.Pages
{
    public abstract class BasePage
    {
        protected AltDriver AltDriver;

        protected BasePage(AltDriver altDriver)
        {
            AltDriver = altDriver;
        }
    }
}