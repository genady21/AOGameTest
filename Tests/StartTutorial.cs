using Allure.NUnit;
using Allure.NUnit.Attributes;
using AltTester.AltTesterSDK.Driver;
using AOGameTest.Pages;

namespace AOGameTest.Tests;

[AllureNUnit]
public class TutorialCombatTests
{
    private AltDriver? altDriver;
    

    [SetUp]
    public void SetUp()
    {
        altDriver = new AltDriver(host: "127.0.0.1", port: 13000, appName: "__default__", secureMode: false);
    }

    [TearDown]
    public void TearDown()
    {
        altDriver?.Stop();
    }

    [Test]
    [CancelAfter(600_000)]
    public void CompleteStarterTutorial()
    {
        var page = new TutorialPage(altDriver!);
        
        page.TutorialStep1();
        page.TutorialStep2();
        page.TutorialStep3();
        page.TutorialStep4();
        page.TutorialStep5();
       
    }
    
}
        






