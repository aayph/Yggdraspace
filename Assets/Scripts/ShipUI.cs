using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipUI : MonoBehaviour
{

    private void Awake()
    {
        EventManager.ShipActions += OpenUI;
    }

    private void OnDestroy()
    {
        EventManager.ShipActions -= OpenUI;
    }

    private void OpenUI(GameObject ship)
    {

    }


}
