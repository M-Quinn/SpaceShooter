using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField]
    AudioClip[] _backgroundMusic;
    int _currentSongIndex;

    float _maxVolume = 1.0f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
        if (_backgroundMusic.Length <= 1) {
            _audioSource.loop = true;
        }
        if (_backgroundMusic.Length >= 1) {
            _currentSongIndex = Random.Range(0, _backgroundMusic.Length);
            StartCoroutine(PlayBackgroundMusic());
        }
        else
        {
            Debug.LogError("Background Music is missing");
        }
        SetVolume();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn(5));
    }
    private void OnEnable() {
        SettingsMenu.UpdateMusicVolume += SetVolume;
    }
    private void OnDisable()
    {
        SettingsMenu.UpdateMusicVolume -= SetVolume;
    }


    IEnumerator FadeIn(float delay) {
        var timer = Time.time + delay;
        while (Time.time < timer) {
            _audioSource.volume = Mathf.Clamp(1 - ((timer - Time.time) / timer),0,_maxVolume);
            yield return null;
        }
    }

    IEnumerator FadeOut(float delay) {
        var timer = Time.time + delay;
        while (Time.time < timer)
        {
            Debug.Log(timer/Time.time);
            Mathf.Clamp(_audioSource.volume = (timer / Time.time)-1,0,_maxVolume);
            yield return null;
        }
    }
    IEnumerator PlayBackgroundMusic() {
        var audioClip = GetNextSong();
        _audioSource.PlayOneShot(audioClip);
        while (_audioSource.isPlaying) {
            yield return null;
        }
        StartCoroutine(PlayBackgroundMusic());
    }
    private AudioClip GetNextSong() {
        _currentSongIndex = NextSong(_currentSongIndex);
        if (_backgroundMusic[_currentSongIndex] != null)
            return _backgroundMusic[_currentSongIndex];
        else {
            Debug.LogError("NextSong Failed");
            return _backgroundMusic[0];
        }
    }

    private int NextSong(int songIndex) {
        songIndex++;
        if (songIndex >= _backgroundMusic.Length)
            songIndex = 0;
        return songIndex;
    }
    private void SetVolume()
    {
        int volume = 100;
        if (PlayerPrefs.HasKey("MusicVolume"))
            volume = PlayerPrefs.GetInt("MusicVolume");
        _maxVolume = (float)volume / 100;
        _audioSource.volume = _maxVolume;
    }

    public void StartFadeOut(float delay) {
        StartCoroutine(FadeOut(delay));
    }
}
