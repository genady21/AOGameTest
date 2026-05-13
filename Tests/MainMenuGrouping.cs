using Allure.NUnit;
using Allure.NUnit.Attributes;
using AltTester.AltTesterSDK.Driver;
using AOGameTest.Pages;


namespace AOGameTest.Tests;

[AllureNUnit]

public class MainMenuGrouping
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
        var page = new MainMenuPage(altDriver!);
        var cheatPanel = new CheatPanelPage(altDriver!);
        
        cheatPanel.Open();
        Thread.Sleep(500);
        cheatPanel.AddSkipits();
        
        
       
       
    }
}


