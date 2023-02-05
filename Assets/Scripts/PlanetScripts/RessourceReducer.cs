using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


[RequireComponent(typeof(Storage))]
public class RessourceReducer : MonoBehaviour
{
    Storage storage;
    public Resources reductionPerSecond;
    public float deadTimeOut = 0.5f;
    public bool endlessFood = false;

    [HideInInspector] public List<Resources> transformer;

    float deadTime = -1f;

    void Start()
    {
        storage = GetComponent<Storage>();
    }

    void Update()
    {
        if (IsDead()) return;

        storage.resources.organic = Mathf.Max(0f, storage.resources.organic - reductionPerSecond.organic * Time.deltaTime);
        storage.resources.metal = Mathf.Max(0f, storage.resources.metal - reductionPerSecond.metal * Time.deltaTime);
        storage.resources.water = Mathf.Max(0f, storage.resources.water - reductionPerSecond.water * Time.deltaTime);

        foreach (Resources r in transformer)
            TransformResources(r);

        if (endlessFood)
            storage.resources.organic = float.PositiveInfinity;
    }

    public bool IsDead()
    {
        if (storage.resources.organic > 0f)
        {
            deadTime = -1f;
            return false;
        }
        if (deadTime == -1f)
            deadTime = GameStates.gameTime;
        return (deadTime + deadTimeOut < GameStates.gameTime);
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
