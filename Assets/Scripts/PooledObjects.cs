using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjects : MonoBehaviour
{
    [Header("Object to Pool")]
    [SerializeField] GameObject _playerLaserPrefab;

    List<GameObject> _pool_PlayerLaser = new List<GameObject>();

    int _totalSpawnedItems = 0;
    int _totalTimesPooled = 0;

    public GameObject GetPlayerLaser(Vector3 location) {
        return SpawnObjectFromPool(_playerLaserPrefab, _pool_PlayerLaser, location);
    }

    private GameObject SpawnObjectFromPool(GameObject objPrefab, List<GameObject> pool, Vector3 location) {
        Debug.LogWarning($"Total items spawned: {_totalSpawnedItems}\n" +
            $"Total times pooled: {_totalTimesPooled}");
        for (int i = 0; i < pool.Count; i++) {
            if (pool[i].activeInHierarchy == false) {
                _totalTimesPooled++;
                pool[i].transform.position = location;
                pool[i].gameObject.SetActive(true);
                return pool[i];
            }
        }
        GameObject obj = Instantiate(objPrefab, location, Quaternion.identity);
        pool.Add(obj);
        _totalSpawnedItems++;
        return obj;
    }
}
