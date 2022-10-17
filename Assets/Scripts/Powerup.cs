using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dev.MikeQ.SpaceShooter.Player;

namespace Dev.MikeQ.SpaceShooter.Utils {
    public class Powerup : MonoBehaviour
    {
        public enum PowerupLogic { TripleShot, Shield, SpeedBoost, Big, Small }
        [SerializeField]
        PowerupLogic _behavior;
        float _speed = 3.0f;
        float _bottomOfScreen = -5.83f;

        private void Update()
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            if (transform.position.y <= _bottomOfScreen)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<PlayerHandler>(out var player))
            {
                if (player.PowerupActivate(_behavior))
                    Destroy(gameObject);
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            //If the powerup couldn't be pickedup on enter
            if (collision.TryGetComponent<PlayerHandler>(out var player))
            {
                if (player.PowerupActivate(_behavior))
                    Destroy(gameObject);
            }
        }

    }

}
