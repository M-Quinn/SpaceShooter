using Dev.MikeQ.SpaceShooter.Events;
using Dev.MikeQ.SpaceShooter.GameManagement;
using Dev.MikeQ.SpaceShooter.Input;
using Dev.MikeQ.SpaceShooter.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev.MikeQ.SpaceShooter.Player {
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
        float _cooldownTimer;
        int _ammo = 15;
        int _maxAmmo = 15;

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

        private void Update()
        {
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
            _ammo--;
            UIManager.UpdateAmmo(_ammo);
            _cooldownTimer = Time.time + _timeToWait;
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
        private void DryFire() {
            EventManager.DryFireShot?.Invoke();
        }
    }
}

