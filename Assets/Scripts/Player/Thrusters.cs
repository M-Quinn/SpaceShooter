using Dev.MikeQ.SpaceShooter.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dev.MikeQ.SpaceShooter.Player {
    public class Thrusters : MonoBehaviour
    {
        [SerializeField] InputHandler _input;
        [SerializeField] Image _thrustersImg;
        [SerializeField] GameObject _thrustersGameObject;
        public bool Engaged { get; private set; }
        float _showTimerUI = 0;
        float _timeBeforeUIDisappears = 2.5f;
        float _totalThrusters;
        float _maxThrusterTime = 3.0f;

        private void Start()
        {
            _totalThrusters = _maxThrusterTime;
        }

        private void Update()
        {
            if (_input.Thrusters)
            {
                ShowThrusterUI();
                _totalThrusters = Mathf.Clamp(_totalThrusters - Time.deltaTime, 0, _maxThrusterTime);
                if (_totalThrusters > 0.0f)
                    ShowThrusters(true);
                else
                    ShowThrusters(false);
                    
            }
            else {
                ShowThrusters(false);
                if (_totalThrusters < _maxThrusterTime) {
                    _totalThrusters = Mathf.Clamp(_totalThrusters + (Time.deltaTime / 2), 0, _maxThrusterTime);
                    ShowThrusterUI();
                }
                    
                if (Time.time >= _showTimerUI) {
                    _thrustersImg.transform.parent.gameObject.SetActive(false);
                }
            }

        }

        private void ShowThrusters(bool isThrusting) {
            if (isThrusting)
                _thrustersGameObject.SetActive(true);
            else
                _thrustersGameObject.SetActive(false);
            Engaged = isThrusting;
        }

        private void ShowThrusterUI()
        {
            _showTimerUI = Time.time + _timeBeforeUIDisappears;
            _thrustersImg.transform.parent.gameObject.SetActive(true);
        }
    }

}