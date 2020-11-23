using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text curCapacity, capacityClips, weapon;

    private Slider healthSlider;
    public GameObject healthColour;
    private float colourChanging = 30.0f;
    private float timer = 0;
    private bool resetTimer = false;

    public Character_Status characterStats;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        characterStats = GameObject.Find("Player").GetComponent<Character_Status>();
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
        healthSlider.value = characterStats.curHealth; //Grab curHealth and set it to the slider's value.
        healthSlider.maxValue = characterStats.maxHealth; //Same with maxHealth with slider's maxValue.
        if (characterStats.curHealth <= 65.0f && characterStats.curHealth > 20.0f) //If its between 50 to 20, it'll change to yellow.
        {
            timer += 0.1f * Time.deltaTime;
            colourChanging = Mathf.Lerp(colourChanging, 16.0f, timer);
            float colourChanged = colourChanging / 100.0f;
            healthColour.GetComponent<Image>().material.color = Color.HSVToRGB(colourChanged, 1, 1);
        }
        else if(characterStats.curHealth <= 30.0f) //Otherwise, change to red.
        {
            if(!resetTimer) //Resets the timer for lerping.
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
        WeaponList();       
        CapacityAmmo();
    }

    //References from character_status and assign to these texts.
    void CapacityAmmo()
    {
        curCapacity.text = characterStats.curCapacity.ToString();
        capacityClips.text = characterStats.capacityClip.ToString();
    }

    void WeaponList()
    {
        weapon.text = Projectiles.shootTypes.ToString();
    }
}
