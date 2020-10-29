using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_System : MonoBehaviour
{
    public List<GameObject> enemies;

    public List<GameObject> enemySpawn;
    public int enemiesLeft;

    public bool beginWave = false;
    private void Start()
    {
        enemiesLeft = enemies.Count;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
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
        beginWave = false;
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject enemy = Instantiate(enemies[i], enemySpawn[i].transform.position, enemySpawn[i].transform.rotation);
            print(i + " " + enemy.name + " has spawned!");
        }
    }
}
