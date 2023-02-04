using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipUI : MonoBehaviour
{
    public TextMeshProUGUI[] resourcesTexts;
    private GameObject associatedShip;

    private void Start()
    {
        EventManager.ShipAction += OpenUI;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventManager.ShipAction -= OpenUI;
    }

    private void OpenUI(GameObject ship)
    {
        gameObject.SetActive(true);
        associatedShip = ship;
        Resources res = ship.GetComponent<Ship>().storage.resources;
        resourcesTexts[0].SetText("Brokolie " + res.organic);
        resourcesTexts[1].SetText("Water " + res.water);
        resourcesTexts[2].SetText("Metal " + res.metal);
    }

    public void clickOnSendShipButton()
    {
        GameStates.isInTravelSelection = true;
        GameStates.traveler = associatedShip;
        gameObject.SetActive(false);
    }


}
