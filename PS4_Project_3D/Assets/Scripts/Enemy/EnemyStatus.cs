using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private bool isAlive = true;
    private bool isInvincible = false;
    [SerializeField]
    protected int maxHealth;

    private int curHealth;

    void Start()
    {
        curHealth = maxHealth;
    }

    void Update()
    {
        if(isAlive)
        {
            if(curHealth == 0)
            {
                isAlive = false;
            }
        }
        else if(isInvincible)
        {
            curHealth = maxHealth;
        }

        if(!isAlive)
        {
            Destroy(gameObject);
        }
    }
}
