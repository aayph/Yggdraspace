using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
