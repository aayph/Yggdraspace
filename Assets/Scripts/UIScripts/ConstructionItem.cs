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
    public CanvasGroup errorPanel;
    public TextMeshProUGUI errorMessage;


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

    string GetCountString(int buildingCount)
    {
        if (blueprint.planetLimit > 0)
            return " (" + buildingCount.ToString() + "/" + blueprint.planetLimit.ToString() + ")";
        else
            return " (" + buildingCount.ToString() + ")";
    }

    public void UpdateStates(Planet planetState)
    {
        bool disableButton = false;
        organicNumberField.color = Color.white;
        metalNumberField.color = Color.white;
        waterNumberField.color = Color.white;
        errorPanel.alpha = 0f;

        int buildingCount = PlanetStructure.GetStructureCount(planetState.structures.ToArray(), blueprint.identifier);

        if (!string.IsNullOrEmpty(blueprint.requiredStructure))
        {
            if (PlanetStructure.GetStructureCount(planetState.structures.ToArray(), blueprint.requiredStructure) < 1)
            {
                disableButton = true;
                errorMessage.text = "Requires Building: " + blueprint.requiredStructure;
                errorPanel.alpha = 1f;
            }
        }

        if (blueprint.planetLimit > 0 && buildingCount >= blueprint.planetLimit)
        {
            disableButton = true;
            errorMessage.text = "Construction Limit Reached";
            errorPanel.alpha = 1f;
        }

        if (planetState.storage.resources.organic < blueprint.costs.organic)
        {
            disableButton = true;
            organicNumberField.color = Color.red;
            errorMessage.text = "Not Enough Materials!";
        }

        if (planetState.storage.resources.metal < blueprint.costs.metal)
        {
            disableButton = true;
            metalNumberField.color = Color.red;
            errorMessage.text = "Not Enough Materials!";
        }

        if (planetState.storage.resources.water < blueprint.costs.water)
        {
            disableButton = true;
            waterNumberField.color = Color.red;
            errorMessage.text = "Not Enough Materials!";
        }

        if (blueprint.isBuilding)
            nameField.text = blueprint.identifier + GetCountString(buildingCount);

        if (disableButton == constructButton.interactable)
            constructButton.interactable = !disableButton;
    }


    public void Construct()
    {
        planetUI.Construct(blueprint);
    }
}
