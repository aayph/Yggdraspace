using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Storage), typeof(RessourceReducer))]
public class Planet : MonoBehaviour
{
    public Blueprint[] constructionList;
    public Storage storage;
    RessourceReducer reducer;

    public List<PlanetStructure> structures;
    public string planetName;

    private void Start()
    {
        storage = GetComponent<Storage>();
        reducer = GetComponent<RessourceReducer>();
    }

    private void OnMouseDown()
    {
        EventManager.PlanetClickedEvent(this);
    }
    private void OnMouseEnter()
    {
        EventManager.PlanetHoverEvent(this,true);
    }
    private void OnMouseExit(){
        EventManager.PlanetHoverEvent(this,false);
    }

    public void Construct(Blueprint blueprint)
    {
        storage.resources -= blueprint.costs;
        if (blueprint.isBuilding)
        {
            structures.Add(new PlanetStructure(blueprint));
        } else
        {
            GameObject newObject = Instantiate(blueprint.prefab);
            newObject.transform.position = transform.position + Vector3.up;
        }
    }
}
