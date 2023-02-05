using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRandomizer : MonoBehaviour
{
    public Vector3 randomLimits;


    void Awake()
    {
        UnityEngine.Random.InitState(Mathf.RoundToInt(transform.position.x * 8564 + transform.position.y * 1742));
        Vector3 random = new Vector3(
            UnityEngine.Random.Range(-1f, 1f) * randomLimits.x,
            UnityEngine.Random.Range(-1f, 1f) * randomLimits.y,
            UnityEngine.Random.Range(-1f, 1f) * randomLimits.z
            );
        transform.localPosition += random;
    }
}
