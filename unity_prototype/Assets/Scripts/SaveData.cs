using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int levelToLoad = 0;
    public int CoinCounterStore = 0;
    public List<PresentColor> AvailablePresentColor;
    public List<RibbonColor> AvailableRibbonColors;
    public List<bool> BoughtRibbonColorBool;
    public List<string> BoughtRibbonColorStr;
    public List<string> BoughtPaperColorStr;
}
