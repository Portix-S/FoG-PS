using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject simpleEnemyPrefab;
    Vector3 startPos;
    [SerializeField] float spawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        Invoke("AttemptSpawn", spawnInterval);

    }

    void SpawnSimpleEnemy()
    {
        GameObject enemy = Instantiate(simpleEnemyPrefab);
        float startX = UnityEngine.Random.Range(startPos.x - 18f, startPos.x + 18f);

        enemy.transform.position = new Vector3(startX, startPos.y, startPos.z);
    }

    void AttemptSpawn()
    {
        //check some things.
        SpawnSimpleEnemy();

        Invoke("AttemptSpawn", spawnInterval);
    }
}
