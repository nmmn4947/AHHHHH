using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnRate = 2f;
    [SerializeField] private int countEnemy = 0;
    [SerializeField] private int waves = 0;
    [SerializeField] private int maximumWave = 5;
    [SerializeField] private int maximumEnemy = 3;
    [SerializeField] private GameObject EnemyGroup;
    [SerializeField] private bool isSpawnActive = false;
    private Enemy enemy;

    void Start()
    {
        enemy = enemyPrefab.GetComponent<Enemy>();
        waves = 1;
        isSpawnActive = true;
    }

    void Update()
    {
        if (waves < maximumWave && isSpawnActive)
        {
            StartCoroutine(SpawnRoutine());
        }
        else
        {
            isSpawnActive = false;
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (countEnemy < maximumEnemy)
        {
            isSpawnActive = false;
            yield return new WaitForSeconds(spawnRate);
            countEnemy++;
            enemy.enemyState = Enemy.EnemyState.MoveToPlayer;
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            newEnemy.transform.SetParent(EnemyGroup.transform);
        }
        waves++;
        maximumEnemy += 3;
        countEnemy = 0;
        yield return new WaitForSeconds(5f);
        isSpawnActive = true;
    }
}
