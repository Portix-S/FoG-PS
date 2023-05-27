using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    List<Transform> laserList;
    public Transform[] laserPositions;
    public GameObject[] bossSprites;
    [SerializeField] Transform laser;
    [SerializeField] Transform bullet;
    Rigidbody2D bossRb;
    [SerializeField] float enemySpeed;
    Animator bossAnim;
    EnemyHealth bossHealth;
    bool isDying;
    bool onPosition;
    [SerializeField] GameObject[] GunPositions;
    int currentGunPos = 1;
    int numberOfBullets;
    float shootingCooldown = 1;
    [SerializeField] float angle;
    int horMove = 1;
    int moveCount;
    // Start is called before the first frame update
    void Start()
    {
        bossRb = GetComponent<Rigidbody2D>();
        bossRb.velocity = new Vector2(0, -1) * enemySpeed;
        bossAnim = GetComponent<Animator>();
        bossHealth = GetComponent<EnemyHealth>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BossTrigger" && !isDying && !onPosition)
        {
            bossHealth.immortal = false;
            //Stop
            bossRb.velocity = Vector2.zero;
            //Animation Started
            //bossAnim.applyRootMotion = false;
            bossAnim.SetBool("OnPosition", true);
            onPosition = true;
            //Shoot();
            Invoke("RepeatLaserAttack", 2f);
            Invoke("TryToShoot", 0.5f);
            Invoke("TryLateralMove", 3f);
        }
    }

    private void Shoot()
    {
        if (!isDying)
        {
            for (int i = 0; i < numberOfBullets; i++)
            {
                Transform bulletT = Instantiate(bullet, GunPositions[currentGunPos].transform.position, transform.rotation);
                Debug.Log(GunPositions[currentGunPos].transform.rotation.z);
                bulletT.GetComponent<Rigidbody2D>().velocity = new Vector2(GunPositions[currentGunPos].transform.rotation.z, -1f) * 10f;
                currentGunPos++;
                if (currentGunPos > GunPositions.Length - 1)
                    currentGunPos = 0;
            }
        }
    }

    private void TryToShoot()
    {
        numberOfBullets = Random.Range(0, GunPositions.Length);
        Shoot();
        if(!isDying)
            Invoke("TryToShoot", shootingCooldown);
    }

    public void Die()
    {
        PlayerStats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerStats.ChangeTotalLives(+1);
        playerStats.won = true;
        bossHealth.GivePoints();
        Destroy(gameObject);
    }

    void LaserAttack()
    {
        int numberOfLasers = Random.Range(1, 5);
        List<int> randomList = new List<int>();
        laserList = new List<Transform>();

        for (int i = 0; i < numberOfLasers; i++)
        {
            // Generate random numbers without duplicates
            int randPos = Random.Range(0, laserPositions.Length);
            while (randomList.Contains(randPos))
            {
                randPos = Random.Range(0, laserPositions.Length);
            }
            randomList.Add(randPos);

            //Instantiate in Random Position
            laserList.Add(Instantiate(laser, laserPositions[randPos].transform.position, transform.rotation));

        }
    }

    void RepeatLaserAttack()
    {
        if (!isDying)
        {
            LaserAttack();
        //Debug.Log("laser");
        
            Invoke("RepeatLaserAttack", 5f);
        }

    }

    void TryLateralMove()
    {
        if (!isDying)
        {
            LateralMove();

            Invoke("TryLateralMove", 9f);
        }
    }

    void LateralMove()
    {
        horMove *= -1;
        moveCount++;
        bossRb.velocity = new Vector2(horMove, 0) * enemySpeed;
        Invoke("StopLateralMove", 5f);
    }

    void StopLateralMove()
    {
        bossRb.velocity = Vector2.zero;
        if (moveCount % 2 == 0)
        {
            horMove *= -1;
            moveCount = 0;
        }
    }

    public void PlayerDeathAnim()
    {
        bossAnim.SetBool("IsDying", true);
        isDying = true;
        bossRb.velocity = Vector2.zero;
    }

    public void TryToChangeSprite()
    {
        if (bossHealth.GetHealthPercentage() > 0.75f)
            return;
        else if (bossHealth.GetHealthPercentage() > 0.50f)
        {
            bossSprites[0].SetActive(false);
            bossSprites[1].SetActive(true);
        }
        else if (bossHealth.GetHealthPercentage() > 0.25f)
        {
            bossSprites[1].SetActive(false);
            bossSprites[2].SetActive(true);
        }
        else if (bossHealth.GetHealthPercentage() > 0.10f)
        {
            bossSprites[2].SetActive(false);
            bossSprites[3].SetActive(true);
        }
        else
        {
            bossSprites[3].SetActive(false);
            bossSprites[4].SetActive(true);
        }
    }
}
