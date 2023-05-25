using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] float points; // Wehen displaying, change back to ints
    private float wavePoints;
    public float multiplier = 1;
    [SerializeField] TextMeshProUGUI pointsText;
    public bool hasRequiredPoints;
    private float requiredPointToSpawnBoss = 100f;
    [SerializeField] GameObject[] powerUps;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject menuUI;
    public bool onMenu = true;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        onMenu = false;
        menuUI.SetActive(false);
        player.SetActive(true);
        gameUI.SetActive(true);
    }

    public void AddPoints(float amount)
    {
        points += amount;
        wavePoints += amount;
        pointsText.text = "Score: " + points;
        if (wavePoints >= requiredPointToSpawnBoss * multiplier)
            hasRequiredPoints = true;
    }

    public void ResetWavePoints()
    {
        wavePoints = 0f;
        hasRequiredPoints = false;
    }

    public void TryLoadScene()
    {
        Invoke("LoadScene", 1f);
    }


    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void TrySpawningPowerUp(int dropChance, Vector3 spawnPos)
    {
        int randomNum = Random.Range(0, 100);
        if (randomNum < dropChance)
            SpawnPowerUp(spawnPos);
    }

    void SpawnPowerUp(Vector3 spawnPos)
    {
        int randomPowerUp = Random.Range(0, powerUps.Length);
        Instantiate(powerUps[randomPowerUp], spawnPos, transform.rotation);
    }
}
