using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Manager : MonoBehaviour
{
    private Slider healthSlider;
    public GameObject healthColour;
    private float colourChanging = 30.0f;
    private float timer = 0;
    private bool resetTimer = false;
    private void Awake()
    {
        healthSlider = GameObject.FindGameObjectWithTag("healthSlider").GetComponent<Slider>();
        float startingColour = 30.0f / 100.0f;
        healthColour.GetComponent<Image>().material.color = Color.HSVToRGB(startingColour, 1, 1);
    }
    void Update()
    {
        //Hue values
        //30 is green
        //16 is yellowish.
        //0 is red.
        healthSlider.value = Character_Status.curHealth;
        healthSlider.maxValue = Character_Status.maxHealth;
        if (Character_Status.curHealth <= 50.0f && Character_Status.curHealth > 20.0f)
        {
            timer += 0.1f * Time.deltaTime;
            colourChanging = Mathf.Lerp(colourChanging, 16.0f, timer);
            float colourChanged = colourChanging / 100.0f;
            healthColour.GetComponent<Image>().material.color = Color.HSVToRGB(colourChanged, 1, 1);
        }
        else if(Character_Status.curHealth <= 20.0f)
        {
            if(!resetTimer)
            {
                timer = 0.0f;
                resetTimer = true;
            }
            else if(resetTimer)
            {
                timer += 0.1f * Time.deltaTime;
                colourChanging = Mathf.Lerp(colourChanging, 0.0f, timer);
                float colourChanged = colourChanging / 100.0f;
                healthColour.GetComponent<Image>().material.color = Color.HSVToRGB(colourChanged, 1, 1);
            }    
        }
    }
}
