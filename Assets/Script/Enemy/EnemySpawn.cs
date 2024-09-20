using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnRate = 2f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];
            Instantiate(enemyPrefab, spawnPoint.position,spawnPoint.rotation);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
