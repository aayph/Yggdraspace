using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroudLifter : MonoBehaviour
{
    public Planet planet;
    
   
    void Start()
    {
        EventManager events = GetComponent<EventManager>();
        EventManager.PlanetExploreAction += EventManager_PlanetExploreAction;
    }

    private void EventManager_PlanetExploreAction(Planet obj)
    {
        StartCoroutine(ReduceScale( 0f));
    }
   
    IEnumerator ReduceScale(float targetscaling)
    {
        float reducingrate = 0.2f;
        while (targetscaling < transform.localScale.x)
        {
            transform.localScale -= reducingrate * Time.deltaTime * Vector3.one;
            yield return null;
        }
    }
    private void OnDestroy()
    {
        EventManager.PlanetExploreAction -= EventManager_PlanetExploreAction;
    }
}
