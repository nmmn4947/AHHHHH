using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void SpawnEnemies()
    {

    }

    private void Update()
    {
        CallSpawner();
    }

    private IEnumerator CallSpawner()
    {
        int current = 1;
        int last = 0;
        while (true)
        {
            yield return new WaitForSeconds(10f);
            SpawnEnemies();
            var next = current + last;
            last = current;
            current = next;
        }
    }
}
