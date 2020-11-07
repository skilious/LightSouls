using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Tai's script.
public class CapacityVisual : MonoBehaviour
{
    private Image capacityCircle;

    private void Awake()
    {
        capacityCircle = GetComponent<Image>();
    }
    private void Update()
    {
        //Grab its current ammo capacity and
        //divide maxCapacity w/ multiplications by 360 and dividing by 360 to create a whole 360 degree circle.
        float amount = Character_Status.curCapacity / (float)Character_Status.maxCapacity * 360.0f / 360.0f;
        capacityCircle.fillAmount = amount; //Apply it to the fillAmount.
    }
}
