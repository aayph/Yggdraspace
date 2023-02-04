using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionItem : MonoBehaviour
{
    public PlanetInterface planetUI;
    public Blueprint blueprint;
    public Button constructButton;
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI organicNumberField;
    public TextMeshProUGUI metalNumberField;
    public TextMeshProUGUI waterNumberField;

    private void Start()
    {
        if (blueprint!= null)
        {
            SetData(blueprint);
        }
    }

    public void SetData(Blueprint blueprint)
    {
        this.blueprint = blueprint;
        nameField.text = blueprint.identifier;
        organicNumberField.text = blueprint.costs.organic.ToString();
        metalNumberField.text = blueprint.costs.metal.ToString();
        waterNumberField.text = blueprint.costs.water.ToString();
    }

    public void UpdateStates(Planet planetState)
    {
        constructButton.enabled = true;
        organicNumberField.color = Color.white;
        metalNumberField.color = Color.white;
        waterNumberField.color = Color.white;

        if (!string.IsNullOrEmpty(blueprint.requiredStructure))
            constructButton.interactable = (PlanetStructure.GetStructureCount(planetState.structures.ToArray(), blueprint.requiredStructure) >= 1);

        if (planetState.storage.resources.organic < blueprint.costs.organic)
        {
            constructButton.interactable = false;
            organicNumberField.color = Color.red;
        }

        if (planetState.storage.resources.metal < blueprint.costs.metal)
        {
            constructButton.interactable = false;
            metalNumberField.color = Color.red;
        }

        if (planetState.storage.resources.water < blueprint.costs.water)
        {
            constructButton.interactable = false;
            waterNumberField.color = Color.red;
        }
    }


    public void Construct()
    {
        planetUI.Construct(blueprint);
    }
}
