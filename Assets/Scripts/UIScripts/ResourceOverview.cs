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
    public string numberFormat = "{0:0.0}";

    public void UpdateNumbers(Resources resources)
    {
        organicNumberField.text = String.Format(numberFormat, resources.organic);
        metalNumberField.text = String.Format(numberFormat, resources.metal);
        waterNumberField.text = String.Format(numberFormat, resources.water);
    }
}
