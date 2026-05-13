using System;
using System.Globalization;
using System.Threading;
using AltTester.AltTesterSDK.Driver;
using NUnit.Framework;




namespace AOGameTest.Pages;


public class CheatPanelPage: BasePage
{
    private const string CheatPanelRootPath = "/SRDebugger/Panel/SR_Canvas/SR_Content/SR_Main/SR_Tab";
    private const string OpenCheatButtonPath = "/SRDebugger/Trigger/Trigger(Clone)/SR_ButtonContainer/SR_TapButton";
    
    private const string HoursValuePath = "/SRDebugger/Panel/SR_Canvas/SR_Content/SR_Main/SR_Tab/Options(Clone)/SR_OptionsContent/SR_Viewport/SR_Content/TabContainer(Clone)[5]/Category(Clone)/NumberOption(Clone)/SR_Contents/SR_Label";   // Text или TMP с цифрой
    private const string HoursDecPath = "/SRDebugger/Panel/SR_Canvas/SR_Content/SR_Main/SR_Tab/Options(Clone)/SR_OptionsContent/SR_Viewport/SR_Content/TabContainer(Clone)[5]/Category(Clone)/NumberOption(Clone)/SR_ButtonDown";       // левая стрелка
    private const string HoursIncPath = "/SRDebugger/Panel/SR_Canvas/SR_Content/SR_Main/SR_Tab/Options(Clone)/SR_OptionsContent/SR_Viewport/SR_Content/TabContainer(Clone)[5]/Category(Clone)/NumberOption(Clone)/SR_ButtonUp>";       // правая стрелка
    
    
    private const string DropDownListResources = "/SRDebugger/Panel/SR_Canvas/SR_Content/SR_Main/SR_Tab/Options(Clone)/SR_OptionsContent/SR_Viewport/SR_Content/TabContainer(Clone)[5]/Category(Clone)[2]/DropDownOption(Clone)/Dropdown";       // стрелка выподения списка ресурсов
    private const string SkipitsResource = "/SRDebugger/Panel/SR_Canvas/SR_Content/SR_Main/SR_Tab/Options(Clone)/SR_OptionsContent/SR_Viewport/SR_Content/TabContainer(Clone)[5]/Category(Clone)[2]/DropDownOption(Clone)/Dropdown/Dropdown List/Viewport/Content/Item 58: SkipAds";       
    private const string InputRecourcesPath = "/SRDebugger/Panel/SR_Canvas/SR_Content/SR_Main/SR_Tab/Options(Clone)/SR_OptionsContent/SR_Viewport/SR_Content/TabContainer(Clone)[5]/Category(Clone)[2]/NumberOption(Clone)/SR_Contents/SR_Label";       
    private const string AddButtonResources = "/SRDebugger/Panel/SR_Canvas/SR_Content/SR_Main/SR_Tab/Options(Clone)/SR_OptionsContent/SR_Viewport/SR_Content/TabContainer(Clone)[5]/Category(Clone)[2]/ActionOption(Clone)[1]";       
    
    // private const string GoldInputPath = "/Canvas/.../GoldInput";
    // private const string AddGoldButtonPath = "/Canvas/.../AddGoldButton";
    // private const string CloseButtonPath = "/Canvas/.../Close";
    
    public CheatPanelPage(AltDriver driver) : base(driver) { }
    public void Open()
    {
        var btn = AltDriver.WaitForObject(By.PATH, OpenCheatButtonPath, timeout: 15);
        for (var i = 0; i < 3; i++)
        {
            btn.Tap();
            Thread.Sleep(120); 
        }
        AssertHoursStepperResponds();
    }
    public void AssertHoursStepperResponds()
    {
        var dec = AltDriver.WaitForObject(By.PATH, HoursDecPath, timeout: 10);
        var inc = AltDriver.WaitForObject(By.NAME, "SR_ButtonUp", timeout: 10);
        var label = AltDriver.WaitForObject(By.PATH, HoursValuePath, timeout: 10);
        
        Assert.That(dec.enabled, Is.True, $"Кнопка 'SR_ButtonDown' не активна — чит-панель не готова");
        Assert.That(inc.enabled, Is.True, $"Кнопка 'SR_ButtonUp' не активна — чит-панель не готова");
        Assert.That(label.enabled, Is.True, $"Поле 'SR_Label' не активно — чит-панель не готова");
        
        Assert.That(dec.enabled && inc.enabled && label.enabled, Is.True, "Hours UI не готов к вводу");
        
        
        var before = ReadIntFromUi(label);
        Assert.That(before, Is.GreaterThanOrEqualTo(0), "Некорректное исходное значение Hours");
        
        inc.Tap();
        Thread.Sleep(150);
        
        var afterInc = ReadIntFromUi(label);
        Assert.That(afterInc, Is.Not.EqualTo(before), "После '>' значение Hours не изменилось — панель не принимает ввод?");
        dec.Tap();
        Thread.Sleep(150);
        
        var afterDec = ReadIntFromUi(label);
        Assert.That(afterDec, Is.EqualTo(before), "После '<' значение Hours не вернулось к исходному");
    }
    private static int ReadIntFromUi(AltObject label)
    {
        var s = label.GetText()?.Trim();
        Assert.That(s, Is.Not.Null.And.Not.Empty, "Не удалось прочитать текст Hours");
        
        Assert.That(int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var v),
            Is.True, $"Hours: ожидалось число, получено '{s}'");
        return v;
    }


    public void AddSkipits()
    {
        var arrow = AltDriver.WaitForObject(By.PATH, DropDownListResources, timeout: 15);
        arrow.Tap();
        Thread.Sleep(500);
        Assert.That(arrow.enabled, Is.True, "Ожидалось выпадение списка ресурсов");
        
        var skipits = AltDriver.WaitForObject(By.PATH, SkipitsResource, timeout: 15);
        skipits.Tap();
        Thread.Sleep(500);
        
        var input = AltDriver.WaitForObject(By.PATH, InputRecourcesPath, timeout: 15);
        //input.Tap();   
        // input.SetComponentProperty(
        //     "TMPro.TMP_InputField",
        //     "Text",
        //     "5000",
        //     "Unity.UI.Text");
        
        //input.SetText("5000");
        Thread.Sleep(2000);
        
        var add = AltDriver.WaitForObject(By.PATH, AddButtonResources, timeout: 15);
        add.Tap();           
        
    }
    

}