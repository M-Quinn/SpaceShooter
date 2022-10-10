using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffects : MonoBehaviour
{
    AudioSource _audioSource;
    [Header("Sound Effects")]
    [SerializeField] AudioClip _laser_SFX;
    [SerializeField] AudioClip _playerDamaged_SFX;
    [SerializeField] AudioClip _enemyExplosion_SFX;
    [SerializeField] AudioClip _asteroidExplosion_SFX;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
}
