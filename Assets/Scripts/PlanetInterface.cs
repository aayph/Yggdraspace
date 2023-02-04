using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CanvasGroup))]
public class PlanetInterface : MonoBehaviour
{
    CanvasGroup canvasGroup;
    Planet selectedPlanet;
    public TextMeshProUGUI nameField;
    public ResourceOverview resourceOverview;
    public Transform constructionItemContainer;
    public GameObject constructionItemPrefab;
    List<ConstructionItem> constructionItems;

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
        SetConstructionList();
    }

    void SetConstructionList()
    {
        if (constructionItems != null)
            foreach (ConstructionItem constructionItem in constructionItems)
                Destroy(constructionItem.gameObject);
        constructionItems = new List<ConstructionItem>();

        float count = 0;
        foreach (Blueprint blueprint in selectedPlanet.constructionList)
        {
            ConstructionItem item = Instantiate(constructionItemPrefab).GetComponent<ConstructionItem>();
            item.transform.SetParent(constructionItemContainer, false);
            item.transform.localPosition += Vector3.down * count * 100;
            item.planetUI = this;
            item.SetData(blueprint);
            constructionItems.Add(item);
            count++;
        }
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
