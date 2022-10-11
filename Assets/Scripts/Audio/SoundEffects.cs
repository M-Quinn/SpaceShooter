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

    float _maxVolume = 1.0f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        SetVolume();
    }

    private void OnEnable()
    {
        Asteroid.AsteroidExploded += AsteroidSFX;
        PlayerLaser.LaserShot += LaserSFX;
        Enemy.EnemyDied += EnemyDeathSFX;
        Health.PlayerTookDamage += HealthSFX;
        SettingsMenu.UpdateSoundEffectVolume += SetVolume;
    }
    private void OnDisable()
    {
        Asteroid.AsteroidExploded -= AsteroidSFX;
        PlayerLaser.LaserShot -= LaserSFX;
        Enemy.EnemyDied -= EnemyDeathSFX;
        Health.PlayerTookDamage -= HealthSFX;
        SettingsMenu.UpdateSoundEffectVolume -= SetVolume;
    }

    private void AsteroidSFX() {
        _audioSource.PlayOneShot(_asteroidExplosion_SFX);
    }
    private void LaserSFX() {
        _audioSource.PlayOneShot(_laser_SFX);
    }
    private void EnemyDeathSFX() {
        _audioSource.PlayOneShot(_enemyExplosion_SFX);
    }
    private void HealthSFX() {
        _audioSource.PlayOneShot(_playerDamaged_SFX);
    }

    private void SetVolume()
    {
        int volume = 100;
        if (PlayerPrefs.HasKey("SoundEffectVolume"))
            volume = PlayerPrefs.GetInt("SoundEffectVolume");
        _maxVolume = (float)volume / 100;
        _audioSource.volume = _maxVolume;
    }
}
