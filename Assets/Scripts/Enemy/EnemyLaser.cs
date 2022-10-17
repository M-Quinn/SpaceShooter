using Dev.MikeQ.SpaceShooter.Events;
using System.Collections;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    Rigidbody _rb;

    float _speed = 6.0f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        StartCoroutine(DisableGameObjectWithDelay(5.0f));
        EventManager.EnemyLaserShot?.Invoke();
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
        if (collision.TryGetComponent<PlayerHandler>(out var player))
        {
            player.TakeHit();
        }
        Destroy(gameObject);
    }
}
