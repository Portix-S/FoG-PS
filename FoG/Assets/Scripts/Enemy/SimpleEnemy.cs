using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    private int health = 100;
    Rigidbody2D enemyRb;
    [SerializeField] float enemySpeed;
    [SerializeField] float shootingInterval = 1.5f;
    [SerializeField] GameObject bullet;

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
        if(collision.tag == "DeathTrigger")
        {
            Destroy(gameObject);
        }
    }


    private void Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 180));
    }

    void TryShooting()
    {
        Shoot();

        Invoke("TryShooting", shootingInterval);
    }

    public void TakeDamage(int amount)
    {
        if(health - amount > 0)
        {
            health -= amount;
        }
        else
        {
            health = 0;
            Destroy(gameObject);
            //Give points to player
        }
    }
}
