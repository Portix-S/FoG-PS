using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] int bulletDamage;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform minorDamageExplosion;

    public Rigidbody2D bulletRb;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        if(gameObject.layer != 11)
            bulletRb.velocity = Vector2.down * bulletSpeed;
        Destroy(gameObject, 1.5f);
    }

    private void Update()
    {
        /*
        if (transform.position.y <= -32f)
            Destroy(gameObject);
        //*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            // Show Damage
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(bulletDamage);
            Transform explosion = Instantiate(minorDamageExplosion, new Vector2(transform.position.x, transform.position.y - 0.5f), transform.rotation);
            Destroy(explosion.gameObject, 0.5f);
            Destroy(gameObject);
        }
    }

    
}
