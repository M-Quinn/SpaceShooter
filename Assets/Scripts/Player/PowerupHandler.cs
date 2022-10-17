using Dev.MikeQ.SpaceShooter.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dev.MikeQ.SpaceShooter.Player
{
    public class PowerupHandler : MonoBehaviour
    {
        [SerializeField] GameObject _shield;
        [Header("Powerup UI")]
        [SerializeField] Image _img_TripleShot;
        [SerializeField] Image _img_Speed;
        [SerializeField] Image _img_Big;
        [SerializeField] Image _img_Small;

        public bool IsTripleShotEnabled { get; private set; }
        public bool IsSpeedBoostEnabled { get; private set; }
        public bool IsShieldEnabled { get; private set; }

        //Powerups
        bool _isTripleShotEnabled = false;
        bool _isSpeedBoostEnabled = false;
        bool _isShieldEnabled = false;
        bool _isBigEnabled = false;
        bool _isSmallEnabled = false;

        float _powerupCooldown = 5.0f;

        Vector3 _bigScale = new Vector3(1, 1, 1);
        Vector3 _smallScale = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 _normalScale = new Vector3(0.7f, 0.7f, 0.7f);

        private void Update()
        {
            IsTripleShotEnabled = _isTripleShotEnabled;
            IsShieldEnabled = _isShieldEnabled;
            IsSpeedBoostEnabled = _isSpeedBoostEnabled;
            SetPlayerScale();
        }


        private void SetPlayerScale()
        {
            if (!_isSmallEnabled & !_isBigEnabled)
            {
                transform.localScale = _normalScale;
            }
            else if (_isBigEnabled)
            {
                transform.localScale = _bigScale;
            }
            else if (_isSmallEnabled)
            {
                transform.localScale = _smallScale;
            }
        }

        public bool PowerupActivate(Powerup.PowerupLogic powerup)
        {
            switch (powerup)
            {
                case Powerup.PowerupLogic.TripleShot:
                    if (_isTripleShotEnabled)
                        return false;
                    StartCoroutine(PowerupCooldown(result => _isTripleShotEnabled = result, _img_TripleShot, _powerupCooldown));
                    return true;
                case Powerup.PowerupLogic.Shield:
                    if (_isShieldEnabled)
                        return false;
                    _isShieldEnabled = true;
                    _shield.SetActive(_isShieldEnabled);
                    return true;
                case Powerup.PowerupLogic.SpeedBoost:
                    if (_isSpeedBoostEnabled)
                        return false;
                    StartCoroutine(PowerupCooldown(result => _isSpeedBoostEnabled = result, _img_Speed, _powerupCooldown));
                    return true;
                case Powerup.PowerupLogic.Big:
                    if (_isBigEnabled)
                        return false;
                    StartCoroutine(PowerupCooldown(result => _isBigEnabled = result, _img_Big, _powerupCooldown));
                    return true;
                case Powerup.PowerupLogic.Small:
                    if (_isSmallEnabled)
                        return false;
                    StartCoroutine(PowerupCooldown(result => _isSmallEnabled = result, _img_Small, _powerupCooldown));
                    return true;
                default:
                    Debug.LogError("No Behavior was detected");
                    return false;
            }
        }

        public void UseShield() {
            _isShieldEnabled = false;
            _shield.SetActive(_isShieldEnabled);
        }

        IEnumerator PowerupCooldown(Action<bool> powerup, Image ui_Image, float cooldown)
        {
            powerup(true);
            ui_Image.fillAmount = 1.0f;
            ui_Image.gameObject.SetActive(true);
            float timer = Time.time + cooldown;
            while (Time.time <= timer)
            {
                ui_Image.fillAmount = (timer - Time.time) / cooldown;
                yield return null;
            }
            ui_Image.gameObject.SetActive(false);
            powerup(false);
        }
    }

}
