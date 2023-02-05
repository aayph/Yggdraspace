using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI gameTimeField;
    public TextMeshProUGUI remainingTime;
    public TextMeshProUGUI progressField;
    public ResourceOverview allPlanets;

    float timeAnimation = 0f;
    float animationTimeFactor = 2f;
    Color animationColorA = new Color(1, 1, 1, 1.0f);
    Color animationColorB = new Color(1, 1, 1, 0.2f);

    public List<Planet> colonies;


    private void Awake()
    {
        colonies = new List<Planet>();
        EventManager.RemainingLifetimeAction += UpdateRemainingTime;
        EventManager.PlanetColonizedAction += AddPlanet;
        EventManager.DangerLevelUpdate += UpdateDangerLevel;
    }

    private void OnDestroy()
    {
        EventManager.RemainingLifetimeAction -= UpdateRemainingTime;
        EventManager.PlanetColonizedAction -= AddPlanet;
        EventManager.DangerLevelUpdate -= UpdateDangerLevel;
    }

    void Update()
    {
        gameTimeField.text = String.Format("Played for {0:00}:{1:00}", Mathf.FloorToInt(GameStates.gameTime) / 60, Mathf.FloorToInt(GameStates.gameTime) % 60);
        AnimateTimer();

        Resources res = new Resources();
        foreach (Planet planet in colonies)
            res += planet.storage.resources;
        allPlanets.UpdateNumbers(res);
    }

    void UpdateRemainingTime(Planet planet, float time)
    {
        if (!planet.isHome) return;
        remainingTime.text = String.Format("Food left for {0:00}:{1:00}", Mathf.FloorToInt(time) / 60, Mathf.FloorToInt(time) % 60);

        progressField.text = String.Format("{0:0} / {1:0} ({2:0.00}%)", planet.storage.resources.organic, GameStates.targetResources, planet.storage.resources.organic / GameStates.targetResources);
    }

    void AddPlanet(Planet planet)
    {
        if (planet.isHome) return;
        colonies.Add(planet);
    }

    void UpdateDangerLevel(Planet planet, int level)
    {
        if (!planet.isHome) return;
        switch (level)
        {
            case 0:
                animationColorA = new Color(1, 1, 1, 1.0f);
                animationColorB = new Color(1, 1, 1, 0.5f);
                animationTimeFactor = 5f;
                break;
            case 1:
                animationColorA = new Color(1f, 0.9f, 0.1f, 1.0f);
                animationColorB = new Color(0.9f, 0.5f, 0.0f, 0.4f);
                animationTimeFactor = 3f;
                break;
            case 2:
                animationColorA = new Color(0.9f, 0.2f, 0.2f, 1.0f);
                animationColorB = new Color(0.9f, 0f, 0f, 0.3f);
                animationTimeFactor = 1.5f;
                break;
        }
    }

    void AnimateTimer()
    {
        timeAnimation += Time.deltaTime / animationTimeFactor;
        if (timeAnimation >= 2f) timeAnimation -= 2f;
        if (timeAnimation < 1f)
            remainingTime.color = Color.Lerp(animationColorA, animationColorB, timeAnimation);
        else
            remainingTime.color = Color.Lerp(animationColorB, animationColorA, timeAnimation-1f);
    }
}
