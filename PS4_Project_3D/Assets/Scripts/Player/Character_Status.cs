using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Status : MonoBehaviour
{
    public static float curHealth = 0f;
    private static float healthHit;
    public static float maxHealth = 100.0f;

    public int level;
    void Start()
    {
        curHealth = 100.0f;
        healthHit = curHealth;
    }

    void Update()
    {
        if (healthHit < curHealth)
        {
            curHealth = Mathf.Lerp(curHealth, healthHit, 5.0f * Time.deltaTime);
        }
        else if(healthHit >= curHealth)
        {
            curHealth = Mathf.RoundToInt(healthHit);
        }
    }

    public void ReceiveDamage(float dmg)
    {
        healthHit -= dmg;
    }
}
