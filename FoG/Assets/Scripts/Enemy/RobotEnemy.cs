using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotEnemy : MonoBehaviour
{
    Rigidbody2D enemyRb;
    [SerializeField] float enemySpeed;
    private float shootingInterval = 0.8f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform[] bulletPos;
    int currentGunPos = 0;

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyRb.velocity = new Vector2(0, -1) * enemySpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ActiveTrigger")
        {
            Shoot();
            Invoke("TryShooting", shootingInterval);
        }
        if (collision.tag == "DeathTrigger")
        {
            Destroy(gameObject);
        }
    }


    private void Shoot()
    {
        Instantiate(bullet, bulletPos[currentGunPos].position, Quaternion.Euler(0, 0, 180));
        currentGunPos++;
        if (currentGunPos >= bulletPos.Length)
            currentGunPos = 0;
    }

    void TryShooting()
    {
        Shoot();
        if (currentGunPos == bulletPos.Length - 1)
            Invoke("TryShooting", shootingInterval*1.6f);
        else
            Invoke("TryShooting", shootingInterval);
    }

}
