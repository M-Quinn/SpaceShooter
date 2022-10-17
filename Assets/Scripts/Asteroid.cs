using Dev.MikeQ.SpaceShooter.Events;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Dev.MikeQ.SpaceShooter.GameManagement
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] GameObject _explosionVFX;
        float _rotSpeed = 17.0f;
        float _moveSpeed = 1.5f;
        Vector3 _finishedLocation = new Vector3(0, 2, 0);

        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] CircleCollider2D _collider;
        [SerializeField] Transform _transform;
        [SerializeField] TMP_Text _text_Wave;

        // Update is called once per frame
        void Update()
        {
            _transform.Rotate(new Vector3(0, 0, _rotSpeed * Time.deltaTime));
            var step = _moveSpeed * Time.deltaTime; // calculate distance to move
                                                    //Need to only move the parent and rotate the child
            gameObject.transform.position = Vector3.MoveTowards(_transform.position, _finishedLocation, step);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            EventManager.AsteroidExploded?.Invoke();
            collision.transform.gameObject.SetActive(false);
            StartCoroutine(Explosion());

            //destroy asteroid
            //invoke start next round
        }

        IEnumerator Explosion()
        {
            GameObject explosion = Instantiate(_explosionVFX, _transform.position, Quaternion.identity);
            _collider.enabled = false;
            yield return new WaitForSeconds(0.5f);
            _spriteRenderer.enabled = false;
            yield return new WaitForSeconds(2.5f);
            EventManager.StartNextRound?.Invoke();
            Destroy(explosion);
            Destroy(gameObject);
        }

        public void SetWaveText(int waveNum)
        {
            _text_Wave.text = "WAVE " + waveNum.ToString();
        }
    }

}
