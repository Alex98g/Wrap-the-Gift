using UnityEngine;

namespace Game
{
    public enum PresentColor
    {
        White,
        Red,
        Green,
        Blue,
        Yellow,
        VividSkyBlue,
        SeaGreenCrayola,
        SafetyOrangeBlazeOrange,
        RedCrayola,
        newPaperColor
    }

    public enum RibbonColor
    {
        White,
        Red,
        Green,
        Blue,
        Yellow,
        SeaGreenCrayola,
        SafetyOrangeBlazeOrange,
        RedCrayola,
        newRibbonColor,
    }

    public static class PresentColorExtension
    {
        public static Color ToColor(this PresentColor presentColor)
        {
            switch (presentColor) {
                case PresentColor.Blue:
                    return Color.blue;
                case PresentColor.Red:
                    return Color.red;
                case PresentColor.Green:
                    return Color.green;
                case PresentColor.Yellow:
                    return Color.yellow;
                case PresentColor.VividSkyBlue:
                    return new Color(0, 0.8039216f, 1);
                case PresentColor.SeaGreenCrayola:
                    return new Color(0, 0.9647059f, 0.7960784f);
                case PresentColor.SafetyOrangeBlazeOrange:
                    return new Color(1, 0.4f, 0);
                case PresentColor.RedCrayola:
                    return new Color(0.9686275f, 0, 0.3372549f);
            }
            throw new System.Exception($"Can't transform present color {presentColor} into common color");
        }
       
        public static int ToInt(this PresentColor NewPaperColor)
        {
            switch (NewPaperColor)
            {
                case PresentColor.VividSkyBlue:
                    return 15;
                case PresentColor.SeaGreenCrayola:
                    return 20;
                case PresentColor.SafetyOrangeBlazeOrange:
                    return 20;
                case PresentColor.RedCrayola:
                    return 20;
            }
            throw new System.Exception($"Can't transform ribbon color {NewPaperColor} into common color");
        }
    }
    
    public static class RibbonColorExtension
    {
        public static Color ToColor(this RibbonColor ribbonColor)
        {
            switch (ribbonColor)
            {
                case RibbonColor.Blue:
                    return Color.blue;
                case RibbonColor.Red:
                    return Color.red;
                case RibbonColor.Green:
                    return Color.green;
                case RibbonColor.Yellow:
                    return Color.yellow;
                case RibbonColor.SeaGreenCrayola:
                    return new Color(0, 0.9647059f, 0.7960784f);
                case RibbonColor.SafetyOrangeBlazeOrange:
                    return new Color(1, 0.4f, 0);
                case RibbonColor.RedCrayola:
                    return new Color(0.9686275f, 0, 0.3372549f);
            }

            throw new System.Exception($"Can't transform ribbon color {ribbonColor} into common color");
        }
        public static int ToInt(this RibbonColor NewribbonColor)
        {
            switch (NewribbonColor)
            {
                case RibbonColor.SeaGreenCrayola:
                    return 20;
                case RibbonColor.SafetyOrangeBlazeOrange:
                    return 20;
                case RibbonColor.RedCrayola:
                    return 15;
            }
            throw new System.Exception($"Can't transform ribbon color {NewribbonColor} into common color");
        }
    }
    

}
