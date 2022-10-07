using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text_GameOver;

    private void OnEnable()
    {
        Player.PlayerDied += HandleGameOver;
    }
    private void OnDisable()
    {
        Player.PlayerDied -= HandleGameOver;
    }

    private void HandleGameOver() {
        _text_GameOver.gameObject.SetActive(true);
    }

}
