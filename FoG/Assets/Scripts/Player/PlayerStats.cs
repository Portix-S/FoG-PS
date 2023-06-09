using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EZCameraShake;
public class PlayerStats : MonoBehaviour
{
    [SerializeField]private int health;
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] SpriteRenderer thrustSprite;
    [SerializeField] Image healthBarFill;
    [SerializeField] float shieldDuration = 5f;
    [SerializeField] float laserBuffDuration = 15f;
    [SerializeField] GameObject shield;
    //[SerializeField] GameObject smokeAnim;
    [SerializeField] Transform deathExplosion;
    GameManager gm;
    WeaponSystem weapon;
    Vector3 initialPosition;
    private int totalLives = 3;
    private int maxLives = 3;
    bool hasShield;
    bool hasMultiShield;
    bool hasWeaponBuff;
    bool hasMultipleWeaponBuff;
    public bool won;
    private bool dead;
    [SerializeField] AudioSource deadAudioSource;
    // Ideias para vida do player (Curti mt esse estilo)
    // https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.freepik.com%2Fpremium-vector%2Fpixel-art-health-bar_30197933.htm&psig=AOvVaw01amKH2v7pFUoaygdX6wLA&ust=1684943593926000&source=images&cd=vfe&ved=0CA4QjRxqFwoTCND176fmi_8CFQAAAAAdAAAAABAI
    // https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.shutterstock.com%2Fpt%2Fsearch%2Fhealth-bar-game&psig=AOvVaw01amKH2v7pFUoaygdX6wLA&ust=1684943593926000&source=images&cd=vfe&ved=0CA4QjRxqFwoTCND176fmi_8CFQAAAAAdAAAAABAS
    

    
    public void TakeDamage(int amount)
    {
        if (!hasShield)
        {
            CameraShaker.Instance.ShakeOnce(2.5f, 1.2f, 0.1f, 1f);
            if (health - amount > 0)
            {
                health -= amount;
            }
            else
            {
                health = 0;
                Explode();
                // Não ira mais morrer direto, irá perder 1 "vida", tendo 3 vidas ao total;
                if (totalLives > 0)
                {
                    ChangeTotalLives(-1);
                    deadAudioSource.PlayOneShot(deadAudioSource.clip);
                    Respawn();
                }
                else
                {
                    gameObject.SetActive(false);
                    ResetStats();
                    dead = true;
                    if (gm.CheckEndGame())
                        ShowFinalMenu();
                }
            }
        }
    }
    public void Explode()
    {
        Transform explosion = Instantiate(deathExplosion, transform.position, transform.rotation);
        explosion.localScale = new Vector2(2.5f, 2.5f);
        Destroy(explosion.gameObject, 0.5f);
    }

    public void CheckIfEndless()
    {
        if (!gm.endless)
        {
            won = true;
            ShowFinalMenu();
        }
    }

    private void ShowFinalMenu()
    {
        gm.ShowFinalWindow(won);
        won = false;
        gm.gameObject.GetComponent<WaveSpawner>().Reset();
    }

    private void ResetStats()
    {
        transform.position = initialPosition;
        totalLives = 3;
        health = 2;
    }

    public void ResetHealth()
    {
        healthBarFill.fillAmount = 1f;
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
            collision.gameObject.GetComponent<EnemyHealth>().Explode();
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
        weapon = GetComponent<WeaponSystem>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }


    // Fazer o Respawn do player
    private void Respawn()
    {
        gameObject.SetActive(false);
        Invoke("ResetPosition", 1.5f);
        gm.StartCoroutine(gm.SmokeAnim(1.35f, initialPosition));
        //Invoke("SmokeAnim", 1f);
        //StartCoroutine("SmokeAnim", 1f);
    }

    public void AddMultiplier()
    {
        gm.multiplier *= 1.2f;
        gm.pointsMultiplier *= 1.2f;
        gm.ResetWavePoints();
    }



    private void ResetPosition()
    {
        transform.position = initialPosition;
        gameObject.SetActive(true);
    }

    public void AddShield()
    {
        if (hasShield)
            hasMultiShield = true;
        hasShield = true;
        shield.SetActive(true);
        Invoke("StopShield", shieldDuration);
    }

    private void StopShield()
    {
        if (!hasMultiShield)
        {
            hasShield = false;
            shield.SetActive(false);
        }
        else
            hasMultiShield = false;
    }

    public void LaserPowerUp()
    {
        if (weapon.numberOfLasers == 1)
        {
            weapon.numberOfLasers = 2;
            gm.chanceMultiplier = 1.5f;
            gm.pointsMultiplier = 1.5f;
        }
        else
        {
            weapon.numberOfLasers = 4;
            gm.chanceMultiplier = 2f;
            laserBuffDuration += 10f;
            hasMultipleWeaponBuff = true;
        }
        Invoke("StopLaserPowerUp", laserBuffDuration);
    }

    void StopLaserPowerUp()
    {
        if (!hasMultipleWeaponBuff)
        {
            weapon.numberOfLasers = 1;
            laserBuffDuration = 15f;
            gm.chanceMultiplier = 1f;
            gm.pointsMultiplier = 1f;
        }
        else
            hasMultipleWeaponBuff = false;
    }

}
