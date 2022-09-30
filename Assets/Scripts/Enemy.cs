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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //when hit bottom respawn at top at random x
        if (transform.position.y < _bottomOfTheScreen) {
            transform.position = new Vector3(Random.Range(_minX, _maxX), _topOfTheScreen, transform.position.z);
        }
    }
}
