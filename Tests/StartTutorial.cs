using AltTester.AltTesterSDK.Driver;
using AOGameTest.Pages;

namespace AOGameTest.Tests;

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
    public void CompleteStarterTutorial()
    {
        new TutorialPage(altDriver!).TutorialStep1();
        new TutorialPage(altDriver!).TutorialStep2();
        new TutorialPage(altDriver!).TutorialStep3();
        new TutorialPage(altDriver!).TutorialStep4();
        
    }
    
}
        






