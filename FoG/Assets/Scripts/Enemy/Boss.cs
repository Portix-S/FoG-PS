using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    List<Transform> laserList;
    public Transform[] laserPositions;
    [SerializeField] Transform laser;
    Rigidbody2D bossRb;
    [SerializeField] float enemySpeed;

    // Start is called before the first frame update
    void Start()
    {
        bossRb = GetComponent<Rigidbody2D>();
        bossRb.velocity = new Vector2(0, -1) * enemySpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BossTrigger")
        {
            Debug.Log("EnteredTrigger");
            //Stop
            bossRb.velocity = Vector2.zero;
            //Shoot();
            Invoke("RepeatLaserAttack", 5f);
        }
    }

    void LaserAttack()
    {
        int numberOfLasers = Random.Range(1, 4);
        List<int> randomList = new List<int>();
        laserList = new List<Transform>();

        for (int i = 0; i < numberOfLasers; i++)
        {
            // Generate random numbers without duplicates
            int randPos = Random.Range(0, laserPositions.Length);
            while (randomList.Contains(randPos))
            {
                randPos = Random.Range(0, laserPositions.Length);
            }
            randomList.Add(randPos);

            //Instantiate in Random Position
            laserList.Add(Instantiate(laser, laserPositions[randPos].transform.position, transform.rotation));

        }
    }

    void RepeatLaserAttack()
    {
        LaserAttack();
        //Debug.Log("laser");
        Invoke("RepeatLaserAttack", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
