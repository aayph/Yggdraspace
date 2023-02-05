using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


[RequireComponent(typeof(Storage))]
public class RessourceReducer : MonoBehaviour
{
    Storage storage;
    public Resources reductionPerSecond;
    public List<Resources> transformer;

    void Start()
    {
        storage = GetComponent<Storage>();
    }

    void Update()
    {
        if (storage.resources.organic <= 0) return;

        storage.resources.organic = Mathf.Max(0f, storage.resources.organic - reductionPerSecond.organic * Time.deltaTime);
        storage.resources.metal = Mathf.Max(0f, storage.resources.metal - reductionPerSecond.metal * Time.deltaTime);
        storage.resources.water = Mathf.Max(0f, storage.resources.water - reductionPerSecond.water * Time.deltaTime);

        foreach (Resources r in transformer)
            TransformResources(r);
    }

    public float RemainingLifeTime()
    {
        float reductionOverTime = reductionPerSecond.organic;
        foreach (Resources r in transformer)
            reductionOverTime -= r.organic;
        float remainingTime = storage.resources.organic / reductionOverTime;
        if (remainingTime < 0f)
            return float.MaxValue;
        return remainingTime;
    }

    void TransformResources(Resources r)
    {
        Resources frameR = r * Time.deltaTime;
        if (storage.resources.CanAdd(frameR))
            storage.resources += frameR;
    }
}
