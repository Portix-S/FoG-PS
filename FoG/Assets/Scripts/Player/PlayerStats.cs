using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]private int health = 100;
 
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
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().TryLoadScene();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            TakeDamage(50);
            Destroy(collision.gameObject);
        }
        else if(collision.transform.tag == "Boss")
        {
            TakeDamage(50);
        }
    }


    // Fazer o Respawn do player


}
