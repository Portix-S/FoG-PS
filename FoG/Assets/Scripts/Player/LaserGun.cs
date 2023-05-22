using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    [SerializeField] int bulletDamage;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform minorDamageExplosion;

    Rigidbody2D bulletRb;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        bulletRb.velocity = new Vector2(0, 1) * bulletSpeed;

    }

    private void Update()
    {
        if (transform.position.y >= -9f)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "Boss")
        {
            // Show Damage
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
            Transform explosion = Instantiate(minorDamageExplosion, transform.position, transform.rotation);
            Destroy(explosion.gameObject, 0.5f);
            Destroy(gameObject);
        }
    }
}
