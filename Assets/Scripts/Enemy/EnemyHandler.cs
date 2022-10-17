using Dev.MikeQ.SpaceShooter.Events;
using Dev.MikeQ.SpaceShooter.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dev.MikeQ.SpaceShooter.Enemy
{
    public class EnemyHandler : MonoBehaviour
    {
        [SerializeField] Animator _anim;
        [SerializeField] BoxCollider2D _collider;

        float _speed = 4.0f;
        float _bottomOfTheScreen = -6.0f;
        float _topOfTheScreen = 8.0f;

        //Width of the screen
        float _minX = -9.0f;
        float _maxX = 9.0f;

        int pointsForDestroying = 10;
        bool _isDead = false;

        GameType _gameType;
        private void Start()
        {
            EventManager.EnemyBorn?.Invoke();
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            if (_isDead)
                return;
            //when hit bottom respawn at top at random x
            if (transform.position.y < _bottomOfTheScreen)
            {
                if (_gameType == GameType.pacifist)
                {
                    EventManager.AddToScore?.Invoke(pointsForDestroying);
                    HandleDeath();
                    return;
                }
                transform.position = new Vector3(Random.Range(_minX, _maxX), _topOfTheScreen, transform.position.z);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.TryGetComponent<Health>(out var health))
                {
                    health.TakeHit();
                }
                HandleDeath();
            }
            else if (other.CompareTag("Laser"))
            {
                other.gameObject.SetActive(false);
                EventManager.EnemyDiedToLaser?.Invoke(transform.position);
                EventManager.AddToScore?.Invoke(pointsForDestroying);
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            _isDead = true;
            _anim.SetTrigger("Die");
            _collider.enabled = false;
            EventManager.EnemyDied?.Invoke();
            Destroy(gameObject, 1.62f);
        }

        public void SetGameType(GameType x)
        {
            _gameType = x;
        }
    }
}

