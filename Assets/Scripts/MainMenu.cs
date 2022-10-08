using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] Background[] _backgrounds;

    public void StartGame() {
        _anim.SetTrigger("Exit");
    }

    IEnumerator LoadLevelAsync() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        while (!asyncLoad.isDone) {
            foreach (Background bg in _backgrounds) {
                bg.IncreaseSpeed();
            }
            yield return null;
        }
    }
}
