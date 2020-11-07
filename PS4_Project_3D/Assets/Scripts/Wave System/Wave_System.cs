using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_System : MonoBehaviour
{
    public List<GameObject> enemies; //Types of enemies

    public List<GameObject> enemySpawn; //Waypoints
    public static int enemiesLeft;

    public bool beginWave = false;
    public bool enableInvincibility = false; //Enables invincibility for each enemy thats being spawned in.
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) //Simple, Press T on keyboard. It starts the wave.
        {
            print("Wave has begun!");
            print("Your task is to survive indefinitely!");
            beginWave = true;
        }
        //Starts the wave.
        if(beginWave)
        {
            StartWave();
        }
    }

    private void StartWave()
    {
        //Sets the boolean to false to prevent spamming for loop.
        beginWave = false;
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject enemy = Instantiate(enemies[i], enemySpawn[i].transform.position, enemySpawn[i].transform.rotation);
            //Enable invincibility on the enemies
            if(enableInvincibility)
            {
                enemy.GetComponent<EnemyStatus>().Invincibility();
            }
            //Checking if they are spawning correctly.
            //print(i + " " + enemy.name + " has spawned!");
        }
        //set the enemiesLeft from enemies.Count.
        enemiesLeft = enemies.Count;
    }
}
