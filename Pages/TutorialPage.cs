using System.Threading;
using AltTester.AltTesterSDK.Driver;
using NUnit.Framework;

namespace AOGameTest.Pages;



public class TutorialPage: BasePage
{
    public string SlotPath = "/Canvas/RoundUI(Clone)/SafeArea/PlayerPanel(Clone)/Stuff/Top/Arrows/ActiveArrowSlot/Slot";
    public string SlotPathLaser = "/Canvas/RoundUI(Clone)/SafeArea/PlayerPanel(Clone)/Stuff/Top/Arrows/ArrowScrollPanel/Mask/ScrollView/Content/SlotArrow_Round(Clone)/Slot/Arrows/Laser";

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
        Thread.Sleep(400);
        
        var o = AltDriver.FindObject(By.PATH, SlotPathLaser);
        if (o == null || !o.enabled)
        {
            slot.Tap();
            Thread.Sleep(400);
        }
        
        var slot1 = AltDriver.WaitForObject(By.PATH, SlotPathLaser, timeout: 20);
        slot1.Tap();
        Thread.Sleep(4000);
        AltDriver.Swipe(start, end, duration: 0.45f, wait: true);
    }

   
    
}
