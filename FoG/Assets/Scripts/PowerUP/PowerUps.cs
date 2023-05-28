using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField]private string powerupName;
    PlayerStats playerScript;
    AudioSource audioSource;
    [SerializeField] AudioClip powerUpClip;

    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            //audioSource.Play();
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
            audioSource = GameObject.FindGameObjectWithTag("PowerUpAudio").GetComponent<AudioSource>();
            audioSource.PlayOneShot(powerUpClip);
            Destroy(gameObject);
        }
        else if (collision.tag == "DeathTrigger")
            Destroy(gameObject);
    }


}
