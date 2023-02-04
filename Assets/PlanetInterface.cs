using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInterface : MonoBehaviour
{
    public GameObject Panel;
    
    public void openUI(){
        if (Panel != null){
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }
}
