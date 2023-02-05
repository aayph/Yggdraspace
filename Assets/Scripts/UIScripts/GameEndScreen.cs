using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GameEndScreen : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public TextMeshProUGUI stateMessage;
    public Button continueButton;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        EventManager.GameEndAction += ActivateMenu;
        DeactivateMenu();
    }


    private void OnDestroy()
    {
        EventManager.GameEndAction -= ActivateMenu;
    }


    public void ContinueGame()
    {
        DeactivateMenu();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        GameManager.StartNewGame();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        GameManager.LoadMainMenu();
    }

    public void ActivateMenu(bool playerWon)
    {
        if (playerWon)
        {
            stateMessage.text = "~* YOU WON! *~";
            continueButton.gameObject.SetActive(true);
        } else
        {
            stateMessage.text = "💀 Game Over 💀";
            continueButton.gameObject.SetActive(false);
        }

        Time.timeScale = 0;
        GameStates.gamePaused = true;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
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
