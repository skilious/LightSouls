using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Status : MonoBehaviour
{
    public static float curHealth = 0f;
    private float healthHit;
    public static float maxHealth = 100.0f;

    public float damage;
    void Start()
    {
        curHealth = 100.0f;
        healthHit = curHealth;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            healthHit -= damage;
            if(curHealth < 0.1f)
            {
                healthHit = 0;
                curHealth = 0;
            }
        }
        if (healthHit < curHealth)
        {
            curHealth = Mathf.Lerp(curHealth, healthHit, 5.0f * Time.deltaTime);
        }
    }
}
