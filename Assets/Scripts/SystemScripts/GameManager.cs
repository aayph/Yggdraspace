using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum ActiveScene { Startup, MainMenu, Game }

    public static GameManager Instance;
    public static ActiveScene activeScene;

    public string mainMenuScene;
    public string gameScene;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitPlayerPrefs();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            LoadMainMenu();
    }

    private void Update()
    {
        if (activeScene == ActiveScene.Game)
            GameStates.gameTime += Time.deltaTime;
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(Instance.mainMenuScene, LoadSceneMode.Single);
        activeScene = ActiveScene.MainMenu;
    }

    public static void StartNewGame()
    {
        SceneManager.LoadScene(Instance.gameScene, LoadSceneMode.Single);
        GameStates.Reset();
        activeScene = ActiveScene.Game;
    }

    public static void InitPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("music_volume"))
            PlayerPrefs.SetFloat("music_volume", 0.5f);
        if (!PlayerPrefs.HasKey("sound_volume"))
            PlayerPrefs.SetFloat("sound_volume", 0.5f);
        PlayerPrefs.Save();
    }


}
