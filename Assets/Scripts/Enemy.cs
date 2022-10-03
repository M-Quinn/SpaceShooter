using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float _speed = 4.0f;
    float _bottomOfTheScreen = -6.0f;
    float _topOfTheScreen = 8.0f;

    //Width of the screen
    float _minX = -9.0f;
    float _maxX = 9.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //when hit bottom respawn at top at random x
        if (transform.position.y < _bottomOfTheScreen) {
            transform.position = new Vector3(Random.Range(_minX, _maxX), _topOfTheScreen, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Damage Player");
            if (other.TryGetComponent<Player>(out var player)) {
                player.Damage();
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Laser")) {
            Debug.Log("Enemy Hit");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
