using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class ResourceOverview : MonoBehaviour
{
    public TextMeshProUGUI organicNumberField;
    public TextMeshProUGUI metalNumberField;
    public TextMeshProUGUI waterNumberField;

    public void UpdateNumbers(Resources resources)
    {
        organicNumberField.text = Math.Round(resources.organic, 2).ToString();
        metalNumberField.text = Math.Round(resources.metal, 2).ToString();
        waterNumberField.text = Math.Round(resources.water, 2).ToString();
    }
}
