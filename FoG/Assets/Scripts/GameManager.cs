using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] float points; // Wehen displaying, change back to ints
    private float wavePoints;
    public float multiplier = 1f;
    public float chanceMultiplier = 1f;
    public float pointsMultiplier = 1f;
    [SerializeField] TextMeshProUGUI pointsText;
    public bool hasRequiredPoints;
    private float requiredPointToSpawnBoss = 300f;
    [SerializeField] GameObject[] powerUps;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject howToPlayScene;
    [SerializeField] GameObject finalMenu;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI gameOverShadowText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI scoreShadowText;
    public bool onMenu = true;
    private bool singlePlayer = true;
    private bool endless;

    public void StartGame()
    {
        Time.timeScale = 1f;
        onMenu = false;
        menuUI.SetActive(false);
        player.SetActive(true);
        gameUI.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        howToPlayScene.SetActive(true);
        menuUI.SetActive(false);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        onMenu = true;
        howToPlayScene.SetActive(false);
        menuUI.SetActive(true);
        gameUI.SetActive(false);
        finalMenu.SetActive(false);
        wavePoints = 0f;
        hasRequiredPoints = false;
        points = 0f;
    }


    public void ShowFinalWindow(bool won)
    {
        onMenu = true;
        finalMenu.SetActive(true);
        if(singlePlayer)
        {
            scoreText.text = "Score " + (int)points;
            scoreShadowText.text = scoreText.text;
        }
        else
        {
            // show multiplayer stats
        }

        if(!endless && won)
        {
            gameOverText.text = "You Win";
            gameOverShadowText.text = gameOverText.text;
        }
        else
        {
            gameOverText.text = "Game Over";
            gameOverShadowText.text = gameOverText.text;
        }
        //Time.timeScale = 0f;
    }

    public void AddPoints(float amount)
    {
        points += amount;
        wavePoints += amount;
        pointsText.text = "Score " + (int)points;
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
        if (randomNum < dropChance * chanceMultiplier)
            SpawnPowerUp(spawnPos);
    }

    void SpawnPowerUp(Vector3 spawnPos)
    {
        int randomPowerUp = Random.Range(0, powerUps.Length);
        Instantiate(powerUps[randomPowerUp], spawnPos, transform.rotation);
    }
}
