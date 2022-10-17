using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev.MikeQ.SpaceShooter.Environment {
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Texture2D))]
    public class Background : MonoBehaviour
    {
        [SerializeField] float _speed;
        Transform _mainCamera;
        float _imageSize;
        // Start is called before the first frame update
        void Start()
        {
            _mainCamera = Camera.main.transform;
            var sprite = GetComponent<SpriteRenderer>().sprite;
            var texture = sprite.texture;
            _imageSize = texture.height / sprite.pixelsPerUnit;
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed);
            if (_mainCamera.position.y - transform.position.y >= _imageSize)
            {
                var offsetPosition = (_mainCamera.position.y - transform.position.y) % _imageSize;
                transform.position = new Vector3(_mainCamera.position.x, _mainCamera.position.y + offsetPosition, transform.position.z);
            }
        }

        public void IncreaseSpeed()
        {
            _speed += Random.Range(0.005f, 0.02f);
        }
    }

}
