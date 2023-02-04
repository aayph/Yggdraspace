using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Storage), typeof(RessourceReducer))]
public class Planet : MonoBehaviour
{
    public Blueprint[] constructionList;
    public Storage storage;
    public bool isDead = false;
    RessourceReducer reducer;

    public List<PlanetStructure> structures;
    public string planetName;

    private void Start()
    {
        storage = GetComponent<Storage>();
        reducer = GetComponent<RessourceReducer>();
    }

    private void Update()
    {
        isDead = (storage.resources.organic <= 0);
    }

    private void OnMouseDown()
    {
        if (!isDead)
            EventManager.PlanetClickedEvent(this);
    }
    private void OnMouseEnter()
    {
        EventManager.TooltipEvent(planetName, PlanetStructure.GetStructureList(structures.ToArray()), storage.resources);
    }

    private void OnMouseExit()
    {
        EventManager.TooltipEvent("", "", null);
    }

    public void Construct(Blueprint blueprint)
    {
        storage.resources -= blueprint.costs;
        if (blueprint.isBuilding)
        {
            structures.Add(new PlanetStructure(blueprint));
            reducer.transformer.Add(blueprint.perSecondChange);
        } else
        {
            GameObject newObject = Instantiate(blueprint.prefab);
            newObject.transform.position = transform.position + Vector3.up;
        }
    }
}
