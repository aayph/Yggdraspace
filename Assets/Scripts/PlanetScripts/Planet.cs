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

    [Space]
    public GameObject deadObject;
    public GameObject settledObject;

    int currentDangerLevel = 0;

    private void Awake()
    {
        if (isHome)
            GameStates.homePosition = transform.position;
        if (isYggdrasized && !isColonized)
            GameStates.yggdrasilPosition = transform.position;

        EventManager.PlanetExploreAction += OnExplore;
        EventManager.PlanetColonizedAction += OnColonize;
        EventManager.PlanetYggdrasilationAction += OnYggdralized;
    }

    private void OnDestroy()
    {
        EventManager.PlanetExploreAction -= OnExplore;
        EventManager.PlanetColonizedAction -= OnColonize;
        EventManager.PlanetYggdrasilationAction -= OnYggdralized;
    }

    private void Start()
    {
        storage = GetComponent<Storage>();
        reducer = GetComponent<RessourceReducer>();
        if (isExplored)
            EventManager.PlanetExploreEvent(this);
        if (isColonized)
            EventManager.PlanetColonizedEvent(this);
        if (isYggdrasized)
            reducer.endlessFood = true;
    }

    private void Update()
    {
        bool newDeadState = reducer.IsDead();
        if (isDead != newDeadState)
        {
            isDead = newDeadState;
            EventManager.PlanetDeadEvent(this, newDeadState);
            if (deadObject != null)
                deadObject.SetActive(isDead);
        }
        EventManager.RemainingLifetimeEvent(this, CheckDangerLevel());
    }

    private void OnExplore(Planet planet)
    {
        if (planet != this) return;
        isExplored = true;
    }

    private void OnColonize(Planet planet)
    {
        if (planet != this) return;
        isColonized = true;
        if (settledObject != null)
            settledObject.SetActive(true);
    }

    private void OnYggdralized(Planet planet)
    {
        if (planet != this) return;
        isYggdrasized = true;
        reducer.endlessFood = true;
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
