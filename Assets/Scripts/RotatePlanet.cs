using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState(Mathf.RoundToInt(transform.position.x * 1000 + transform.position.y *15000 ));
        RotateX *= UnityEngine.Random.Range(-1f,1f);
        RotateY *= UnityEngine.Random.Range(-1f, 1f);
        RotateZ *= UnityEngine.Random.Range(-1f, 1f);
    }

    public float RotateX;
    public float RotateY;
    public float RotateZ;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateX * Time.deltaTime, RotateY * Time.deltaTime, RotateZ * Time.deltaTime) ;
    }
}
