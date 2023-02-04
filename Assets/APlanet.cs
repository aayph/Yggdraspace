using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APlanet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Timer._onTick += delegate (object sender,Timer.OntickEvent e){
         };
    }

    // Update is called once per frame
    void Update()
    {
        //not used atm;
    }
}
