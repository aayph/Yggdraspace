using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftShroud : MonoBehaviour
{
    public event EventHandler PlanetExploreAction;
    public GameObject Shroud;
    public Vector3 scalechanger;
    void Start()
    {
        scalechanger = new Vector3(-.2f, -.2f, -.2f);
    }

    // Update is called once per frame
    void Update()
    {
        //if ( event -> PlanetExploreAction occurs and Shroudscale != to 0){
        Shroud.transform.localScale += scalechanger; 
        //}
    }
}
