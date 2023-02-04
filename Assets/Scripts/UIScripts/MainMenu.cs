using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.StartNewGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}