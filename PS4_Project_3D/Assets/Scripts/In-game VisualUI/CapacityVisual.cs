using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Tai's script.
public class CapacityVisual : MonoBehaviour
{
    [SerializeField]
    private Image capacityCircle, fireRate;
    Character_Status charStats;
    private void Awake()
    {
        charStats = GameObject.Find("Player").GetComponent<Character_Status>();
    }
    private void Update()
    {
        //Grab its current ammo capacity and
        //divide maxCapacity w/ multiplications by 360 and dividing by 360 to create a whole 360 degree circle.
        float amount = charStats.curCapacity / (float)charStats.maxCapacity * 360.0f / 360.0f;
        capacityCircle.fillAmount = amount; //Apply it to the fillAmount.
        float fireRateAmount = Projectiles.fireRate / 1.0f * 360.0f / 360.0f;
        fireRate.fillAmount = fireRateAmount;
    }
}
