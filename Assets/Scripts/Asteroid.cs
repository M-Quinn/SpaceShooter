using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    float _rotSpeed = 17.0f;
    float _moveSpeed = 1.5f;
    Vector3 _finishedLocation = new Vector3(0,2,0);

    public static Action StartNextRound;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _rotSpeed * Time.deltaTime));
        var step = _moveSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, _finishedLocation, step);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //destroy asteroid
        //invoke start next round
    }
}
