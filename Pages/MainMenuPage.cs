using AltTester.AltTesterSDK.Driver;

namespace AOGameTest.Pages
{
    public class MainMenuPage : BasePage
    {
        private const string ArenasButtonPath = 
            "/Canvas/MenuUI(Clone)/BottomMainScreen/SafeArea/Bottom/ArenasButton";
        private const string ShopBtnPath = 
            "/Canvas/MenuUI(Clone)/BottomMainScreen/SafeArea/LeftPanelButton/Shop_btn";
        
        private const string MainMenuGrouping = 
            "/Canvas/MenuUI(Clone)/BottomMainScreen/SafeArea/LeftPanelButton/HorizontalGroup/GroupButton/GroupButton";
        private const string DailyBonusBottonPath = 
            "/Canvas/MenuUI(Clone)/GroupPopup/SafeArea/Panel/PopupsContainer/FeatureButtons/IconGroup/DailyBonus/UI Button/DailyBonusButton/DailyBonusMainMenuButton";
        private const string StickmanTvButtonPath = 
            "/Canvas/MenuUI(Clone)/GroupPopup/SafeArea/Panel/PopupsContainer/FeatureButtons/IconGroup/TV/UI Button";
        private const string AdsRevardButtonPath = 
            "/Canvas/MenuUI(Clone)/GroupPopup/SafeArea/Panel/PopupsContainer/FeatureButtons/IconGroup/AdsReward/UI Button";
        private const string ProgressBarAdsStickmanTvPath = 
            "/Canvas/MenuUI(Clone)/GroupPopup/SafeArea/Panel/PopupsContainer/TelevisionPopup/Panel/TelevisionPopupContent/Background/RewardProgress/ProgressValue";
        
        public MainMenuPage(AltDriver altDriver) : base(altDriver) { }


        public void StickmanTVTest()
        {
            var mMGrouping = AltDriver.WaitForObject(By.PATH, MainMenuGrouping, timeout: 10);
            mMGrouping.Tap();
            Thread.Sleep(2000);
            
            var stickmanTvButton = AltDriver.WaitForObject(By.PATH, StickmanTvButtonPath, timeout: 10);
            stickmanTvButton.Tap();
            Thread.Sleep(2000);
            
            var progressBar = AltDriver.WaitForObject(By.PATH, ProgressBarAdsStickmanTvPath, timeout: 10);
            var text = progressBar.GetText()?.Trim();
            Assert.That(text, Is.EqualTo("0 / 15")); // пример
            
        }
        private string ReadStickmanTvProgressText()
        {
            var bar = AltDriver.WaitForObject(By.PATH, ProgressBarAdsStickmanTvPath, timeout: 10);
            return bar.GetText()?.Trim() ?? string.Empty;
        }

        public void RunPrimerUntilFullThenReset(TimeSpan timeout)
        {
            var deadline = DateTime.UtcNow + timeout;

            // 1) пока не 15/15 — крутим Primer
            while (ReadStickmanTvProgressText() != "15 / 15")
            {
                Assert.That(DateTime.UtcNow, Is.LessThan(deadline),
                    "Таймаут: не дождались 15 / 15 после вызовов Primer()");

                Primer();
                Thread.Sleep(200); // при необходимости подкрути / замени на ожидание смены текста
            }

            // 2) пока не вернулось 0/15 — ждём (или снова Primer — см. ниже)
            while (ReadStickmanTvProgressText() != "0 / 15")
            {
                Assert.That(DateTime.UtcNow, Is.LessThan(deadline),
                    "Таймаут: не дождались сброса 0 / 15 после 15 / 15");

                // Если сброс сам по таймеру/рекламе — достаточно подождать:
                Thread.Sleep(200);

                // Если сброс только после ещё одного Primer() — раскомментируй:
                // Primer();
            }
        }
    }
}