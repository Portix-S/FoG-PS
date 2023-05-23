using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] float points; // Wehen displaying, change back to ints
    public float multiplier = 1;
    [SerializeField] TextMeshProUGUI pointsText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints(float amount)
    {
        points += amount;
        pointsText.text = "Points: " + points;
    }


    public void TryLoadScene()
    {
        Invoke("LoadScene", 1f);
    }


    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}
