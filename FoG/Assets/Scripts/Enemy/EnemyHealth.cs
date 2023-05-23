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

    private void Start()
    {
        totalHealth = health;
    }

    public void TakeDamage(int amount)
    {
        if (!immortal) 
        { 
            if (health - amount > 0)
            {
                health -= amount;
            }
            else
            {
                health = 0;
                if (gameObject.layer != 11) // Checks if it's not the boss
                {
                    Transform explosion = Instantiate(deathExplosion, transform.position, transform.rotation);
                    explosion.localScale = new Vector2(2f, 2f);
                    Destroy(explosion.gameObject, 0.5f);
                    Destroy(gameObject);
                }
                else
                {
                    bossScript.PlayerDeathAnim();
                }
                GivePoints();

            }
            if(gameObject.layer == 11)
            {
                TryChangingSprite();
            }
        }
    }


    private void GivePoints()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gm.AddPoints(amountOfPoints * gm.multiplier);
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
}
