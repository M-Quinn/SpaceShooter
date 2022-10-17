using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireLaser : MonoBehaviour
{
    [SerializeField] GameObject _enemyLaser;
    float _minTimeBeforeShot = 3.0f;
    float _maxTimeBeforeShot = 7.0f;
    float timer;

    private void Start()
    {
        timer = Time.time + GetRandomNumber();
    }

    private void Update()
    {

        if (Time.time >= timer) {
            Instantiate(_enemyLaser, new Vector3(transform.position.x, transform.position.y - 1.0f, 0), Quaternion.identity);
            
            timer = Time.time + GetRandomNumber();
        }
    }

    private float GetRandomNumber() {
        return Random.Range(_minTimeBeforeShot, _maxTimeBeforeShot);
    }
}
