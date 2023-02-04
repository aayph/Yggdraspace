using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Resources
{
    public float organic;
    public float metal;
    public float water;

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
        r.organic = value1.metal + value2.metal;
        r.organic = value1.water + value2.water;
        return r;
    }

    public static Resources operator -(Resources value1, Resources value2)
    {
        Resources r = new Resources();
        r.organic = value1.organic - value2.organic;
        r.organic = value1.metal - value2.metal;
        r.organic = value1.water - value2.water;
        return r;
    }
}