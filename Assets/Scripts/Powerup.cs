using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    enum PowerupLogic { TripleShot, Shield, SpeedBoost}
    [SerializeField]
    PowerupLogic _behavior;
    float _speed;

    private void Update()
    {
        
    }
}
