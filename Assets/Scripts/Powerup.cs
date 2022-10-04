using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    enum PowerupLogic { TripleShot, Shield, SpeedBoost}
    [SerializeField]
    PowerupLogic _behavior;
    float _speed;
    float _bottomOfScreen = -5.83f;

    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= _bottomOfScreen) {
            Destroy(gameObject);
        }
    }


}
