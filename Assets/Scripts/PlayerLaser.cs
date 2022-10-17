using Dev.MikeQ.SpaceShooter.Events;
using System.Collections;
using UnityEngine;

namespace Dev.MikeQ.SpaceShooter.Player
{
    public class PlayerLaser : MonoBehaviour
    {
        Rigidbody _rb;

        float _speed = 8.0f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        private void OnEnable()
        {
            StartCoroutine(DisableGameObjectWithDelay(5.0f));
            if (EventManager.PlayerLaserShot == null)
            {
                Debug.LogError("Laser Shot event is null");
            }
            EventManager.PlayerLaserShot.Invoke();
        }
        IEnumerator DisableGameObjectWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }
        private void Update()
        {
            transform.Translate(transform.up * _speed * Time.deltaTime);
        }
    }

}
