using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField]private string powerupName;
    PlayerStats playerScript;
    //Rigidbody2D powerUpRb;
    //float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        //powerUpRb = GetComponent<Rigidbody2D>();
        //powerUpRb.velocity = new Vector2(0, -1) * speed;
    }
    
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerScript = collision.gameObject.GetComponent<PlayerStats>();

            if (powerupName == "Health")
                playerScript.ChangeTotalLives(1);
            else if(powerupName == "Shield")
            {

            }
            else if(powerupName == "Laser")
            {

            }
            Destroy(gameObject);
        }
    }
    //*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerScript = collision.gameObject.GetComponent<PlayerStats>();

            if (powerupName == "Health")
                playerScript.ChangeTotalLives(1);
            else if (powerupName == "Shield")
            {

            }
            else if (powerupName == "Laser")
            {

            }
            Destroy(gameObject);
        }
    }


}
