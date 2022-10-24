using Dev.MikeQ.SpaceShooter.Events;
using Dev.MikeQ.SpaceShooter.Utils;
using System;
using System.Collections;
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

        Color _shieldFull = new Color(0.2047369f, 1, 0, 0.5019608f);
        Color _shieldMed = new Color(1, 0.495748f, 0, 0.5019608f);
        Color _shieldLow = new Color(1, 0.08126676f, 0, 0.5019608f);
        SpriteRenderer _shieldSpriteRenderer;
        Animator _shieldAnim;
        const string TAKE_HIT_STRING = "TakeHit";

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
        int _shieldStrength = 3;

        Vector3 _bigScale = new Vector3(1, 1, 1);
        Vector3 _smallScale = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 _normalScale = new Vector3(0.7f, 0.7f, 0.7f);

        private void Start()
        {
            _shieldSpriteRenderer = _shield.GetComponent<SpriteRenderer>();
            _shieldAnim = _shield.GetComponent<Animator>();
        }

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
                    if (_shieldStrength>2&&_isShieldEnabled)
                        return false;
                    _isShieldEnabled = true;
                    _shieldStrength = 3;
                    _shieldSpriteRenderer.color = _shieldFull;
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
                case Powerup.PowerupLogic.Ammo:
                    EventManager.AmmoPickup?.Invoke();
                    return true;
                case Powerup.PowerupLogic.Health:
                    EventManager.HealthPickup?.Invoke(1);
                    return true;
                default:
                    Debug.LogError("No Behavior was detected");
                    return false;
            }
        }

        public void UseShield() {
            _shieldAnim.SetTrigger(TAKE_HIT_STRING);
            _shieldStrength--;
            if (_shieldStrength <= 0)
            {
                _isShieldEnabled = false;
                _shield.SetActive(_isShieldEnabled);
            }
            else
            {
                if (_shieldStrength == 2)
                {
                    StartCoroutine(TweenColor(_shieldMed, _shieldStrength));
                }
                else
                    StartCoroutine(TweenColor(_shieldLow, _shieldStrength));

            }
        }

        IEnumerator TweenColor(Color newColor, int strength) {
            float t = 0;
            Color oldColor = _shieldSpriteRenderer.color;
            while (_shieldSpriteRenderer.color != newColor && _shieldStrength==strength) {
                _shieldSpriteRenderer.color = Color.Lerp(oldColor, newColor, t);
                yield return null;
                t += Time.deltaTime*2;
            }
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
