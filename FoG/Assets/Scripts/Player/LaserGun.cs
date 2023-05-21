using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    [SerializeField] int bulletDamage;
    [SerializeField] float bulletSpeed;

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
        if (collision.transform.tag == "Enemy")
        {
            // Show Damage
            collision.gameObject.GetComponent<SimpleEnemy>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
