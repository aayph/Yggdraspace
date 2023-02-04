using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Blueprint", menuName = "Data/Blueprint", order = 1)]
public class Blueprint : ScriptableObject
{
    public string identifier;
    public GameObject prefab;
    public Resources costs;
    public bool isBuilding;
    public float constructionTime;
    public string requiredStructure;
}