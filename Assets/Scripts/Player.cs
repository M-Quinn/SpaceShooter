using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    InputHandler _input;

    float _speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(new Vector3(_input.Move.x, _input.Move.y, 0) * _speed * Time.deltaTime);
    }
}
