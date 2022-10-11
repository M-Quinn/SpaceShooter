using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    Rigidbody rb;

    float _speed = 6.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        StartCoroutine(DisableGameObjectWithDelay(5.0f));
        SoundEffects.EnemyLaserShot?.Invoke();
    }
    IEnumerator DisableGameObjectWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * -transform.up);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            player.TakeHit();
        }
        Destroy(gameObject);
    }
}
