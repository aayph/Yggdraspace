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
    public AudioClip flySound;
    public AudioClip landSound;

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
            storage.resources.water -= EnergyConsumptionrate * Time.deltaTime;
            TravelTimer -= Time.deltaTime;
            if (TravelTimer <= 0)
            {
                FinsishTravel();
            }
        }
    }

    void OnMouseOver()
    {
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
            // TODO Pop Up Can't afford this journey
            ResetTravelValues();
            Debug.Log("Can't afford this journey");
        }
        else
        {
            // AUDIO
            audioSource.clip = flySound;
            audioSource.loop = true;
            audioSource.Play();
        }


    }

    private void FinsishTravel()
    {
        if (closestPlanet != null)
        {
            // WIP
            Storage planetStorage = closestPlanet.GetComponentInChildren<Storage>();
            storage.transferAllResources(planetStorage);
        }
        ResetTravelValues();
        audioSource.loop = false;
        audioSource.Stop();
        audioSource.clip = landSound;
        audioSource.Play();
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
