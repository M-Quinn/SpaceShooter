using Dev.MikeQ.SpaceShooter.Events;
using TMPro;
using UnityEngine;

namespace Dev.MikeQ.SpaceShooter.GameManagement
{
    public class Score : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _text_score;
        [SerializeField] TextMeshProUGUI _text_highScore;

        int _score;
        int _highScore = 0;

        string _hScore = "HighScore";

        private void Start()
        {
            var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (gameManager == null)
                Debug.LogError($"{this.name} Couldn't find the GameManager");
            else
            {
                if (gameManager.GetGameType() == GameType.pacifist)
                {
                    _hScore = "PacifistHighScore";
                }
            }
            _score = 0;
            _text_score.text = _score.ToString();
            if (PlayerPrefs.HasKey(_hScore))
                _highScore = PlayerPrefs.GetInt(_hScore);
            _text_highScore.text = _highScore.ToString();
        }

        private void OnEnable()
        {
            EventManager.AddToScore += UpdateScore;
            EventManager.PlayerDied += SaveHighScore;
        }

        private void OnDisable()
        {
            EventManager.AddToScore -= UpdateScore;
            EventManager.PlayerDied -= SaveHighScore;
        }

        private void UpdateScore(int amountToAdd)
        {
            _score += amountToAdd;
            _text_score.text = _score.ToString();
            if (_score >= _highScore)
            {
                _highScore = _score;
                _text_highScore.text = _highScore.ToString();
            }
        }

        private void SaveHighScore()
        {
            PlayerPrefs.SetInt(_hScore, _highScore);
        }
    }

}
