using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float _speed = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //when hit bottom respawn at top at random x
        if (transform.position.y < -6.0f) {
            transform.position = new Vector3(Random.Range(-9.0f, 9.0f), 8.0f, transform.position.z);
        }
    }
}
