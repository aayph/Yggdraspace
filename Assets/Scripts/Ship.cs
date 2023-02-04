using UnityEngine;

public class Ship : MonoBehaviour
{
    public Storage storage;

    // Start is called before the first frame update
    void Start()
    {
        storage = gameObject.GetComponentInChildren<Storage>();
        EventManager.ShipTravelAction += TravelToTarget;
    }

    private void OnDestroy()
    {
        EventManager.ShipTravelAction -= TravelToTarget;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO remove pseudo Code
        if (Input.GetKey(KeyCode.Space) == true)
        {
            EventManager.TravelTargetSelected(new Vector3());
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

        // ToDo Not instant Travel
        transform.position = target;
    }

}
