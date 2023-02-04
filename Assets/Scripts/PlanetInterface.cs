using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CanvasGroup))]
public class PlanetInterface : MonoBehaviour
{
    CanvasGroup canvasGroup;
    Planet selectedPlanet;
    public TextMeshProUGUI nameField;
    public ConstructionItem[] constructionItems;
    public ResourceOverview resourceOverview;

    private void Awake()
    {
        EventManager.PlanetAction += PlanetClicked;
        canvasGroup = GetComponent<CanvasGroup>();
        DeactivateMenu();
    }

    private void OnDestroy()
    {
        EventManager.PlanetAction -= PlanetClicked;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && GameStates.isPlanetMenuOpen)
            DeactivateMenu();
        if (!GameStates.isPlanetMenuOpen || selectedPlanet == null) return;
        foreach (ConstructionItem constructionItem in constructionItems)
            constructionItem.UpdateStates(selectedPlanet);
        resourceOverview.UpdateNumbers(selectedPlanet.storage.resources);
    }

    public void PlanetClicked(Planet planet)
    {
        if (GameStates.isInTravelSelection) return;
        AcivateMenu();

        selectedPlanet = planet;
        nameField.text = selectedPlanet.planetName;
        resourceOverview.UpdateNumbers(selectedPlanet.storage.resources);
    }

    public void AcivateMenu()
    {
        GameStates.isPlanetMenuOpen = true;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DeactivateMenu()
    {
        GameStates.isPlanetMenuOpen = false;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Construct(Blueprint blueprint)
    {
        selectedPlanet.Construct(blueprint);
    }
}
