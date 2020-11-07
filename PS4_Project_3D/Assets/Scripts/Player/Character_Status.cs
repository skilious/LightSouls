using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script
public class Character_Status : MonoBehaviour
{
    public float curHealth = 0f;
    public float healthHit;
    public float maxHealth = 100.0f;

    public static int curCapacity, maxCapacity = 100, capacityClip = 999;
    public int level;
    void Start()
    {
        curHealth = 100.0f;
        healthHit = curHealth;
    }

    protected virtual void Update()
    {
        if (healthHit < curHealth)
        {
            curHealth = Mathf.Lerp(curHealth, healthHit, 5.0f * Time.deltaTime); //Smooth transition which later used w/ slider UI.
        }
        else if(healthHit >= curHealth)
        {
            curHealth = Mathf.RoundToInt(healthHit); //Rounds it once it hits curHealth
        }
    }

    public void ReceiveDamage(float dmg)
    {
        //Simple damage receiver.
        healthHit -= dmg;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Just increases the amount of clips you have in your inventory.
        if(other.gameObject.CompareTag("AmmoClip"))
        {
            capacityClip += 10;
            Destroy(other.gameObject);
        }
    }
}
