using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]private int health;
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] SpriteRenderer thrustSprite;
    [SerializeField] Image healthBarFill;

    Vector3 initialPosition;
    private int totalLives = 3;
    private int maxLives = 3;
    // Ideias para vida do player (Curti mt esse estilo)
    // https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.freepik.com%2Fpremium-vector%2Fpixel-art-health-bar_30197933.htm&psig=AOvVaw01amKH2v7pFUoaygdX6wLA&ust=1684943593926000&source=images&cd=vfe&ved=0CA4QjRxqFwoTCND176fmi_8CFQAAAAAdAAAAABAI
    // https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.shutterstock.com%2Fpt%2Fsearch%2Fhealth-bar-game&psig=AOvVaw01amKH2v7pFUoaygdX6wLA&ust=1684943593926000&source=images&cd=vfe&ved=0CA4QjRxqFwoTCND176fmi_8CFQAAAAAdAAAAABAS
    public void TakeDamage(int amount)
    {
        if (health - amount > 0)
        {
            health -= amount;
        }
        else
        {
            health = 0;
            // Não ira mais morrer direto, irá perder 1 "vida", tendo 3 vidas ao total;
            if (totalLives > 0)
            {
                ChangeTotalLives(-1);
                Respawn();
            }
            else
            {
                Destroy(gameObject);
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().TryLoadScene();
            }
        }
    }

    public void ChangeTotalLives(int amount)
    {
        if(totalLives + amount <= maxLives)
            totalLives += amount;
        healthBarFill.fillAmount += 0.33f * amount;
        health = 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            TakeDamage(2);
            Destroy(collision.gameObject);
        }
        else if(collision.transform.tag == "Boss")
        {
            TakeDamage(2);
        }
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    // Fazer o Respawn do player
    private void Respawn()
    {
        gameObject.SetActive(false);
        Invoke("ResetPosition", 1.5f);
    }

    private void ResetPosition()
    {
        transform.position = initialPosition;
        gameObject.SetActive(true);
    }

}
