using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public class PlanetStructure
{
    public string identifer;
    public float finishTime;

    public PlanetStructure (Blueprint blueprint)
    {
        identifer = blueprint.identifier;
        finishTime = GameStates.gameTime + blueprint.constructionTime;
    }

    public static int GetStructureCount(PlanetStructure[] structures, string identifer)
    {
        int count = 0;
        foreach (PlanetStructure structure in structures)
            if (structure.identifer == identifer && structure.IsFinished())
                count++;
        return count;
    }

    public bool IsFinished()
    {
        return (finishTime <= GameStates.gameTime);
    }

    public static string GetStructureList(PlanetStructure[] structures)
    {
        string s = "";

        Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
        foreach (PlanetStructure structure in structures)
            if (keyValuePairs.ContainsKey(structure.identifer))
                keyValuePairs[structure.identifer]++;
            else
                keyValuePairs[structure.identifer] = 1;

        foreach (string key in keyValuePairs.Keys)
            s += key+ ": " + keyValuePairs[key].ToString()+"\n";

        return s;
    }
}
