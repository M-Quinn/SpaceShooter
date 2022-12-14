using Dev.MikeQ.SpaceShooter.Events;
using Dev.MikeQ.SpaceShooter.GameManagement;
using Dev.MikeQ.SpaceShooter.Input;
using Dev.MikeQ.SpaceShooter.Utils;
using System;
using System.Collections;
using UnityEngine;

namespace Dev.MikeQ.SpaceShooter.Player
{
    public class PlayerFireLaser : MonoBehaviour
    {
        [SerializeField] InputHandler _input;
        [SerializeField] PooledObjects _objectPool;
        [Header("Laser Positions To Spawn")]
        [SerializeField] GameObject _topLaserPosition;
        [SerializeField] GameObject _leftLaserPosition;
        [SerializeField] GameObject _rightLaserPosition;
        PowerupHandler _powerupHandler;

        float _timeToWait = 0.3f;
        float _timeToWaitSuperLaser = .01f;
        float _cooldownTimer;
        int _ammo = 15;
        int _maxAmmo = 15;
        bool _ammoCanDecrement;
        bool _isFiringSuperLaser;

        GameType _gameType;

        

        private void Start()
        {
            _powerupHandler = GetComponent<PowerupHandler>();
            _cooldownTimer = Time.time + _timeToWait;

            var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (gameManager == null)
                Debug.LogError($"{this.name} Couldn't find the GameManager");
            else
                _gameType = gameManager.GetGameType();
        }
        private void OnEnable()
        {
            EventManager.AmmoPickup += RefilAmmo;
            EventManager.RoundOver += PauseAmmoDecrement;
            EventManager.StartNextRound += ResumeAmmoDecrement;
        }
        private void OnDisable()
        {
            EventManager.AmmoPickup -= RefilAmmo;
            EventManager.RoundOver -= PauseAmmoDecrement;
            EventManager.StartNextRound -= ResumeAmmoDecrement;
        }

        private void Update()
        {
            if (_powerupHandler.IsSuperLaserEnabled) {
                if (_isFiringSuperLaser)
                    return;
                StartCoroutine(FireSuperLaser());
                return;
            }
            if (_isFiringSuperLaser) {
                if (!_powerupHandler.IsSuperLaserEnabled)
                    _isFiringSuperLaser = false;
                return;
            }
            if (_input.Fire && Time.time >= _cooldownTimer)
            {
                if (_gameType == GameType.normal)
                    FireLaser();
            }
            
        }
        private void FireLaser()
        {
            if (_ammo <= 0)
                DryFire();
            else if (_powerupHandler.IsTripleShotEnabled)
                FireTripleShot();
            else
                FireNormalShot();
            DecrementAmmo();
            _cooldownTimer = Time.time + _timeToWait;
        }

        private void DecrementAmmo()
        {
            if (!_ammoCanDecrement)
                return;
            _ammo = Mathf.Clamp(_ammo - 1, 0, _maxAmmo);
            UIManager.UpdateAmmo(_ammo);
        }

        private void FireTripleShot()
        {
            _objectPool.GetPlayerLaser(_topLaserPosition.transform.position);
            _objectPool.GetPlayerLaser(_leftLaserPosition.transform.position);
            _objectPool.GetPlayerLaser(_rightLaserPosition.transform.position);
        }
        private void FireNormalShot()
        {
            _objectPool.GetPlayerLaser(_topLaserPosition.transform.position);
        }
        IEnumerator FireSuperLaser() {
            _isFiringSuperLaser = true;
            while (_isFiringSuperLaser) {
                FireNormalShot();
                yield return new WaitForSeconds(_timeToWaitSuperLaser);
            }
        }
        private void DryFire() {
            EventManager.DryFireShot?.Invoke();
        }
        private void RefilAmmo()
        {
            _ammo = _maxAmmo;
            UIManager.UpdateAmmo(_ammo);
        }
        private void PauseAmmoDecrement() {
            RefilAmmo();
            _ammoCanDecrement = false;
            UIManager.InfiniteAmmo?.Invoke();
        }
        private void ResumeAmmoDecrement() {
            RefilAmmo();
            _ammoCanDecrement = true;
            UIManager.UpdateAmmo(_ammo);

        }
    }
}

