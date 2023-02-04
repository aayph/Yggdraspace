using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerSample : MonoBehaviour
{
    void Start()
    {
        EventManager.OnSampleEvent("test");
    }
}
