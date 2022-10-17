using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dev.MikeQ.SpaceShooter.Input {
    public class InputHandler : MonoBehaviour
    {
        [SerializeField]
        PlayerInput _playerInput;

        public bool Fire;
        public Vector2 Move;
        public bool Restart;
        public bool Thrusters;

        InputAction _fireAction;
        InputAction _moveAction;
        InputAction _restartAction;
        InputAction _thrustersAction;

        private void Awake()
        {
            _fireAction = _playerInput.actions["Fire"];
            _moveAction = _playerInput.actions["Move"];
            _restartAction = _playerInput.actions["Restart"];
            _thrustersAction = _playerInput.actions["Thrusters"];
        }
        private void Update()
        {
            Restart = _restartAction.triggered;
            Fire = _fireAction.IsPressed();
            Thrusters = _thrustersAction.IsPressed();
            Move = _moveAction.ReadValue<Vector2>();
        }
    }

}
