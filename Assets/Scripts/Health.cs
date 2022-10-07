using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] SpriteRenderer _playerSprite;
    int _maxLives = 3;
    int _lives = 3;
    bool _isAboutToDie;

    public void UpdateHealth(int amount) {
        _lives = Mathf.Clamp(_lives += amount, 0, _maxLives);
        if (_isAboutToDie && _lives > 1) {
            PlayerIsAboutToDie(false);
        }
        if (_lives == 1)
        {
            PlayerIsAboutToDie(true);
        }
        else if (_lives == 0) {
            Player.PlayerDied?.Invoke();
            Destroy(gameObject);
        }
    }
    public void PlayerIsAboutToDie(bool isAboutToDie) {
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
