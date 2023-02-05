using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroudLifter : MonoBehaviour
{
    public GameObject Shroud;
    public Vector3 scalechanger, wishedscale;
   
    void Start()
    {
        EventManager events = GetComponent<EventManager>();
        EventManager.PlanetExploreAction += EventManager_PlanetExploreAction;
        scalechanger = new Vector3(-.2f, -.2f, -.2f);
        wishedscale = new Vector3(0, 0, 0);
    }

    private void EventManager_PlanetExploreAction(Planet obj)
    {
        StartCoroutine(ReduceScale( 2f));
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
