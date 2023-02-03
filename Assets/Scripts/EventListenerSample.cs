using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EventListenerSample : MonoBehaviour
{
    private void Awake()
    {
        EventManager.SampleActions += SampleFunction;
    }

    private void OnDestroy()
    {
        EventManager.SampleActions -= SampleFunction;
    }

    private void SampleFunction(string data)
    {

    }
}
