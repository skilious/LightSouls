using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Manager : MonoBehaviour
{
    private Slider healthSlider;

    private void Awake()
    {
        healthSlider = GameObject.FindGameObjectWithTag("healthSlider").GetComponent<Slider>();
    }
    void Update()
    {
        healthSlider.value = Character_Status.curHealth;
        healthSlider.maxValue = Character_Status.maxHealth;
    }
}
