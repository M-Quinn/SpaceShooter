using Dev.MikeQ.SpaceShooter.Events;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Dev.MikeQ.SpaceShooter.GameManagement
{
    public class GameStart : MonoBehaviour
    {


        [SerializeField] Image _fadeOutImage;
        GameObject _mainCamera;
        float delay;

        private void Start()
        {
            _mainCamera = Camera.main.gameObject;
            StartCoroutine(StartGame());
        }
        IEnumerator StartGame()
        {

            while (_fadeOutImage.color.a >= 0 || (_mainCamera.transform.position.y < 1))
            {
                var color = _fadeOutImage.color;
                color.a -= (Time.deltaTime / 2);
                _fadeOutImage.color = color;
                _mainCamera.transform.Translate(Vector3.up * Time.deltaTime * 3);
                yield return null;
            }
            _fadeOutImage.gameObject.SetActive(false);
            EventManager.GameIsReady?.Invoke();

        }
    }

}
