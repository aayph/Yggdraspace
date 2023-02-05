using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipUI : MonoBehaviour
{
    public enum ResourceType {water, organic, metal}

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
        UpdateInterface();
    }

    public void clickOnSendShipButton()
    {
        GameStates.isInTravelSelection = true;
        GameStates.traveler = associatedShip;
        gameObject.SetActive(false);
    }

    private void UpdateInterface()
    {
        Resources res = associatedShip.GetComponent<Ship>().storage.resources;
        resourcesTexts[0].SetText("" + (int)res.organic);
        resourcesTexts[1].SetText("" + (int)res.metal);
        resourcesTexts[2].SetText("" + (int)res.water);
        resourcesTexts[3].SetText(associatedShip.GetComponent<Ship>().shipName);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    // TODO I would love to improve this scetion

    public void TransferWater(float amount)
    {
        TransferResource(ResourceType.water, amount);
    }

    public void TransferMetal(float amount)
    {
        TransferResource(ResourceType.metal, amount);
    }

    public void TransferOrganic(float amount)
    {
        TransferResource(ResourceType.organic, amount);
    }

    public void TransferResource(ResourceType type, float amount)
    {
        GameObject planet = associatedShip.GetComponent<Ship>().closestPlanet;
        if (planet == null)
        {
            return;
        }
        if (!planet.GetComponent<Planet>().isColonized)
        {
            return;
        }

        Storage transmitter;
        Storage reciever;
        if (amount > 0)
        {
            reciever = associatedShip.GetComponent<Storage>();
            transmitter = planet.GetComponent<Storage>();
        }
        else
        {
            transmitter  = associatedShip.GetComponent<Storage>();
            reciever = planet.GetComponent<Storage>();
            amount *= -1;
        }

        switch (type)
        {
            case ResourceType.metal:
                transmitter.transferMetal(reciever, amount);
                break;
            case ResourceType.water:
                transmitter.transferWater(reciever, amount);
                break;
            case ResourceType.organic:
                transmitter.transferOrganic(reciever, amount);
                break;
        }

        UpdateInterface();
    }



}
