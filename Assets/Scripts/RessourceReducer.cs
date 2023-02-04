using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Storage))]
public class RessourceReducer : MonoBehaviour
{
    Storage storage;
    public Resources reductionPerSecond;

    void Start()
    {
        storage = GetComponent<Storage>();
    }

    void Update()
    {
        storage.resources.organic = Mathf.Max(0f, storage.resources.organic - reductionPerSecond.organic * Time.deltaTime);
        storage.resources.metal = Mathf.Max(0f, storage.resources.metal - reductionPerSecond.metal * Time.deltaTime);
        storage.resources.water = Mathf.Max(0f, storage.resources.water - reductionPerSecond.water * Time.deltaTime);
    }
}
