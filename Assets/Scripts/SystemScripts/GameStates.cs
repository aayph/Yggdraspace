using UnityEngine;

public class GameStates
{
    public static float gameTime = 0f;
    public static bool gamePaused = false;
    public static bool isInTravelSelection = false;
    public static bool isPlanetMenuOpen = false;
    public static GameObject traveler = null;
    public static Vector3 homePosition = Vector3.zero;
    public static Vector3 yggdrasilPosition = Vector3.one;
    public static float gameProgress = 0f;
    public static float targetResources = 10000f;
    public static bool gameWon = false;

    public static void Reset()
    {
        gameTime = 0f;
        gamePaused = false;
        isInTravelSelection = false;
        isPlanetMenuOpen = false;
        traveler = null;
        gameProgress = 0f;
        gameWon = false;
    }
}
