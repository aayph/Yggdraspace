using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


[Serializable]
public class Resources
{
    public float organic;
    public float metal;
    public float water;


    public bool CanAdd(Resources value)
    {
        return (organic >= -value.organic && metal >= -value.metal && water >= -value.water);
    }
    public bool CanSubstract(Resources value)
    {
        return (organic >= value.organic && metal >= value.metal && water >= value.water);
    }

    public bool HasValue()
    {
        return (organic != 0 || metal != 0 || water != 0);
    }

    public static Resources operator *(Resources value, float factor)
    {
        Resources r = new Resources();
        r.organic = value.organic * factor;
        r.metal = value.metal * factor;
        r.water = value.water * factor;
        return r;
    }

    public static Resources operator +(Resources value)
    {
        Resources r = new Resources();
        r.organic += value.organic;
        r.metal += value.metal;
        r.water += value.water;
        return r;
    }

    public static Resources operator -(Resources value)
    {
        Resources r = new Resources();
        r.organic -= value.organic;
        r.metal -= value.metal;
        r.water -= value.water;
        return r;
    }

    public static Resources operator +(Resources value1, Resources value2)
    {
        Resources r = new Resources();
        r.organic = value1.organic + value2.organic;
        r.metal = value1.metal + value2.metal;
        r.water = value1.water + value2.water;
        return r;
    }

    public static Resources operator -(Resources value1, Resources value2)
    {
        Resources r = new Resources();
        r.organic = value1.organic - value2.organic;
        r.metal = value1.metal - value2.metal;
        r.water = value1.water - value2.water;
        return r;
    }

    public void setEmpty()
    {
        organic = 0;
        metal = 0;
        water = 0;
    }
}