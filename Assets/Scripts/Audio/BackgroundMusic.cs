using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField]
    AudioClip[] _backgroundMusic;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
        if (_backgroundMusic.Length <= 1) {
            _audioSource.loop = true;
        }
        if (_backgroundMusic.Length >= 1) {
            _audioSource.clip = _backgroundMusic[0];
            _audioSource.Play();
        }
        else
        {
            Debug.LogError("Background Music is missing");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn(5));
    }

    IEnumerator FadeIn(float delay) {
        var timer = Time.time + delay;
        while (Time.time < timer) {
            _audioSource.volume = 1 - ((timer - Time.time) / timer);
            yield return null;
        }
    }

    IEnumerator FadeOut(float delay) {
        var timer = Time.time + delay;
        while (Time.time < timer)
        {
            _audioSource.volume = ((timer - Time.time) / timer);
            yield return null;
        }
    }

    public void StartFadeOut(float delay) {
        StartCoroutine(FadeOut(delay));
    }
}
