using Dev.MikeQ.SpaceShooter.Events;
using Dev.MikeQ.SpaceShooter.Player;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dev.MikeQ.SpaceShooter.Player {
    public class Health : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _playerSprite;
        [SerializeField] GameObject[] _damageVFXs;
        PowerupHandler _powerupHandler;
        int _maxLives = 3;
        int _lives = 3;
        bool _isAboutToDie;

        private void Start()
        {
            _powerupHandler = GetComponent<PowerupHandler>();
            var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (gameManager == null)
                Debug.LogError($"{this.name} Couldn't find the GameManager");
            else
            {
                if (gameManager.GetGameType() == GameType.pacifist)
                {
                    _maxLives = 1;
                    _lives = 1;
                    PlayerIsAboutToDie(true);
                }
            }
        }
        private void OnEnable()
        {
            EventManager.HealthPickup += UpdateHealth;
        }

        public void UpdateHealth(int amount)
        {
            _lives = Mathf.Clamp(_lives += amount, 0, _maxLives);
            if (_isAboutToDie && _lives > 1)
            {
                PlayerIsAboutToDie(false);
            }
            if (_lives == 1)
            {
                PlayerIsAboutToDie(true);
            }
            else if (_lives == 0)
            {
                EventManager.PlayerDied?.Invoke();
                Destroy(gameObject);
            }
            UpdateDamageVFX(amount);
        }

        private void UpdateDamageVFX(int amount)
        {
            if (amount < 0)
            {
                EventManager.PlayerTookDamage?.Invoke();
                _damageVFXs[Random.Range(0, 2)].SetActive(true);
            }
            else
            {
                foreach (var damageVFX in _damageVFXs)
                {
                    if (damageVFX.activeInHierarchy)
                    {
                        damageVFX.SetActive(false);
                        break;
                    }
                }
            }
        }

        public void PlayerIsAboutToDie(bool isAboutToDie)
        {
            foreach (GameObject go in _damageVFXs)
            {
                go.SetActive(true);
            }
            _isAboutToDie = isAboutToDie;
            if (_isAboutToDie)
                StartCoroutine(RedFlash());
        }
        IEnumerator RedFlash()
        {
            while (_isAboutToDie)
            {
                _playerSprite.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));
                yield return null;
            }
        }
        public void TakeHit()
        {
            if (_powerupHandler.IsShieldEnabled)
            {
                _powerupHandler.UseShield();
                return;
            }
            UpdateHealth(-1);
        }
    }
}

