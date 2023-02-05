using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNameGenerator 
{
    public static string GetShipName()
    {
        return RandomShipName() + " " + RandomSymbol() + RandomSymbol()+ RandomSymbol();
    }

    public static string RandomSymbol()
    {
        string symbols = "?abcdefghijklmnopqrstuvwxyz1234567890$%€";
        int num = (int) (Random.value * symbols.Length);
        return symbols[num].ToString().ToUpper();
    }

    public static string RandomShipName()
    {
        string[] names = {"Spaceship", "Explorer", "Tardis", "Entersprize", "Infinity", "Elysium", "Falcon", "Titanic", "Maryflower", "Destiny", "Victory", "Planet Express" };
        int num = (int)(Random.value * names.Length);
        return names[num];
    }
}
