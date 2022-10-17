using Dev.MikeQ.SpaceShooter.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev.MikeQ.SpaceShooter.Player {
    [RequireComponent(typeof(PowerupHandler))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] InputHandler _input;

        float _speed;
        float _normalSpeed = 5.0f;
        float _boostSpeed = 8.5f;
        float _minYPos = -3.8f;
        float _maxYPos = 0.0f;
        float _rightOutOfBounds = 11.1f;
        float _leftOutOfBounds = -11.2f;

        PowerupHandler _powerupHandler;
        private void Start()
        {
            _powerupHandler = GetComponent<PowerupHandler>();
        }

        private void Update()
        {
            _speed = _powerupHandler.IsSpeedBoostEnabled ? _boostSpeed : _normalSpeed;
            _speed = _input.Thrusters ? _speed + _normalSpeed : _speed;
            transform.Translate(new Vector3(_input.Move.x, _input.Move.y, 0) * _speed * Time.deltaTime);
            Vector3 curPos = SetBounds(transform.position);
            transform.position = curPos;
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
    }

}

