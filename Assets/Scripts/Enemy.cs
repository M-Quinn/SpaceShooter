using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] BoxCollider2D _collider;

    float _speed = 4.0f;
    float _bottomOfTheScreen = -6.0f;
    float _topOfTheScreen = 8.0f;

    //Width of the screen
    float _minX = -9.0f;
    float _maxX = 9.0f;

    int pointsForDestroying = 10;
    bool _isDead = false;

    public static Action<Vector3> EnemyDiedToLaser;

    GameType _gameType;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (_isDead)
            return;
        //when hit bottom respawn at top at random x
        if (transform.position.y < _bottomOfTheScreen) {
            if (_gameType == GameType.pacifist) {
                Score.AddToScore?.Invoke(pointsForDestroying);
                HandleDeath();
            }
            transform.position = new Vector3(Random.Range(_minX, _maxX), _topOfTheScreen, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Damage Player");
            if (other.TryGetComponent<Player>(out var player)) {
                player.TakeHit();
            }
            HandleDeath();
        }
        else if (other.CompareTag("Laser"))
        {
            Debug.Log("Enemy Hit");
            other.gameObject.SetActive(false);
            EnemyDiedToLaser?.Invoke(transform.position);
            Score.AddToScore?.Invoke(pointsForDestroying);
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        _isDead = true;
        _anim.SetTrigger("Die");
        _collider.enabled = false;
        Destroy(gameObject, 1.62f);
    }

    public void SetGameType(GameType x) {
        _gameType = x;
    }
}
