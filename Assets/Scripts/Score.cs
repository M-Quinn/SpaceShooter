using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text_score;
    [SerializeField] TextMeshProUGUI _text_highScore;

    int _score;
    int _highScore = 0;

    string _hScore = "HighScore";

    public static Action<int> AddToScore;

    private void Start()
    {
        _score = 0;
        _text_score.text = _score.ToString();
        if (PlayerPrefs.HasKey(_hScore))
            _highScore = PlayerPrefs.GetInt(_hScore);
        _text_highScore.text = _highScore.ToString();
    }

    private void OnEnable()
    {
        AddToScore += UpdateScore;
        Player.PlayerDied += SaveHighScore;
    }

    private void OnDisable()
    {
        AddToScore -= UpdateScore;
        Player.PlayerDied -= SaveHighScore;
    }

    private void UpdateScore(int amountToAdd) {
        _score += amountToAdd;
        _text_score.text = _score.ToString();
        if (_score >= _highScore) {
            _highScore = _score;
            _text_highScore.text = _highScore.ToString();
        }
    }

    private void SaveHighScore() {
        PlayerPrefs.SetInt(_hScore, _highScore);
    }
}
