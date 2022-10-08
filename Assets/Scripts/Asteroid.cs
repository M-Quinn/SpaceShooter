using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] GameObject _explosionVFX;
    float _rotSpeed = 17.0f;
    float _moveSpeed = 1.5f;
    Vector3 _finishedLocation = new Vector3(0,2,0);

    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] CircleCollider2D _collider;
    [SerializeField] new Transform transform;//Just replaces every transform with child
    [SerializeField] TextMeshProUGUI _text_Wave;

    public static Action StartNextRound;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _rotSpeed * Time.deltaTime));
        var step = _moveSpeed * Time.deltaTime; // calculate distance to move
        //Need to only move the parent and rotate the child
        gameObject.transform.position = Vector3.MoveTowards(transform.position, _finishedLocation, step);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.transform.gameObject);
        StartCoroutine(Explosion());
        
        //destroy asteroid
        //invoke start next round
    }

    IEnumerator Explosion() {
        GameObject explosion = Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        _collider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(2.5f);
        Destroy(explosion);
        Destroy(gameObject);
    }

    public void SetWaveText(int waveNum) {
        _text_Wave.text = "WAVE " + waveNum.ToString();
    }
}
