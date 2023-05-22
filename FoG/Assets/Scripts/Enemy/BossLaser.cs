using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    private bool playerIsOnLaser;
    private PlayerStats playerStats;
    [SerializeField] private int laserDamage;
    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerIsOnLaser = true;
            Invoke("TryLaserDamage", 0f);
            playerStats = collision.GetComponent<PlayerStats>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIsOnLaser = false;
        }
    }

    void TryLaserDamage()
    {
        if (!playerIsOnLaser)
            return;
        else
        {
            LaserDamage();
            Invoke("TryLaserDamage", 0.5f);
        }
   
    }

    void LaserDamage()
    {
        playerStats.TakeDamage(laserDamage);
    }


    public void DestroyLaser()
    {
        Destroy(GetComponentInParent<RectTransform>().gameObject);
    }
}
