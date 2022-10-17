using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour
{
    [SerializeField] SpriteRenderer _playerSprite;
    [SerializeField] GameObject[] _damageVFXs;
    int _maxLives = 3;
    int _lives = 3;
    bool _isAboutToDie;

    private void Start()
    {
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager == null)
            Debug.LogError($"{this.name} Couldn't find the GameManager");
        else {
            if (gameManager.GetGameType() == GameType.pacifist) { 
                _maxLives = 1;
                _lives = 1;
                PlayerIsAboutToDie(true);
            }
        }

    }

    public void UpdateHealth(int amount) {
        if (amount < 0)
            EventManager.PlayerTookDamage?.Invoke();
        _lives = Mathf.Clamp(_lives += amount, 0, _maxLives);
        _damageVFXs[Random.Range(0, 2)].SetActive(true);
        if (_isAboutToDie && _lives > 1) {
            PlayerIsAboutToDie(false);
        }
        if (_lives == 1)
        {
            PlayerIsAboutToDie(true);
        }
        else if (_lives == 0) {
            EventManager.PlayerDied?.Invoke();
            Destroy(gameObject);
        }
    }
    public void PlayerIsAboutToDie(bool isAboutToDie) {
        foreach (GameObject go in _damageVFXs) {
            go.SetActive(true);
        }
        _isAboutToDie = isAboutToDie;
        if (_isAboutToDie)
            StartCoroutine(RedFlash());
    }
    IEnumerator RedFlash() {
        while (_isAboutToDie) {
            _playerSprite.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));
            yield return null;
        }
    }
}
