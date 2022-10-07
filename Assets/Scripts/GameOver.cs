using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [Header("Text Objects")]
    [SerializeField] GameObject _text_GameOver;
    [SerializeField] GameObject _text_Restart;
    [Header("Dependencies")]
    [SerializeField] InputHandler _input;

    bool _isGameOver;

    private void OnEnable()
    {
        Player.PlayerDied += HandleGameOver;
    }
    private void OnDisable()
    {
        Player.PlayerDied -= HandleGameOver;
    }

    private void Update()
    {
        if (!_isGameOver)
            return;
        if (_input.Restart)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HandleGameOver() {
        _text_GameOver.SetActive(true);
        _text_Restart.SetActive(true);
    }

}
