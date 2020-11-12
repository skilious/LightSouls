using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script
public class Character_Status : Popup_Text
{
    public float curHealth = 0f;
    public float healthHit;
    public float maxHealth = 100.0f;

    public int curCapacity, maxCapacity = 100, capacityClip = 999;
    public int level;
    protected virtual void Start()
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
        if (damageObjPrefab)
        {
            damageObjPrefab.GetComponent<TextMesh>().text = dmg.ToString(); //Grabs the variable from other class that its inheriting.
            damageObjPrefab.GetComponent<TextMesh>().color = color; //To set values such as this.
            ShowFloatingText(); //From inherited class that instantiates the text as prefab.
        }
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
