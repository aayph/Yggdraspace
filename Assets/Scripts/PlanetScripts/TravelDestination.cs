using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelDestination : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && GameStates.isInTravelSelection)
        {
            EventManager.TravelTargetSelected(transform.position);
        }
    }
}
