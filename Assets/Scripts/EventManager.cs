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

    public static event Action<GameObject> ShipActions;
    public static void OnShipClickEvent(GameObject ship)
    {
        ShipActions?.Invoke(ship);
    }

}
