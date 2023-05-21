using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    private int health = 100;

    public void TakeDamage(int amount)
    {
        if (health - amount > 0)
        {
            health -= amount;
        }
        else
        {
            health = 0;
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }
}
