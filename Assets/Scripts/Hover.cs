using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class Hover : MonoBehaviour 
{
	CanvasGroup canvasGroup;
    Planet selectedPlanet;
    public TextMeshProUGUI[] resourcesTexts;
    private void Update(){
        if(canvasGroup.alpha == 1)UpdateText();
    }
    private void Awake()
    {
        EventManager.PlanetHoverAction += PlanetHovered;
        canvasGroup = GetComponent<CanvasGroup>();
        DeactivateMenu();
    }

    private void OnDestroy()
    {
        EventManager.PlanetHoverAction -= PlanetHovered;
    }


    public void PlanetHovered(Planet planet,bool isOpened)
    {
    if(isOpened){
        AcivateMenu();
        selectedPlanet = planet;}
     
    else{
        DeactivateMenu();
    }}
    private void UpdateText(){ 
    resourcesTexts[0].SetText("Name:"+ selectedPlanet.planetName);
        resourcesTexts[1].SetText("Brokkoli:"+ Math.Round(selectedPlanet.storage.resources.organic,2));
        resourcesTexts[2].SetText("Wasser:"+ Math.Round(selectedPlanet.storage.resources.water,2));
        resourcesTexts[3].SetText("Metall:"+ Math.Round(selectedPlanet.storage.resources.metal,2));
    }

    public void AcivateMenu()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DeactivateMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

}
