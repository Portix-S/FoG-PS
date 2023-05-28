using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] int points; // Wehen displaying, change back to ints
    string singlePlayerHighScore = "000000";
    string multiPlayerHighScore = "000000";
    private float wavePoints;
    public float multiplier = 1f;
    public float chanceMultiplier = 1f;
    public float pointsMultiplier = 1f;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] TextMeshProUGUI highScoreIngameText;
    public bool hasRequiredPoints;
    private float requiredPointToSpawnBoss = 300f;
    [SerializeField] GameObject smokeAnim;
    [SerializeField] GameObject[] powerUps;
    [SerializeField] GameObject player;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject player2Health;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject playOptions;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject howToPlayScene;
    [SerializeField] GameObject finalMenu;
    [SerializeField] GameObject newHighScore;
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject playAgainButton;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI gameOverShadowText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI scoreShadowText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI highScoreShadowText;
    public bool onMenu = true;
    private bool singlePlayer = true;
    public bool endless;
    int players;
    public int waveMultiplier;

    public void SetEndless()
    {
        endless = true;
    }

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        onMenu = false;
        menuUI.SetActive(false);
        playOptions.SetActive(false);
        player.SetActive(true);
        gameUI.SetActive(true);
        finalMenu.SetActive(false);
        newHighScore.SetActive(false);
        player.GetComponent<PlayerStats>().ResetHealth();
        if (singlePlayer)
        {
            player2.SetActive(false);
            player2Health.SetActive(false);
            waveMultiplier = 1;
            requiredPointToSpawnBoss = 400f;
            highScoreIngameText.text = "Hi " + PlayerPrefs.GetInt("SingleHighScore").ToString("000000");
            players = 1;
        }
        else
        {
            player2.SetActive(true);
            player2Health.SetActive(true);
            players = 2;
            waveMultiplier = 2;
            requiredPointToSpawnBoss = 800f;
            player2.GetComponent<PlayerStats>().ResetHealth();
            //Change Spawners
            highScoreIngameText.text = "Hi " + PlayerPrefs.GetInt("MultiHighScore").ToString("000000");
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy") ;
        foreach (GameObject enemy in enemies)
            Destroy(enemy);
        enemies = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject enemy in enemies)
            Destroy(enemy);
    }

    public void ShowPlayOptions()
    {
        menuUI.SetActive(false);
        playOptions.SetActive(true);
    }

    public void SinglePlayer()
    {
        singlePlayer = true;
        endless = false;
        StartGame();
    }

    public void MultiPlayer()
    {
        singlePlayer = false;
        endless = false;
        StartGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public IEnumerator SmokeAnim(float delay, Vector3 initialPosition)
    {
        yield return new WaitForSeconds(delay);
        GameObject smoke = Instantiate(smokeAnim, initialPosition + new Vector3(-0.08f, -0.9f, 0f), transform.rotation);
        yield return new WaitForSeconds(0.6f);
        Destroy(smoke);
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
        playOptions.SetActive(false);
        newHighScore.SetActive(false);
        points = 0;
        wavePoints = 0f;
        hasRequiredPoints = false;
        pointsText.text = "Score " + points.ToString("000000");

    }

    public void ShowFinalWindow(bool won)
    {
        Cursor.lockState = CursorLockMode.None;
        onMenu = true;
        gameUI.SetActive(false);
        finalMenu.SetActive(true);
        
        //Debug.Log(highScore + " " + PlayerPrefs.GetInt("SingleHighScore") + " " + points);
        if ((points > PlayerPrefs.GetInt("SingleHighScore") && singlePlayer)|| (points > PlayerPrefs.GetInt("MultiHighScore") && !singlePlayer))
        {
            newHighScore.SetActive(true);
            if (singlePlayer)
            {
                PlayerPrefs.SetInt("SingleHighScore", (int)points);
            }
            else
            {
                PlayerPrefs.SetInt("MultiHighScore", (int)points);
            }
        }
        singlePlayerHighScore = PlayerPrefs.GetInt("SingleHighScore").ToString("000000");
        multiPlayerHighScore = PlayerPrefs.GetInt("MultiHighScore").ToString("000000");

        scoreText.text = "Score " + points.ToString("000000");
        scoreShadowText.text = scoreText.text;
        if (singlePlayer)
        {
            
            highScoreText.text = "HighScore " + singlePlayerHighScore;
            highScoreShadowText.text = "HighScore " + singlePlayerHighScore;
        }
        else
        {
            highScoreText.text = "HighScore " + multiPlayerHighScore;
            highScoreShadowText.text = "HighScore " + multiPlayerHighScore;
        }

        if (!endless && won)
        {
            continueButton.SetActive(true);
            playAgainButton.SetActive(false);
            gameOverText.text = "You Win";
            gameOverShadowText.text = gameOverText.text;
        }
        else
        {
            continueButton.SetActive(false);
            playAgainButton.SetActive(true);
            gameOverText.text = "Game Over";
            gameOverShadowText.text = gameOverText.text;
        }
        if (!won)
        {
            points = 0;
            wavePoints = 0f;
            hasRequiredPoints = false;
        }
        pointsText.text = "Score " + points.ToString("000000");
    }

    public void AddPoints(float amount)
    {
        points += (int)amount;
        wavePoints += amount;
        pointsText.text = "Score " + points.ToString("000000");
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

    public bool CheckEndGame()
    {
        players--;
        if (players == 0)
            return true;
        else
            return false;
    }
}
