using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn(2));
    }

    IEnumerator FadeIn(float delay) {
        var timer = Time.time + delay;
        while (Time.time < timer) {
            _audioSource.volume = 1 - ((timer - Time.time) / timer);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
