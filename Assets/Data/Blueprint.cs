using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "Blueprint", menuName = "Data/Blueprint", order = 1)]
public class Blueprint : ScriptableObject
{
    public bool isBuilding = true;
    public int planetLimit = 0;
    [Space]
    public string identifier;
    public string requiredStructure;
    [TextArea] public string description;
    [Space]
    public GameObject prefab;
    public Resources costs;
    public float constructionTime = 0f;
    [Space]
    public Resources perSecondChange;
}