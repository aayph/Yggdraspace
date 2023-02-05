using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameRule
{
    public static bool TravelIsContinous = true;

    public enum DepletionOptions { All, None, AllButFuel }
    public static DepletionOptions depletionDuringLanding = DepletionOptions.AllButFuel;
    public static float[] DangerLevels = { 180, 90};
    public static bool ColonizeShipsCanExplore = false;
    public static float maxColonisationRange = 15;
    public static float maxYggdrasilationRange = 15;

}
