using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    [SerializeField]
    InputHandler _input;
    [SerializeField] PooledObjects _objectPool;
    [SerializeField] GameObject _shield;
    [Header("Laser Positions To Spawn")]
    [SerializeField] GameObject _topLaserPosition;
    [SerializeField] GameObject _leftLaserPosition;
    [SerializeField] GameObject _rightLaserPosition;


    int _lives = 3;

    float _speed;
    float _normalSpeed = 5.0f;
    float _boostSpeed = 8.5f;
    float _minYPos = -3.8f;
    float _maxYPos = 0.0f;
    float _rightOutOfBounds = 11.1f;
    float _leftOutOfBounds = -11.2f;

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


    float _timeToWait = 0.3f;
    float _cooldownTimer;

    public static event Action PlayerDied;

    // Start is called before the first frame update
    void Start()
    {
        _cooldownTimer = Time.time + _timeToWait;

        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayerScale();

        _speed = _isSpeedBoostEnabled ? _boostSpeed : _normalSpeed;
        transform.Translate(new Vector3(_input.Move.x, _input.Move.y, 0) * _speed * Time.deltaTime);
        Vector3 curPos = SetBounds(transform.position);
        transform.position = curPos;


        if (_input.Fire && Time.time >= _cooldownTimer)
        {
            FireLaser();
        }

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

    private void FireLaser()
    {
        
        if (_isTripleShotEnabled)
            FireTripleShot();
        else
            FireNormalShot();
        _cooldownTimer = Time.time + _timeToWait;
    }

    private void FireTripleShot()
    {
        _objectPool.GetPlayerLaser(_topLaserPosition.transform.position);
        _objectPool.GetPlayerLaser(_leftLaserPosition.transform.position);
        _objectPool.GetPlayerLaser(_rightLaserPosition.transform.position);
    }
    private void FireNormalShot() {
        _objectPool.GetPlayerLaser(_topLaserPosition.transform.position);
    }

    private Vector3 SetBounds(Vector3 currentPosition)
    {
        var curPos = currentPosition;
        curPos.y = Mathf.Clamp(curPos.y, _minYPos, _maxYPos);
        if (curPos.x > _rightOutOfBounds)
            curPos.x = _leftOutOfBounds + 0.1f;
        else if (curPos.x < _leftOutOfBounds)
            curPos.x = _rightOutOfBounds - 0.1f;
        return curPos;
    }

    public void Damage() {
        if (_isShieldEnabled) {
            _isShieldEnabled = false;
            _shield.SetActive(_isShieldEnabled);
            return;
        }
        _lives -= 1;
        if (_lives <= 0) {
            PlayerDied?.Invoke();
            Destroy(gameObject);
        }
    }

    public void PowerupActivate(Powerup.PowerupLogic powerup) {
        switch (powerup) {
            case Powerup.PowerupLogic.TripleShot:
                StartCoroutine(PowerupCooldown(result => _isTripleShotEnabled = result, _powerupCooldown));
                return;
            case Powerup.PowerupLogic.Shield:
                _isShieldEnabled = true;
                _shield.SetActive(_isShieldEnabled);
                return;
            case Powerup.PowerupLogic.SpeedBoost:
                StartCoroutine(PowerupCooldown(result => _isSpeedBoostEnabled = result, _powerupCooldown));
                return;
            case Powerup.PowerupLogic.Big:
                StartCoroutine(PowerupCooldown(result => _isBigEnabled = result, _powerupCooldown));
                return;
            case Powerup.PowerupLogic.Small:
                StartCoroutine(PowerupCooldown(result => _isSmallEnabled = result, _powerupCooldown));
                return;
            default:
                Debug.LogError("No Behavior was detected");
                return;
        }
    }

    IEnumerator PowerupCooldown(Action<bool> powerup, float cooldown) {
        powerup (true);
        yield return new WaitForSeconds(cooldown);
        powerup (false);
    }
}
