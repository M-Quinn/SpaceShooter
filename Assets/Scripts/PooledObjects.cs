using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjects : MonoBehaviour
{
    [Header("Object to Pool")]
    [SerializeField] GameObject _playerLaserPrefab;

    List<GameObject> _pool_PlayerLaser = new List<GameObject>();

    public GameObject GetPlayerLaser(Vector3 location) {
        return SpawnObjectFromPool(_playerLaserPrefab, _pool_PlayerLaser, location);
    }

    private GameObject SpawnObjectFromPool(GameObject objPrefab, List<GameObject> pool, Vector3 location) {
        for (int i = 0; i < pool.Count; i++) {
            if (pool[i].activeInHierarchy == false) {
                pool[i].transform.position = location;
                pool[i].gameObject.SetActive(true);
                return pool[i];
            }
        }
        GameObject obj = Instantiate(objPrefab, location, Quaternion.identity);
        pool.Add(obj);
        return obj;
    }
}
