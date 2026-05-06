using AltTester.AltTesterSDK.Driver;

namespace AOGameTest.Pages
{
    public class MainMenuPage : BasePage
    {
        private const string ArenasButtonPath = "/Canvas/MenuUI(Clone)/BottomMainScreen/SafeArea/Bottom/ArenasButton";
        private const string ShopBtnPath = "/Canvas/MenuUI(Clone)/BottomMainScreen/SafeArea/LeftPanelButton/Shop_btn";

        public MainMenuPage(AltDriver altDriver) : base(altDriver) { }

        public void OpenArenas()
        {
            AltDriver.WaitForObject(By.PATH, ArenasButtonPath, timeout: 20).Tap();
        }

        public void OpenShop()
        {
            AltDriver.WaitForObject(By.PATH, ShopBtnPath, timeout: 20).Tap();
        }
    }
}