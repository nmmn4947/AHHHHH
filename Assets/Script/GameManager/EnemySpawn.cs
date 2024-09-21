using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnRate = 2f;
    private int countEnemy = 0;
    [SerializeField] private GameObject EnemyGrouop;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (countEnemy < 2)
        {
            yield return new WaitForSeconds(spawnRate);
            countEnemy++;
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position,spawnPoint.rotation);
            newEnemy.transform.SetParent(EnemyGrouop.transform);
        }
    }
}
