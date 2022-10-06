using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Powerups")]
    [SerializeField] GameObject _tripleShotPrefab;
    [SerializeField] GameObject _speedBoostPrefab;
    [SerializeField] GameObject _shieldBoostPrefab;
    [Header("Enemy")]
    [SerializeField] GameObject _enemyContainer;
    [SerializeField] GameObject _enemyPrefab;
    float _secondsToWait = 5.0f;
    Vector3 positionToSpawn = Vector3.zero;

    float _topOfTheScreen = 8.0f;
    //Width of the screen
    float _minX = -9.0f;
    float _maxX = 9.0f;
    bool _stopSpawning = false;

    private void OnEnable()
    {
        Player.PlayerDied += () => _stopSpawning = true;
        Enemy.EnemyDiedToLaser += ChanceToSpawnPowerup;
    }
    private void OnDisable()
    {
        Player.PlayerDied -= () => _stopSpawning = true;
        Enemy.EnemyDiedToLaser -= ChanceToSpawnPowerup;
    }

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine() {
        while (!_stopSpawning) {
            positionToSpawn = new Vector3(Random.Range(_minX, _maxX), _topOfTheScreen, transform.position.z);
            GameObject temp = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
            temp.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(_secondsToWait);
        }
    }

    private void ChanceToSpawnPowerup(Vector3 location) {
        float chance = Random.Range(0.0f, 1.0f);
        if (chance <= 0.2f)
        {
            Instantiate(_tripleShotPrefab, location, Quaternion.identity);
        }
        else if (chance <= 0.4)
        {
            Instantiate(_speedBoostPrefab, location, Quaternion.identity);
        }
        else if (chance <= .06) {
            Instantiate(_shieldBoostPrefab, location, Quaternion.identity);
        }

    }
}
