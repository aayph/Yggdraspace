using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroudLifter : MonoBehaviour
{
    public Planet planet;
   
    void Awake()
    {
        if (planet.isExplored)
        {
            transform.localScale = Vector3.zero;
            return;
        }
        EventManager.PlanetExploreAction += EventManager_PlanetExploreAction;
    }

    private void OnDestroy()
    {
        EventManager.PlanetExploreAction -= EventManager_PlanetExploreAction;
    }

    private void EventManager_PlanetExploreAction(Planet obj)
    {
        if (obj != planet) return;
        StartCoroutine(ReduceScale(0f));
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
}
