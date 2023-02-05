using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Storage), typeof(RessourceReducer))]
public class Planet : MonoBehaviour
{
    public string planetName;
    public Blueprint[] constructionList;
    [TextArea] public string planetDescription;

    [Space]
    [Header ("States")]
    public bool isHome = false;
    public bool isExplored = false;
    public bool isColonized = false;
    public bool isYggdrasized = false;
    public bool isDead = false;

    [Space]
    [Header ("Components & Data | Automatic")]
    public Storage storage;
    public RessourceReducer reducer;
    public List<PlanetStructure> structures;

    int currentDangerLevel = 0;

    private void Start()
    {
        storage = GetComponent<Storage>();
        reducer = GetComponent<RessourceReducer>();
    }

    private void Update()
    {
        isDead = (storage.resources.organic <= 0);
        EventManager.RemainingLifetimeEvent(this, CheckDangerLevel());
    }

    private void OnMouseDown()
    {
        if (!isDead && isColonized)
            EventManager.PlanetClickedEvent(this);
    }
    private void OnMouseEnter()
    {
        string structure_list = PlanetStructure.GetStructureList(structures.ToArray());
        if (structure_list == "") structure_list = "No buildings have been constructed.";

        if (!isExplored && !isColonized)
            EventManager.TooltipEvent("Unknown Planet", planetDescription, null);
        else if (!isColonized)
            EventManager.TooltipEvent("Unhabitated Planet", planetDescription, storage.resources);
        else if (!isDead)
            EventManager.TooltipEvent(planetName, "Structures:\n" + structure_list, storage.resources);
        else
            EventManager.TooltipEvent(planetName + " (DEAD)", "Structures:\n" + structure_list, storage.resources);
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

    public float CheckDangerLevel()
    {

        float remainingLifeTime = reducer.RemainingLifeTime();
        for (int n = GameRule.DangerLevels.Length - 1; n >= 0; n--)
        {
            if (remainingLifeTime < GameRule.DangerLevels[n])
            {
                if (currentDangerLevel != n + 1)
                {
                    currentDangerLevel = n + 1;
                    EventManager.DangerLevelUpdated(this, currentDangerLevel);
                }
                return remainingLifeTime;
            }
        }
        if (currentDangerLevel != 0)
        {
            currentDangerLevel = 0;
            EventManager.DangerLevelUpdated(this, currentDangerLevel);
        }
        return remainingLifeTime;
    }
}
