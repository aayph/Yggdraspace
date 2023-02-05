using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip newProgressUpdateSound;
    public AudioClip BG_Music;
    private AudioSource audioSource;

    public AudioSource musicLayer1;
    public AudioSource musicLayer2;


    private int currentProgressState = 0;

    public AudioMixer mixer;
    public AudioMixerSnapshot snapshotDanger1;
    public AudioMixerSnapshot snapshotDanger2;
    public AudioMixerSnapshot snapshotDanger3;

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



    
    private void OnDangerLevelUpdate(Planet planet, int dangerLevel)
    {
        if (planet == null || !planet.isHome) return;

        if (dangerLevel.Equals(0))
        {
            StartCoroutine(FadeOutAudioSource(10, musicLayer2));
            StartCoroutine(FadeInAudioSource(10, musicLayer1));
            snapshotDanger1.TransitionTo(10);
        }
        else if (dangerLevel.Equals(1))
        {
            StartCoroutine(FadeOutAudioSource(15, musicLayer1));
            StartCoroutine(FadeInAudioSource(15, musicLayer2));
            snapshotDanger2.TransitionTo(15);
        }
        else if (dangerLevel.Equals(2))
        {
            snapshotDanger3.TransitionTo(30);
        }
    }
    
    private void OnGameProgressUpdate(float gameProgress)
    {
        /*
        if (gameProgress > 0.25f && currentProgressState.Equals(0))
        {
            currentProgressState = 1;
            StartCoroutine(FadeOutAudioSource(  1,    musicLayer1));
            StartCoroutine(FadeInAudioSource(   1,    musicLayer2));
        }
        else if (gameProgress > 0.5f && currentProgressState.Equals(1))
        {
            currentProgressState = 2;
            StartCoroutine(FadeOutAudioSource(1, musicLayer2));
            StartCoroutine(FadeInAudioSource(1, musicLayer3));
        }
        else if (gameProgress > 0.75f && currentProgressState.Equals(2))
        {
            currentProgressState = 3;
            StartCoroutine(FadeOutAudioSource(1, musicLayer3));
            StartCoroutine(FadeInAudioSource(1, musicLayer4));
        }
  */
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
       //musicSource1.Play();
    }

    public void StopBackgroundMusic()
    {
       // musicSource1.Play();
    }

    private void ShipStartedTraveling(Vector3 destination)
    {
        // PLAY START SOUND
    }

    private void DiscoverPlanet(Planet planet)
    {
        if (GameStates.gameTime <= 1f) return;
        audioSource.clip = newProgressUpdateSound;
        audioSource.volume = 1;// Random.Range(0.8f, 1);
        audioSource.pitch = 1;// Random.Range(0.9f, 1.1f);
        audioSource.Play();
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
        EventManager.PlanetExploreAction += DiscoverPlanet;
    }

    private void OnDestroy()
    {
        EventManager.ShipAction -= OnShipClicked;
        EventManager.PlanetAction -= OnPlanetClicked;
        EventManager.GameProgressUpdate -= OnGameProgressUpdate;
        EventManager.DangerLevelUpdate -= OnDangerLevelUpdate;
        EventManager.ShipTravelAction -= ShipStartedTraveling;
        EventManager.PlanetExploreAction -= DiscoverPlanet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
