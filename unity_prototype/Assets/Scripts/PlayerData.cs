using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

public static class PlayerData
{
    public static int levelToLoad = 0;
    public static int CoinCounter = 0;
    public static int CoinCounterStore = 0;
    public static List<PresentColor> AvailablePresentColor = new List<PresentColor> {
        PresentColor.Red,
        PresentColor.Green,
        PresentColor.Blue,
        PresentColor.Yellow
    };

    public static List<RibbonColor> AvailableRibbonColors = new List<RibbonColor>
    {
        RibbonColor.Blue,
        RibbonColor.Green,
        RibbonColor.Red,
        RibbonColor.Yellow

    };
    public static List<RibbonColor> AvailableNewRibbonColors = new List<RibbonColor>
    {
        RibbonColor.SeaGreenCrayola,
        RibbonColor.SafetyOrangeBlazeOrange,
        RibbonColor.RedCrayola,
        RibbonColor.newRibbonColor1,
        RibbonColor.newRibbonColor2,
        RibbonColor.newRibbonColor3,
        RibbonColor.newRibbonColor4,
        RibbonColor.newRibbonColor5,
        RibbonColor.newRibbonColor6


    };
    public static List<PresentColor> AvailableNewPaperColors1 = new List<PresentColor>
    {
        PresentColor.VividSkyBlue,
        PresentColor.SeaGreenCrayola,
        PresentColor.SafetyOrangeBlazeOrange,
        PresentColor.newPaperColor1,
        PresentColor.newPaperColor2,
        PresentColor.newPaperColor3,
        PresentColor.newPaperColor4,
        PresentColor.newPaperColor5,
        PresentColor.newPaperColor6
    };
    public static List<PresentColor> AvailableNewPaperColors2 = new List<PresentColor>
    {
        PresentColor.RedCrayola,
        PresentColor.newPaperColor1,
        PresentColor.newPaperColor2,
        PresentColor.newPaperColor3,
        PresentColor.newPaperColor4,
        PresentColor.newPaperColor5,
        PresentColor.newPaperColor6,
        PresentColor.newPaperColor7,
        PresentColor.newPaperColor8,
    };
    public static List<string> BoughtRibbonColorStr = new List<string> { };
    public static List<string> BoughtPaperColorStr = new List<string> { };

}
