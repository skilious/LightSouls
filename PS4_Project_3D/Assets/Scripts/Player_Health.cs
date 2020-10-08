using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    //Deals with health and curHealth.
    //The differences between the two float values is that, lerp will be used to transition the slider value instead of instantly changing value.
    //Lerp will slowly decrease/increases to the value of its health from curHealth with time.deltaTime and how fast it decreases or increases.
    private float health;
    public static float curHealth;

    //Max health is to represent the limit of how far your health can go up.
    public float maxHealth;

    //Slider as the visualisation of the two values between maxHealth and curHealth.
    public Slider healthSlider;
    void Start()
    {
        health = maxHealth;
        curHealth = health;
    }

    void Update()
    {
        healthSlider.value = health;
        healthSlider.maxValue = maxHealth;

        if(health >= curHealth)
        {
            health = Mathf.Lerp(health, curHealth, Time.deltaTime * 2.0f);
        }
        else if(health <= curHealth)
        {
            health = Mathf.RoundToInt(curHealth);
        }
    }
}
