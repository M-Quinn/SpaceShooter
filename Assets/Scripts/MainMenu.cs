using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] Background[] _backgrounds;

    float _delay = 3.5f;

    public void StartGame() {
        _anim.SetTrigger("Exit");
        StartCoroutine(LoadLevelAsync(_delay));
    }

    IEnumerator LoadLevelAsync(float delay) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;
        var timer = Time.time + delay;
        while (!asyncLoad.isDone && (Time.time<=timer)) {
            Debug.Log($"{Time.time} vs {timer}");
            Debug.Log(asyncLoad.isDone);
            foreach (Background bg in _backgrounds) {
                bg.IncreaseSpeed();
            }
            yield return null;
        }
        Debug.Log("Loop exited");
        asyncLoad.allowSceneActivation = true;
    }
}
