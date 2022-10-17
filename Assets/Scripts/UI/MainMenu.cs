using Dev.MikeQ.SpaceShooter.Audio;
using Dev.MikeQ.SpaceShooter.Environment;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dev.MikeQ.SpaceShooter.GameManagement
{
    public class MainMenu : MonoBehaviour
    {
        GameManager _gameManager;
        [SerializeField] Animator _anim;
        [SerializeField] Background[] _backgrounds;
        [SerializeField] Image _fadeOutImage;
        [SerializeField] BackgroundMusic _backgroundMusic;

        float _delay = 1.7f;

        private void Awake()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        public void StartNormalGame()
        {
            _gameManager.SetGameType(GameType.normal);
            StartGame();
        }
        public void StartPacifistGame()
        {
            _gameManager.SetGameType(GameType.pacifist);
            StartGame();
        }
        private void StartGame()
        {
            _anim.SetTrigger("Exit");
            StartCoroutine(LoadLevelAsync(_delay));
        }

        IEnumerator LoadLevelAsync(float delay)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            asyncLoad.allowSceneActivation = false;
            _fadeOutImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _backgroundMusic.StartFadeOut(delay);
            var timer = Time.time + delay;
            var timerDelay = timer - 1;
            while (!asyncLoad.isDone && (Time.time <= timer))
            {
                var color = _fadeOutImage.color;
                if (Time.time >= timerDelay)
                {
                    color.a += Time.deltaTime;
                    _fadeOutImage.color = color;
                }
                foreach (Background bg in _backgrounds)
                {
                    bg.IncreaseSpeed();
                }
                yield return null;
            }
            asyncLoad.allowSceneActivation = true;
        }
    }

}
