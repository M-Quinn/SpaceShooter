using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    InputHandler _input;
    [SerializeField]
    GameObject _laserPrefab;

    float _speed = 5.0f;
    float _minYPos = -3.8f;
    float _maxYPos = 0.0f;
    float _rightOutOfBounds = 11.1f;
    float _leftOutOfBounds = -11.2f;
    float _laserSpawnOffset = 0.7f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(new Vector3(_input.Move.x, _input.Move.y, 0) * _speed * Time.deltaTime);
        Vector3 curPos = SetBounds(transform.position);
        transform.position = curPos;
        if (_input.Fire) {
            var posToSpawn = new Vector3(transform.position.x, transform.position.y + _laserSpawnOffset, transform.position.z);
            Instantiate(_laserPrefab, posToSpawn, Quaternion.identity);
        }

    }

    private Vector3 SetBounds(Vector3 currentPosition)
    {
        var curPos = currentPosition;
        curPos.y = Mathf.Clamp(curPos.y, _minYPos, _maxYPos);
        if (curPos.x > _rightOutOfBounds)
            curPos.x = _leftOutOfBounds + 0.1f;
        else if (curPos.x < _leftOutOfBounds)
            curPos.x = _rightOutOfBounds - 0.1f;
        return curPos;
    }
}
