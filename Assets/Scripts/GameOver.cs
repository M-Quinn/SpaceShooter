using Dev.MikeQ.SpaceShooter.Audio;
using Dev.MikeQ.SpaceShooter.Events;
using Dev.MikeQ.SpaceShooter.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dev.MikeQ.SpaceShooter.GameManagement {
    public class GameOver : MonoBehaviour
    {
        [Header("Text Objects")]
        [SerializeField] GameObject _text_GameOver;
        [SerializeField] GameObject _text_Restart;
        [Header("Dependencies")]
        [SerializeField] InputHandler _input;

        bool _isGameOver;

        SoundEffects _soundEffects;

        private void Start()
        {
            _soundEffects = GameObject.Find("SoundEffects").GetComponent<SoundEffects>();
        }

        private void OnEnable()
        {
            EventManager.PlayerDied += HandleGameOver;
        }
        private void OnDisable()
        {
            EventManager.PlayerDied -= HandleGameOver;
        }

        private void Update()
        {
            if (!_isGameOver)
                return;
            if (_input.Restart)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void HandleGameOver()
        {
            _isGameOver = true;
            _text_GameOver.SetActive(true);
            _text_Restart.SetActive(true);
        }

    }

}
