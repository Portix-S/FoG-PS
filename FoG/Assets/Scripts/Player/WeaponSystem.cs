using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] GameObject[] GunPositions;
    [SerializeField] GameObject laser;
    int currentGunPos = 0;
    public int numberOfLasers = 1;
    bool input;
    AudioSource audioSource;
    // Update is called once per frame
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(gameObject.name == "Player")
            input = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
        else
            input = Input.GetKeyDown(KeyCode.P);
        if (input)
        {
            for (int i = 0; i < numberOfLasers; i++)
            {
                Instantiate(laser, GunPositions[currentGunPos].transform.position, Quaternion.Euler(0, 0, 0));
                currentGunPos++;
                if (currentGunPos > GunPositions.Length - 1)
                    currentGunPos = 0;
            }
            audioSource.PlayOneShot(audioSource.clip);
        }
    }


}
