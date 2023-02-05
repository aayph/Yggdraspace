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

    private AudioManager audioManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioManager = GetComponent<AudioManager>();
        InitPlayerPrefs();

        EventManager.PlanetColonizedAction += OnPlanetColonized;
        EventManager.PlanetYggdrasilationAction += OnPlanetYggralized;
        EventManager.RemainingLifetimeAction += OnTimeRemaingUpdate;
    }

    public AudioManager GetAudioManager()
    {
        return audioManager;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            LoadMainMenu();
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            activeScene = ActiveScene.MainMenu;
            EventManager.SceneChanged(ActiveScene.MainMenu);
            EventManager.GameProgressUpdated(0f);
        }
        else
        {
            GameStates.Reset();
            activeScene = ActiveScene.Game;
            EventManager.SceneChanged(ActiveScene.Game);
            EventManager.GameProgressUpdated(0f);
        }
    }

    private void Update()
    {
        if (activeScene == ActiveScene.Game)
            GameStates.gameTime += Time.deltaTime;
    }

   void OnPlanetColonized(Planet planet)
    {
        if (planet.isHome) return;
        if (GameStates.gameProgress >= 1f) return;

        float newDistance = Vector3.Distance(planet.transform.position, GameStates.yggdrasilPosition);
        float homeDistance = Vector3.Distance(GameStates.homePosition, GameStates.yggdrasilPosition);
        float newProgress = 1f - newDistance / homeDistance;
        if (GameStates.gameProgress < newProgress)
        {
            GameStates.gameProgress = newProgress;
            EventManager.GameProgressUpdated(newProgress);
        }
    }

    void OnTimeRemaingUpdate(Planet planet, float remainingTime)
    {
        if (!planet.isHome) return;
        if (planet.storage.resources.organic <= 0f)
            EndGame(false);
        if (planet.storage.resources.organic >= GameStates.targetResources)
            EndGame(true);
    }

    void EndGame(bool gameWon)
    {
        if (GameStates.gameWon) return;
        GameStates.gameWon = gameWon;
        EventManager.GameEndEvent(gameWon);
    }

    void OnPlanetYggralized(Planet planet)
    {
        if (!planet.isColonized && planet.isYggdrasized) return;

        float newDistance = Vector3.Distance(planet.transform.position, GameStates.homePosition);
        float homeDistance = Vector3.Distance(GameStates.homePosition, GameStates.yggdrasilPosition);
        float newProgress = 2f - (newDistance / homeDistance);
        if (GameStates.gameProgress < newProgress)
        {
            GameStates.gameProgress = newProgress;
            EventManager.GameProgressUpdated(newProgress);
        }
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(Instance.mainMenuScene, LoadSceneMode.Single);
        activeScene = ActiveScene.MainMenu;
        EventManager.SceneChanged(ActiveScene.MainMenu);
        EventManager.GameProgressUpdated(0f);
    }

    public static void StartNewGame()
    {
        SceneManager.LoadScene(Instance.gameScene, LoadSceneMode.Single);
        GameStates.Reset();
        activeScene = ActiveScene.Game;
        EventManager.SceneChanged(ActiveScene.Game);
        EventManager.GameProgressUpdated(0f);
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
