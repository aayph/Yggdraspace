using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameRule
{
    public static bool TravelIsContinous = true;

    public enum DepletionOptions { All, None, AllButFuel }
    public static DepletionOptions depletionDuringLanding = DepletionOptions.AllButFuel;
    public static float[] DangerLevels = { 240, 120 };
    public static bool ColonizeShipsCanExplore = false;

}
