using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] GameObject[] GunPositions;
    [SerializeField] GameObject laser;
    int currentGunPos = 0;
    public int numberOfLasers = 1;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < numberOfLasers; i++)
            {
                Instantiate(laser, GunPositions[currentGunPos].transform.position, Quaternion.Euler(0, 0, 0));
                currentGunPos++;
                if (currentGunPos > GunPositions.Length - 1)
                    currentGunPos = 0;
            }
        }
    }


}
