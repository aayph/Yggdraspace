using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip newProgressUpdateSound;
    public AudioClip BG_Music;
    private AudioSource audioSource;

    public AudioSource musicSource1;
    public AudioSource baseTrack;
    public AudioSource musicLayer1;
    public AudioSource musicLayer2;
    public AudioSource musicLayer3;
    public AudioSource musicLayer4;
    public AudioSource musicLayerVariations;

    private int currentProgressState = 0;


    private IEnumerator FadeInAudioSource(float fadeTime, AudioSource source)
    {
        source.volume = 0;
        for (int i = 0; i<100; i++)
        {
            source.volume += 0.01f;
            yield return new WaitForSeconds(fadeTime / 100);
        }
    }

    private IEnumerator FadeOutAudioSource(float fadeTime, AudioSource source)
    {
        source.volume = 1;
        for (int i = 0; i < 100; i++)
        {
            source.volume -= 0.01f;
            yield return new WaitForSeconds(fadeTime / 100);
        }
    }

    private void OnDangerLevelUpdate(float dangerLevel)
    {

    }
    
    private void OnGameProgressUpdate(float gameProgress)
    {
        if (gameProgress > 0.2f && currentProgressState.Equals(0))
        {
            currentProgressState = 1;
        }
        else if (gameProgress > 0.4f && currentProgressState.Equals(1))
        {
            currentProgressState = 2;
        }
        else if (gameProgress > 0.6f && currentProgressState.Equals(2))
        {
            currentProgressState = 3;
        }
        else if (gameProgress > 0.8f && currentProgressState.Equals(3))
        {
            currentProgressState = 4;
        }
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

    private void ShipStartedTraveling(Vector3 destination)
    {
        // PLAY START SOUND
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        EventManager.ShipAction += OnShipClicked;
        EventManager.PlanetAction += OnPlanetClicked;
        EventManager.GameProgressUpdate += OnGameProgressUpdate;
        EventManager.DangerLevelUpdate += OnDangerLevelUpdate;
        EventManager.ShipTravelAction += ShipStartedTraveling;
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
