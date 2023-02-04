using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip BG_Music;
    private AudioSource audioSource;

    public AudioSource musicSource1;
    
    private void OnDangerLevelUpdate(int dangerLevel)
    {

    }
    
    private void OnGameProgressUpdate(float gameProgress)
    {

    }

    private void OnShipClicked(GameObject ship)
    {
        PlayClickSound();
    }

    private void OnPlanetClicked(Planet planet)
    {
        PlayClickSound();
    }

    public void PlayClickSound()
    {
        audioSource.clip = clickSound;
        audioSource.volume = Random.Range(0.8f, 1);
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }

    public void PlayBackgroundMusic()
    {
        musicSource1.Play();
    }

    public void StopBackgroundMusic()
    {
        musicSource1.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        EventManager.ShipAction += OnShipClicked;
        EventManager.PlanetAction += OnPlanetClicked;
        EventManager.GameProgressUpdate += OnGameProgressUpdate;
        EventManager.DangerLevelUpdate += OnDangerLevelUpdate;
    }

    private void OnDestroy()
    {
        EventManager.ShipAction -= OnShipClicked;
        EventManager.PlanetAction -= OnPlanetClicked;
        EventManager.GameProgressUpdate -= OnGameProgressUpdate;
        EventManager.DangerLevelUpdate -= OnDangerLevelUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
