using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField]private string powerupName;
    PlayerStats playerScript;
    // Start is called before the first frame update
    void Start()
    {
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerScript = collision.gameObject.GetComponent<PlayerStats>();

            if (powerupName == "Health")
                playerScript.ChangeTotalLives(1);
            else if (powerupName == "Shield")
            {
                playerScript.AddShield();
            }
            else if (powerupName == "Laser")
            {
                playerScript.LaserPowerUp();
            }
            Destroy(gameObject);
        }
        else if (collision.tag == "DeathTrigger")
            Destroy(gameObject);
    }


}
