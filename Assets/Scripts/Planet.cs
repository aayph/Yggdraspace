using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Storage), typeof(RessourceReducer))]
public class Planet : MonoBehaviour
{
    Storage storage;
    RessourceReducer reducer;

    public PlanetStructure structures;
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
}
