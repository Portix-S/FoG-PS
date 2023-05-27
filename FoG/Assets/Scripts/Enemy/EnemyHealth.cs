using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    private int totalHealth;
    [SerializeField] Transform deathExplosion;
    public bool immortal;
    Boss bossScript;
    [SerializeField] private int amountOfPoints;
    [SerializeField] int dropChance;
    GameManager gm;
    private void Start()
    {
        totalHealth = health;
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void TakeDamage(int amount)
    {
        if (!immortal) 
        { 
            if (health - amount > 0)
            {
                health -= amount;
                if(gameObject.layer == 11)
                    gm.TrySpawningPowerUp(1, transform.position);
            }
            else
            {
                immortal = true;
                health = 0;
                if (gameObject.layer != 11) // Checks if it's not the boss
                {
                    Transform explosion = Instantiate(deathExplosion, transform.position, transform.rotation);
                    explosion.localScale = new Vector2(2f, 2f);
                    Destroy(explosion.gameObject, 0.5f);
                    Destroy(gameObject);
                    GivePoints();
                }
                else
                {
                    bossScript.PlayerDeathAnim();
                }
            }
            if(gameObject.layer == 11)
            {
                TryChangingSprite();
            }
        }
    }


    public void GivePoints()
    {
        gm.AddPoints(amountOfPoints * gm.pointsMultiplier);
        gm.TrySpawningPowerUp(dropChance, transform.position);
    }

    public float GetHealthPercentage()
    {
        return (float)health / totalHealth;
    }

    private void TryChangingSprite()
    {
        bossScript = gameObject.GetComponent<Boss>();
        bossScript.TryToChangeSprite();
    }

    private void OnDestroy()
    {
    }
}
