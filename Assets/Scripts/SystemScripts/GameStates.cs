using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class GameStates
{
    public static float gameTime = 0f;
    public static bool gamePaused = false;
    public static bool isInTravelSelection = false;
    public static bool isPlanetMenuOpen = false;
    public static GameObject traveler = null;

    public static void Reset()
    {
        gameTime = 0f;
        gamePaused = false;
        isInTravelSelection = false;
        isPlanetMenuOpen = false;
        traveler = null;
    }
}
