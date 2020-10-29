using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private bool isAlive = true;
    protected bool isInvincible = false;

    protected float maxHealth;
    private float curHealth;
    private Vector3 spawnPos;
    void Start()
    {
        spawnPos = transform.position;
        maxHealth = 100.0f;
        curHealth = maxHealth;
    }

    void Update()
    {
        print("Object:  " + gameObject.name + "'s health: " + curHealth);
        aliveStatus();
    }

    public float ReceiveDamage(float dmg)
    {
        curHealth -= dmg;
        return curHealth;
    }

    void aliveStatus()
    {
        //Check if its alive and is not invincible.
        if (isAlive && !isInvincible)
        {
            //it'll proceed death normally.
            if (curHealth == 0)
            {
                isAlive = false;
            }
        }
        //Otherwise, if its invincible, it'll respawn indefinitely no matter how many times you kill it.
        else if (isInvincible)
        {
            if (curHealth == 0)
            {
                transform.position = spawnPos;
                curHealth = maxHealth;
            }
        }

        //If its not alive anymore, destroy it.
        if (!isAlive)
        {
            Wave_System.enemiesLeft--;
            Destroy(gameObject);
        }
    }

    public void Invincibility()
    {
        isInvincible = true;

    }
}
