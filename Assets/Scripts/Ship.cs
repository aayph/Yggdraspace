using UnityEngine;

public class Ship : MonoBehaviour
{
    public Storage storage;

    // Ship Statsa
    public float EnergyConsumptionrate = 1;
    public float TravelSpeed = 1;

    // For Traveling
    private bool IsTraveling;
    private float TravelTimer;
    private Vector3 travelDirection;

    // For Orbiting
    public GameObject closestPlanet;

    // AUDIO
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        storage = gameObject.GetComponentInChildren<Storage>();
        EventManager.ShipTravelAction += TravelToTarget;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        EventManager.ShipTravelAction -= TravelToTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTraveling)
        {
            transform.Rotate(0, Vector3.Angle(travelDirection, transform.forward), 0, Space.Self);
            transform.position = transform.position + travelDirection * TravelSpeed * Time.deltaTime;
            if (GameRule.TravelIsContinous)
            {
                storage.resources.water -= EnergyConsumptionrate * Time.deltaTime;
            }
            TravelTimer -= Time.deltaTime;
            if (TravelTimer <= 0)
            {
                FinsishTravel();
            }
        }
    }

    void OnMouseOver()
    {
        if (!GameRule.TravelIsContinous && IsTraveling)
        { 
            return; 
        }
        if (Input.GetMouseButtonDown(0) && !GameStates.isInTravelSelection)
        {
            EventManager.OnShipClickEvent(gameObject);
        }
    }

    public void TravelToTarget(Vector3 target)
    {
        if (gameObject != GameStates.traveler)
        {
            return;
        }

        // Deactivate the GameState as the Command has been send
        GameStates.isInTravelSelection = false;
        GameStates.traveler = null;

        // Set some later used values
        target = new Vector3(target.x, 1, target.z);
        float distance = Vector3.Distance(target, transform.position);

        // Set Travel Values
        IsTraveling = true;
        travelDirection = (target - transform.position).normalized; 
        TravelTimer = distance / TravelSpeed;

        // Do not travel if it would cost more energy then stored
        if (TravelTimer * EnergyConsumptionrate > storage.resources.water)
        {
            ResetTravelValues();
            EventManager.OpenTooExpensivePopUp();
            return;
        }
        if (!GameRule.TravelIsContinous)
        {
            storage.resources.water -= TravelTimer * EnergyConsumptionrate;
        }

        // AUDIO
        audioSource.loop = true;
        audioSource.Play();
    }

    private void FinsishTravel()
    {
        if (closestPlanet != null)
        {
            Storage planetStorage = closestPlanet.GetComponentInChildren<Storage>();
            switch (GameRule.depletionDuringLanding)
            {
                case GameRule.DepletionOptions.All:
                    storage.transferAllResources(planetStorage);
                    break;
                case GameRule.DepletionOptions.None:
                    break;
                case GameRule.DepletionOptions.AllButFuel:
                    storage.transferMetal(planetStorage, storage.resources.metal);
                    storage.transferOrganic(planetStorage, storage.resources.organic);
                    break;
            }            
        }
        ResetTravelValues();
        audioSource.loop = false;
        audioSource.Stop();
    }

    private void ResetTravelValues()
    {
        IsTraveling = false;
        TravelTimer = 0;
        travelDirection = Vector3.zero;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Planet")
        {
            closestPlanet = collider.gameObject;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Planet")
        {
            if (closestPlanet == gameObject)
            {
                closestPlanet = null;
            }            
        }
    }

}
