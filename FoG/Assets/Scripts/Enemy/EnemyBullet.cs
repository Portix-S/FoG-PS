using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] int bulletDamage;
    [SerializeField] float bulletSpeed;

    public Rigidbody2D bulletRb;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        if(gameObject.layer != 11)
            bulletRb.velocity = Vector2.down * bulletSpeed;
    }

    private void Update()
    {
        if (transform.position.y <= -32f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            // Show Damage
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
