using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public Resources resources;

    public void transferAllResources(Storage targetStorage) 
    {
        targetStorage.resources.water += resources.water;
        targetStorage.resources.organic += resources.organic;
        targetStorage.resources.metal += resources.metal;
        resources.setEmpty();
    }

    public void transferWater(Storage targetStorage, float amount)
    {
        if (resources.water < amount)
        {
            amount = resources.water;
        }
        targetStorage.resources.water += amount;
        resources.water -= amount;
    }

    public void transferOrganic(Storage targetStorage, float amount)
    {
        if (resources.organic < amount)
        {
            amount = resources.organic;
        }
        targetStorage.resources.organic += amount;
        resources.organic -= amount;
    }

    public void transferMetal(Storage targetStorage, float amount)
    {
        if (resources.metal < amount)
        {
            amount = resources.metal;
        }
        targetStorage.resources.metal += amount;
        resources.metal -= amount;
    }


}
