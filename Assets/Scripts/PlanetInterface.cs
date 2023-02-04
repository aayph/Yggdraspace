using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PlanetInterface : MonoBehaviour
{
    CanvasGroup canvasGroup;
    Planet selectedPlanet;

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
    }

    public void PlanetClicked(Planet planet)
    {
        if (GameStates.isInTravelSelection) return;
        AcivateMenu();
        selectedPlanet = planet;
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
}
