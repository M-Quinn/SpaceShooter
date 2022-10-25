using Dev.MikeQ.SpaceShooter.Enemy;
using Dev.MikeQ.SpaceShooter.Events;
using System.Collections;
using UnityEngine;

namespace Dev.MikeQ.SpaceShooter.GameManagement
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Powerups")]
        [SerializeField] GameObject _tripleShotPrefab;
        [SerializeField] GameObject _speedBoostPrefab;
        [SerializeField] GameObject _shieldBoostPrefab;
        [SerializeField] GameObject _bigPrefab;
        [SerializeField] GameObject _smallPrefab;
        [SerializeField] GameObject _healthPrefab;
        [SerializeField] GameObject _superLaserPrefab;
        [SerializeField] GameObject _ammoPrefab;
        [Header("Enemy")]
        [SerializeField] GameObject _enemyContainer;
        [SerializeField] GameObject _enemyPrefab;
        [Header("Asteroid")]
        [SerializeField] GameObject _asteroidPrefab;
        float _secondsToWait = 5.0f;
        Vector3 positionToSpawn = Vector3.zero;

        float _topOfTheScreen = 8.0f;
        //Width of the screen
        float _minX = -9.0f;
        float _maxX = 9.0f;

        bool _stopSpawning = false;
        bool _startSpawningAmmo = false; //true - spawns ammo | false - stops spawning ammo

        int _waveNumber = 0;
        int _amountOfEnemiesToSpawn = 5;    //Start on 5
        int _totalEnemiesSpawned_Wave = 0;  //Counts the enemies spawned;
        int _amountOfEnemiesLeft;

        GameType _gameType;

        private void OnEnable()
        {
            EventManager.PlayerDied += StopSpawning;
            EventManager.EnemyDiedToLaser += ChanceToSpawnPowerup;
            EventManager.EnemyBorn += AddEnemy;
            EventManager.EnemyDied += SubtractEnemy;
            EventManager.GameIsReady += SpawnAsteroid;
            EventManager.StartNextRound += NextRound;
            EventManager.LowAmmo += SpawnAmmoOn;
            EventManager.AmmoPickup += SpawnAmmoOff;
        }
        private void OnDisable()
        {
            EventManager.PlayerDied -= StopSpawning;
            EventManager.EnemyDiedToLaser -= ChanceToSpawnPowerup;
            EventManager.EnemyBorn -= AddEnemy;
            EventManager.EnemyDied -= SubtractEnemy;
            EventManager.GameIsReady -= SpawnAsteroid;
            EventManager.StartNextRound -= NextRound;
            EventManager.LowAmmo -= SpawnAmmoOn;
            EventManager.AmmoPickup -= SpawnAmmoOff;
        }

        private void Start()
        {
            var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (gameManager == null)
                Debug.LogError($"{this.name} Couldn't find the GameManager");
            else
                _gameType = gameManager.GetGameType();
        }

        private void SpawnAsteroid()
        {
            EventManager.RoundOver?.Invoke();
            _waveNumber++;
            _amountOfEnemiesToSpawn += (_amountOfEnemiesToSpawn / 2);
            if (_gameType == GameType.pacifist)
            {
                StartCoroutine(SpawnCoroutine());
                return;
            }
            GameObject asteroid = Instantiate(_asteroidPrefab, new Vector3(Random.Range(-2, 3), 8, 0), Quaternion.identity);
            asteroid.GetComponent<Asteroid>().SetWaveText(_waveNumber);

        }

        private void SpawnAmmoOn() {
            if (_startSpawningAmmo) {
                return;
            }
            StartCoroutine(SpawnAmmoCoroutine());
        }
        private void SpawnAmmoOff() => _startSpawningAmmo = false;

        IEnumerator SpawnCoroutine()
        {
            _totalEnemiesSpawned_Wave = 0;
            bool allEnemiesSpawned = false;
            //Increase speed here
            while (!_stopSpawning && !allEnemiesSpawned)
            {
                positionToSpawn = new Vector3(Random.Range(_minX, _maxX), _topOfTheScreen, transform.position.z);
                GameObject temp = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
                temp.GetComponent<EnemyHandler>().SetGameType(_gameType);
                temp.transform.parent = _enemyContainer.transform;
                _totalEnemiesSpawned_Wave++;
                if (_totalEnemiesSpawned_Wave >= _amountOfEnemiesToSpawn)
                    allEnemiesSpawned = true;
                yield return new WaitForSeconds(_secondsToWait);
                _secondsToWait -= _secondsToWait * 0.02f;
            }
            bool isWaveOver = false;
            while (!isWaveOver)
            {
                if (_amountOfEnemiesLeft <= 0)
                {
                    isWaveOver = true;
                }
                yield return null;
            }
            SpawnAsteroid();
        }

        IEnumerator SpawnAmmoCoroutine() {
            _startSpawningAmmo = true;
            while (_startSpawningAmmo) {
                yield return new WaitForSeconds(2.7f);
                Instantiate(_ammoPrefab, new Vector3(Random.Range(_minX, _maxX), _topOfTheScreen, 0), Quaternion.identity);
            }

                

        }

        private void ChanceToSpawnPowerup(Vector3 location)
        {
            float chance = Random.Range(0.0f, 1.0f);
            if (chance <= 0.10f)
            {
                Instantiate(_tripleShotPrefab, location, Quaternion.identity);
            }
            else if (chance <= 0.20f)
            {
                Instantiate(_speedBoostPrefab, location, Quaternion.identity);
            }
            else if (chance <= 0.30f)
            {
                Instantiate(_shieldBoostPrefab, location, Quaternion.identity);
            }
            else if (chance <= 0.40f)
            {
                Instantiate(_bigPrefab, location, Quaternion.identity);
            }
            else if (chance <= 0.50f)
            {
                Instantiate(_smallPrefab, location, Quaternion.identity);
            }
            else if (chance <= 0.60f)
            {
                Instantiate(_healthPrefab, location, Quaternion.identity);
            }
            else if (chance <= 0.65f) {
                Instantiate(_superLaserPrefab, location, Quaternion.identity);
            }
        }

        private void NextRound()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private void StopSpawning()
        {
            _stopSpawning = true;
        }

        private void AddEnemy()
        {
            _amountOfEnemiesLeft++;
        }
        private void SubtractEnemy()
        {
            _amountOfEnemiesLeft--;
        }
    }

}
