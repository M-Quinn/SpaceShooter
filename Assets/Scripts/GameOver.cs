using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject _text_GameOver;
    [SerializeField] GameObject _text_Restart;

    private void OnEnable()
    {
        Player.PlayerDied += HandleGameOver;
    }
    private void OnDisable()
    {
        Player.PlayerDied -= HandleGameOver;
    }

    private void HandleGameOver() {
        _text_GameOver.SetActive(true);
        _text_Restart.SetActive(true);
    }

}
