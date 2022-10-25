using System.Collections;
using UnityEngine;
using Dev.MikeQ.SpaceShooter.Events;

namespace Dev.MikeQ.SpaceShooter.Utils {
    public class CameraShake : MonoBehaviour
    {

        private void OnEnable()
        {
            EventManager.PlayerTookDamage += CreateShake;
        }
        private void OnDisable()
        {
            EventManager.PlayerTookDamage -= CreateShake;
        }

        public void CreateShake() {
            StartCoroutine(Shake(0.1f, 0.4f));
        }
        public IEnumerator Shake(float duration, float magnitude)
        {
            Vector3 orignalPosition = transform.position;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = transform.position.y;

                transform.position = new Vector3(x, y, -10f);
                elapsed += Time.deltaTime;
                yield return 0;
            }
            transform.position = orignalPosition;
        }
    }
}
