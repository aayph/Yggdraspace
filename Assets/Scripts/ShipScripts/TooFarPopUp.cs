using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooFarPopUp : MonoBehaviour
{
    private float Timer=1.5f;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.ShipTravelTooFar += PopUp;
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        EventManager.ShipTravelTooFar -= PopUp;
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void PopUp()
    {
        gameObject.SetActive(true);
        Timer += 2f;
    }
}
