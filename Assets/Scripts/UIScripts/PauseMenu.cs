using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenu : MonoBehaviour
{
    CanvasGroup canvasGroup;
    SoundSettings soundSettings;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        soundSettings = GetComponentInChildren<SoundSettings>();
        DeactivateMenu();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && GameStates.gamePaused)
            ContinueGame();
        else if (Input.GetButtonDown("Cancel") && !GameStates.gamePaused &&
            !GameStates.isPlanetMenuOpen && !GameStates.isInTravelSelection)
            ActivateMenu();
    }

    public void ContinueGame()
    {
        DeactivateMenu();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        GameManager.LoadMainMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ActivateMenu()
    {
        Time.timeScale = 0;
        GameStates.gamePaused = true;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        if (soundSettings != null)
            soundSettings.UpdatePrefs();
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        GameStates.gamePaused = false;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
