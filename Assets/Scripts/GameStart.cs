using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public static Action GameIsReady;

    [SerializeField] Image _fadeOutImage;
    GameObject _mainCamera;
    float delay;

    private void Start()
    {
        _mainCamera = Camera.main.gameObject;
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame() {
        
        while (_fadeOutImage.color.a >= 0 || (_mainCamera.transform.position.y < 1)) {
            var color = _fadeOutImage.color;
            color.a -= (Time.deltaTime / 2);
            _fadeOutImage.color = color;
            _mainCamera.transform.Translate(Vector3.up * Time.deltaTime * 3);
            yield return null;
        }
        GameIsReady?.Invoke();

    }
}
