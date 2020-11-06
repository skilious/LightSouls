using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : Popup_Text
{
    [SerializeField]
    private bool isAlive = true;
    [SerializeField]
    protected bool isInvincible = false;

    [SerializeField]
    protected float maxHealth;

    private float curHealth;
    private Vector3 spawnPos;
    void Start()
    {
        spawnPos = transform.position;
        curHealth = maxHealth;
    }

    void Update()
    {
        //print("Object:  " + gameObject.name + "'s health: " + curHealth);
        aliveStatus();

    }

    public float ReceiveDamage(float dmg)
    {
        curHealth -= dmg; 
        if (objPrefab)
        {
            objPrefab.GetComponent<TextMesh>().text = dmg.ToString();
            ShowFloatingText();
        }
        return curHealth;
    }

    void aliveStatus()
    {
        //Check if its alive and is not invincible.
        if (isAlive && !isInvincible)
        {
            //it'll proceed death normally.
            if (curHealth < 0)
            {
                isAlive = false;
            }
        }
        //Otherwise, if its invincible, it'll respawn indefinitely no matter how many times you kill it.
        else if (isInvincible)
        {
            if (curHealth < 0)
            {
                transform.position = spawnPos;
                curHealth = maxHealth;
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            //This disables the invincibility
            isInvincible = false;
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

    public void DisableInvincibility()
    {
        isInvincible = false;
    }
}
