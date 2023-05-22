using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] clouds;

    [SerializeField]
    float spawnInterval;

    [SerializeField]
    GameObject endPoint;

    Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        Prewarm();
        Invoke("AttemptSpawn", spawnInterval);

    }

    void SpawnCloud(Vector3 startPos)
    {

        int randomIndex = Random.Range(0, clouds.Length);
        GameObject cloud = Instantiate(clouds[randomIndex]);

        float startX = Random.Range(startPos.x - 18f, startPos.x + 18f);

        cloud.transform.position = new Vector3(startX, startPos.y, 100f);

        float scale = Random.Range(1f, 4f);
        cloud.transform.localScale = new Vector2(scale, scale);

        float speed = Random.Range(16f, 32f);
        cloud.GetComponent<CloudScript>().StartFloating(speed, endPoint.transform.position.y);


    }

    void AttemptSpawn()
    {
        //check some things.
        SpawnCloud(startPos);

        Invoke("AttemptSpawn", spawnInterval);
    }

    void Prewarm()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 spawnPos = startPos + Vector3.right * (i * 2);
            SpawnCloud(spawnPos);
        }
    }
}
