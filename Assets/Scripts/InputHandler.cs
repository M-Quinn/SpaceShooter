using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dev.MikeQ.SpaceShooter.Input {
    public class InputHandler : MonoBehaviour
    {
        //Grab the Player Input component
        [SerializeField]
        PlayerInput _playerInput;

        //Public variables for other classes to check
        public bool Fire;
        public Vector2 Move;
        public bool Restart;

        //Private variables for holding the Input Action data
        InputAction _fireAction;
        InputAction _moveAction;
        InputAction _restartAction;

        private void Awake()
        {
            //Assign the private Input Action variables
            _fireAction = _playerInput.actions["Fire"];
            _moveAction = _playerInput.actions["Move"];
            _restartAction = _playerInput.actions["Restart"];
        }
        private void Update()
        {
            Restart = _restartAction.triggered;
            Fire = _fireAction.IsPressed();
            //IsPressed will read true whenever the button is held down

            Move = _moveAction.ReadValue<Vector2>();
        }
    }

}
