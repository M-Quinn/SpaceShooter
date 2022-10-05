using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupLogic { TripleShot, Shield, SpeedBoost}
    [SerializeField]
    PowerupLogic _behavior;
    float _speed = 3.0f;
    float _bottomOfScreen = -5.83f;

    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= _bottomOfScreen) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (_behavior) {
            case PowerupLogic.TripleShot:
                Debug.Log("TripleShot");
                Destroy(gameObject);
                return;
            case PowerupLogic.Shield:
                Destroy(gameObject);
                return;
            case PowerupLogic.SpeedBoost:
                Destroy(gameObject);
                return;
            default:
                Debug.LogError("No Behavior was detected");
                return;
        }
        
    }

}
