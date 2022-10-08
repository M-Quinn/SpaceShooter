using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] Background[] _backgrounds;
    [SerializeField] Image _fadeOutImage;

    float _delay = 2.5f;

    public void StartGame() {
        _anim.SetTrigger("Exit");
        StartCoroutine(LoadLevelAsync(_delay));
    }

    IEnumerator LoadLevelAsync(float delay) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;
        _fadeOutImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        var timer = Time.time + delay;
        var timerDelay = timer-1;
        while (!asyncLoad.isDone && (Time.time<=timer)) {
            var color = _fadeOutImage.color;
            if (Time.time >= timerDelay) {
                color.a += Time.deltaTime;
                _fadeOutImage.color = color;
            }
            foreach (Background bg in _backgrounds) {
                bg.IncreaseSpeed();
            }
            yield return null;
        }
        Debug.Log("Loop exited");
        asyncLoad.allowSceneActivation = true;
    }
}
