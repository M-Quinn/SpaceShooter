using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    InputHandler _input;
    [SerializeField]
    GameObject _laserPrefab;
    [Header("Laser Positions To Spawn")]
    [SerializeField] GameObject _topLaserPosition;
    [SerializeField] GameObject _leftLaserPosition;
    [SerializeField] GameObject _rightLaserPosition;


    int _lives = 3;

    float _speed = 5.0f;
    float _minYPos = -3.8f;
    float _maxYPos = 0.0f;
    float _rightOutOfBounds = 11.1f;
    float _leftOutOfBounds = -11.2f;

    bool _isTripleShotEnabled = true;


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

        transform.Translate(new Vector3(_input.Move.x, _input.Move.y, 0) * _speed * Time.deltaTime);
        Vector3 curPos = SetBounds(transform.position);
        transform.position = curPos;


        if (_input.Fire && Time.time >= _cooldownTimer)
        {
            FireLaser();
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

    private void FireTripleShot() {
        Instantiate(_laserPrefab, _topLaserPosition.transform.position, Quaternion.identity);
        Instantiate(_laserPrefab, _leftLaserPosition.transform.position, Quaternion.identity);
        Instantiate(_laserPrefab, _rightLaserPosition.transform.position, Quaternion.identity);
    }
    private void FireNormalShot() {
        Instantiate(_laserPrefab, _topLaserPosition.transform.position, Quaternion.identity);
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
        _lives -= 1;
        if (_lives <= 0) {
            PlayerDied?.Invoke();
            Destroy(gameObject);
        }
    }
}
