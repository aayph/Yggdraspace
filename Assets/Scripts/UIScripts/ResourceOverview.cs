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
        organicNumberField.text = String.Format("{0:0.0}", resources.organic);
        metalNumberField.text = String.Format("{0:0.0}", resources.metal);
        waterNumberField.text = String.Format("{0:0.0}", resources.water);
    }
}
