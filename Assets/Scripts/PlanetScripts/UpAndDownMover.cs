using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownMover : MonoBehaviour
{
    float startposition;
    public float difference;
    public float animationSpeed;
    float progressTime;

    // Start is called before the first frame update
    void Start()
    {
        startposition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        progressTime += Time.deltaTime / animationSpeed;
        if (progressTime > 6.28f) progressTime -= 6.28f;

        transform.position = new Vector3(transform.position.x, (Mathf.Lerp(startposition, difference, (Mathf.Sin(progressTime)+1f)/2 )), transform.position.z);

    }
}
