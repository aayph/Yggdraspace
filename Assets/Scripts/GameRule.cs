using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameRule
{
    public static bool TravelIsContinous = true;

    public enum DepletionOptions { All, None, AllButFuel }
    public static DepletionOptions depletionDuringLanding = DepletionOptions.AllButFuel;


}
