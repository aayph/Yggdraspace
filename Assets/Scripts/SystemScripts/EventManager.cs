using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class EventManager
{
    public static event Action<string> SampleActions;
    public static void OnSampleEvent(string data)
    {
        SampleActions?.Invoke(data);
    }

    public static event Action<GameObject> ShipAction;
    public static void OnShipClickEvent(GameObject ship)
    {
        ShipAction?.Invoke(ship);
    }

    public static event Action<Vector3> ShipTravelAction;
    public static void TravelTargetSelected(Vector3 target)
    {
        ShipTravelAction?.Invoke(target);
    }



    public static event Action<Planet> PlanetAction;
    public static void PlanetClickedEvent(Planet planet)
    {
        PlanetAction?.Invoke(planet);
    }



    public static event Action<string> PlayerPrefsUpdate;
    public static void PlayerPrefsUpdated(string identifier)
    {
        PlayerPrefsUpdate?.Invoke(identifier);
    }


    //0f - 2f
    public static event Action<float> GameProgressUpdate;
    public static void GameProgressUpdated(float gameProgress)
    {
        GameProgressUpdate?.Invoke(gameProgress);
    }


    //0 - 2
    public static event Action<int> DangerLevelUpdate;
    public static void DangerLevelUpdated(int dangerLevel)
    {
        DangerLevelUpdate?.Invoke(dangerLevel);
    }


    public static event Action<GameManager.ActiveScene> SceneChange;
    public static void SceneChanged(GameManager.ActiveScene newScene)
    {
        SceneChange?.Invoke(newScene);
    }


    public static event Action<Planet, bool> PlanetHoverAction;
    public static void PlanetHoverEvent(Planet planet, bool isOpened)
    {
        PlanetHoverAction?.Invoke(planet, isOpened);
    }
}
