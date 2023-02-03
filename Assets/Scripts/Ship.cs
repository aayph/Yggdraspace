using UnityEngine;

public class Ship : MonoBehaviour
{
    public Storage storage;

    // Start is called before the first frame update
    void Start()
    {
        storage = gameObject.GetComponentInChildren<Storage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.OnShipClickEvent(gameObject);
        }
    }

}
