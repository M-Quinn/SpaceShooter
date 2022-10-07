using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text_score;
    [SerializeField] TextMeshProUGUI _text_highScore;

    int _score;
    int _highScore;

    public static Action<int> AddToScore;

    private void Start()
    {
        _score = 0;
        _text_score.text = _score.ToString();
    }

    private void OnEnable()
    {
        AddToScore += UpdateScore;
    }

    private void OnDisable()
    {
        AddToScore -= UpdateScore;
    }

    private void UpdateScore(int amountToAdd) {
        _score += amountToAdd;
        _text_score.text = _score.ToString();
    }
}
