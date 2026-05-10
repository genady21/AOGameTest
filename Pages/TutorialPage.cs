using AltTester.AltTesterSDK.Driver;

namespace AOGameTest.Pages;



public class TutorialPage: BasePage
{
    public string SlotPath = 
        "/Canvas/RoundUI(Clone)/SafeArea/PlayerPanel(Clone)/Stuff/Top/Arrows/ActiveArrowSlot/Slot";
    public string SlotPathLaser = 
        "/Canvas/RoundUI(Clone)/SafeArea/PlayerPanel(Clone)/Stuff/Top/Arrows/ArrowScrollPanel/Mask/ScrollView/Content/SlotArrow_Round(Clone)/Slot/Arrows/Laser";
    public string ButtonReceivingFireArrow = 
        "/Canvas/OverlayPopupController/InformationNewAward_OverlayPopup(Clone)/Panel/Content/FirstRewardInformationNewAwardContent(Clone)/Panel/Ok_btn";
    public string ButtonEndTutorialBattleN2 = 
        "/Canvas/MenuUI(Clone)/VersusScreen/SafeArea/Bottom/NextBtn";
    public string ButtonSelectDefaultSkin = 
        "/Canvas/MenuUI(Clone)/CustomizationSetSelectionPopup/Panel/RootSelection/SectionGrid/TutorialCustomizationElement(Clone)/Content/TapZone/ItemSimple/LeaveButton";

    /// <summary>Что показать после «Оставить» на попапе скина (замените, если уходит не в главное меню).</summary>
    public string PathAfterSelectDefaultSkin =
        "/Canvas/MenuUI(Clone)/BottomMainScreen/SafeArea/Bottom/ArenasButton";

    public TutorialPage(AltDriver driver) : base(driver) { }
    
    
    

    public void TutorialStep1()
    {
        //Нашли чучело
        var dummy = AltDriver.WaitForObject(By.NAME, "ArcherDummy(Clone)", timeout: 30);
        Assert.That(dummy.enabled, Is.True);
        //Нашли и взяли позицию от стикмана
        var archer = AltDriver.WaitForObject(By.NAME, "Archer(Clone)", timeout: 30);
        var p = archer.GetScreenPosition();
        //выстрел
        var start = new AltVector2(p.x - 80f, p.y + 20f);
        var end = new AltVector2(p.x + 300f, p.y + 120f);
        AltDriver.Swipe(start, end, duration: 0.45f, wait: true);
        
        Thread.Sleep(4000);
        //Выбираем стрелы
        var slot = AltDriver.WaitForObject(By.PATH, SlotPath, timeout: 20);
        slot.Tap();
        Thread.Sleep(2000);
        
        //Проверяем или лазерная есть на экране
        var o = AltDriver.FindObject(By.PATH, SlotPathLaser);
        if (o == null || !o.enabled)
        {
            slot.Tap();
            Thread.Sleep(2000);
        }
        //Выбираем лазерную стрелу
        var slot1 = AltDriver.WaitForObject(By.PATH, SlotPathLaser, timeout: 20);
        slot1.Tap();
        
        Thread.Sleep(3000);
        //выстрел
        AltDriver.Swipe(start, end, duration: 0.45f, wait: true);
    }


    public void TutorialStep2()
    //Получение огенной стрелы
    {
        var butonReceiving = AltDriver.WaitForObject(By.PATH, ButtonReceivingFireArrow, timeout: 30);
        Assert.That(butonReceiving.enabled, Is.True);
        butonReceiving.Tap();
    }
    
    public void TutorialStep3()
    //Второй туториальный бой
    {
        Thread.Sleep(6000);
        void SelectLaser()
        {
            var slot = AltDriver.WaitForObject(By.PATH, SlotPath, timeout: 20);
            slot.Tap();
            TestContext.WriteLine("Слот найден");
            Thread.Sleep(2000);

            //Проверяем или лазерная есть на экране
            var o = AltDriver.FindObject(By.PATH, SlotPathLaser);
            if (o == null || !o.enabled)
            {
                slot = AltDriver.WaitForObject(By.PATH, SlotPath, timeout: 20);
                slot.Tap();
                TestContext.WriteLine("Слот с лазером не найден");
                Thread.Sleep(2000);
            }

            //Выбираем лазерную стрелу
            var slotLaser = AltDriver.WaitForObject(By.PATH, SlotPathLaser, timeout: 20);
            Assert.That(slotLaser.enabled, Is.True, "Лазер в дереве, но выключен");
            slotLaser.Tap();
            
        }
        void Fire()
        {
            var archer = AltDriver.WaitForObject(By.NAME, "Archer(Clone)", timeout: 30);
                var p = archer.GetScreenPosition();
                //выстрел
                var start = new AltVector2(p.x - 80f, p.y + 20f);
                var end = new AltVector2(p.x + 300f, p.y + 120f);
                AltDriver.Swipe(start, end, duration: 0.45f, wait: true);
        }

        SelectLaser();
        Thread.Sleep(5000);
        Fire();
        Thread.Sleep(15000);
        Fire();
        // Дать игре дорисовать конец боя и показать VersusScreen с Next, иначе Step4 стартует слишком рано.
        Thread.Sleep(8000);
    }


    public void TutorialStep4()
    {
        TestContext.WriteLine("TutorialStep4: Versus Next → ждём «{0}»", ButtonSelectDefaultSkin);
        const int maxTries = 4;
        const int waitNextScreenSec = 25;

        for (var attempt = 1; attempt <= maxTries; attempt++)
        {
            var next = AltDriver.WaitForObject(By.PATH, ButtonEndTutorialBattleN2, timeout: 45);
            Assert.That(next.enabled, Is.True);
            AltDriver.Tap(next.GetScreenPosition(), count: 1, interval: 0.1f, wait: true);
            Thread.Sleep(3000);

            try
            {
                var marker = AltDriver.WaitForObject(
                    By.PATH,
                    ButtonSelectDefaultSkin,
                    enabled: true,
                    timeout: waitNextScreenSec);
                Assert.That(marker.enabled, Is.True);
                // Сразу жмём «Оставить», пока попап стабилен; иначе к шагу 5 дерево уже другое — WaitForObject снова не найдёт тот же путь.
                AltDriver.Tap(marker.GetScreenPosition(), count: 1, interval: 0.1f, wait: true);
                return;
            }
            catch (Exception ex)
            {
                TestContext.WriteLine(
                    $"Тап #{attempt}: за {waitNextScreenSec} не появился маркер следующего экрана ({ButtonSelectDefaultSkin}). {ex.Message}");
            }
        }

        Assert.Fail(
            $"После {maxTries} тапов по Next не дождались элемента следующего шага: {ButtonSelectDefaultSkin}.");
    }

    public void TutorialStep5()
    {
        // // «Оставить» уже нажато в конце шага 4. Здесь — подтверждение следующего экрана.
        // Thread.Sleep(1500);
        // var nextScreen = AltDriver.WaitForObject(By.PATH, PathAfterSelectDefaultSkin, timeout: 45);
        // Assert.That(nextScreen.enabled, Is.True, PathAfterSelectDefaultSkin);
    }
}
