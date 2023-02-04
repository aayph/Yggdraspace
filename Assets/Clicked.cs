using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicked : MonoBehaviour
{
    public APlanet  APlanet;

    void MouseDown(){
        APlanet.openUI();
    }
}
