using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    Rigidbody rb;

    float _speed = 8.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        StartCoroutine(DisableGameObjectWithDelay(5.0f));
    }
    IEnumerator DisableGameObjectWithDelay(float delay) {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    private void Update()
    {
        transform.Translate(transform.up * _speed * Time.deltaTime);
    }
}
