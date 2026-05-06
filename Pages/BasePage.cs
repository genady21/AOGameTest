using AltTester.AltTesterSDK.Driver;
using System.Diagnostics;

namespace AOGameTest.Pages
{
    public abstract class BasePage
    {
        protected AltDriver AltDriver;

        protected BasePage(AltDriver altDriver)
        {
            AltDriver = altDriver;
        }

        public static void SetupAdbReverse()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "adb",
                Arguments = "reverse tcp:13000 tcp:13000",
                UseShellExecute = false
            });
        }
    }
}