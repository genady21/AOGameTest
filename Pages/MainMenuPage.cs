using AltTester.AltTesterSDK.Driver;

namespace AOGameTest.Pages
{
    public class MainMenuPage : BasePage
    {
        private const string ArenasButtonPath = "/Canvas/MenuUI(Clone)/BottomMainScreen/SafeArea/Bottom/ArenasButton";
        private const string ShopBtnPath = "/Canvas/MenuUI(Clone)/BottomMainScreen/SafeArea/LeftPanelButton/Shop_btn";
        private const string MainMenuGrouping = "/Canvas/MenuUI(Clone)/BottomMainScreen/SafeArea/LeftPanelButton/HorizontalGroup/GroupButton/GroupButton";

        public MainMenuPage(AltDriver altDriver) : base(altDriver) { }


        public void StickmanTVTest()
        {
            var mMGrouping = AltDriver.WaitForObject(By.PATH, MainMenuGrouping, timeout: 20);
            mMGrouping.Tap();
            Thread.Sleep(2000);
        }
    }
}