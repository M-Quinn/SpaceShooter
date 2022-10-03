using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyContainer;
    [SerializeField] GameObject _enemyPrefab;
    float _secondsToWait = 5.0f;
    Vector3 positionToSpawn = Vector3.zero;

    float _topOfTheScreen = 8.0f;
    //Width of the screen
    float _minX = -9.0f;
    float _maxX = 9.0f;


    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine() {
        while (true) {
            positionToSpawn = new Vector3(Random.Range(_minX, _maxX), _topOfTheScreen, transform.position.z);
            GameObject temp = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
            temp.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(_secondsToWait);
        }
    }
}
