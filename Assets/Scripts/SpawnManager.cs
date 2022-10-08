using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Powerups")]
    [SerializeField] GameObject _tripleShotPrefab;
    [SerializeField] GameObject _speedBoostPrefab;
    [SerializeField] GameObject _shieldBoostPrefab;
    [SerializeField] GameObject _bigPrefab;
    [SerializeField] GameObject _smallPrefab;
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

    GameType _gameType;

    private void OnEnable()
    {
        Player.PlayerDied += () => _stopSpawning = true;
        Enemy.EnemyDiedToLaser += ChanceToSpawnPowerup;
        GameStart.GameIsReady += ()=> StartCoroutine(SpawnCoroutine());//Change to spawn asteroid then on asteroid destruction, start coroutine
        //Asteroid.StartNextRound +=
    }
    private void OnDisable()
    {
        Player.PlayerDied -= () => _stopSpawning = true;
        Enemy.EnemyDiedToLaser -= ChanceToSpawnPowerup;
        GameStart.GameIsReady -= () => StartCoroutine(SpawnCoroutine());
    }
    private void Start()
    {
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager == null)
            Debug.LogError($"{this.name} Couldn't find the GameManager");
        else
            _gameType = gameManager.GetGameType();
    }


    IEnumerator SpawnCoroutine() {
        //Increase speed here
        //Increase amount to spawn here
        while (!_stopSpawning) {
            positionToSpawn = new Vector3(Random.Range(_minX, _maxX), _topOfTheScreen, transform.position.z);
            GameObject temp = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
            temp.GetComponent<Enemy>().SetGameType(_gameType);
            temp.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(_secondsToWait);
            _secondsToWait -= _secondsToWait * 0.02f;
        }
    }

    private void ChanceToSpawnPowerup(Vector3 location) {
        float chance = Random.Range(0.0f, 1.0f);
        if (chance <= 0.1f)
        {
            Instantiate(_tripleShotPrefab, location, Quaternion.identity);
        }
        else if (chance <= 0.2f)
        {
            Instantiate(_speedBoostPrefab, location, Quaternion.identity);
        }
        else if (chance <= 0.3f)
        {
            Instantiate(_shieldBoostPrefab, location, Quaternion.identity);
        }
        else if (chance <= 0.4f)
        {
            Instantiate(_bigPrefab, location, Quaternion.identity);
        }
        else if (chance <= 0.5f) {
            Instantiate(_smallPrefab, location, Quaternion.identity);
        }

    }
}
