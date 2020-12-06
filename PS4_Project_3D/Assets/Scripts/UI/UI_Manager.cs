using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    [SerializeField] private Text curCapacity, capacityClips, weapon;

    private Slider healthSlider;
    public GameObject healthColour;
    //private float colourChanging = 30.0f;
    //private float timer = 0;
    //private bool resetTimer = false;

    [SerializeField] private Text boss_Name;
    [SerializeField] private Slider boss_HP;
    private static float maxBossHealth = 0.0f;
    private float curBossHealth = 0.0f;
    private static bool bossTrigger = false;
    private bool stopLerp = false;
    public Character_Status characterStats;

    [SerializeField] private EnemyStatus targetBoss;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    private void Start()
    {
        boss_Name.enabled = false;
        boss_HP.gameObject.SetActive(false);
        characterStats = GameObject.Find("Player").GetComponent<Character_Status>();
        healthSlider = GameObject.FindGameObjectWithTag("healthSlider").GetComponent<Slider>();
        //float startingColour = 30.0f / 100.0f;
        //healthColour.GetComponent<Image>().material.color = Color.HSVToRGB(startingColour, 1, 1);

        if(targetBoss == null)
        {
            HideBossUI();
        }
    }
    void Update()
    {
        //Hue values
        //30 is green
        //16 is yellowish.
        //0 is red.
        healthSlider.value = characterStats.curHealth; //Grab curHealth and set it to the slider's value.
        healthSlider.maxValue = characterStats.maxHealth; //Same with maxHealth with slider's maxValue.

        //if(characterStats.curHealth > 65.0f)
        //{
        //    if (!resetTimer) //Resets the timer for lerping.
        //    {
        //        timer = 0.0f;
        //        resetTimer = true;
        //    }
        //    else if (resetTimer)
        //    {
        //        timer += 0.1f * Time.deltaTime;
        //        colourChanging = Mathf.Lerp(colourChanging, 30.0f, timer);
        //        float colourChanged = colourChanging / 100.0f;
        //        healthColour.GetComponent<Image>().material.color = Color.HSVToRGB(colourChanged, 1, 1);
        //    }
        //}
        //if (characterStats.curHealth <= 65.0f && characterStats.curHealth > 20.0f) //If its between 50 to 20, it'll change to yellow.
        //{
        //    if (!resetTimer) //Resets the timer for lerping.
        //    {
        //        timer = 0.0f;
        //        resetTimer = true;
        //    }
        //    else if (resetTimer)
        //    {
        //        timer += 0.1f * Time.deltaTime;
        //        colourChanging = Mathf.Lerp(colourChanging, 16.0f, timer);
        //        float colourChanged = colourChanging / 100.0f;
        //        healthColour.GetComponent<Image>().material.color = Color.HSVToRGB(colourChanged, 1, 1);
        //    }
        //}
        //else if(characterStats.curHealth <= 30.0f) //Otherwise, change to red.
        //{
        //    if(!resetTimer) //Resets the timer for lerping.
        //    {
        //        timer = 0.0f;
        //        resetTimer = true;
        //    }
        //    else if(resetTimer)
        //    {
        //        timer += 0.1f * Time.deltaTime;
        //        colourChanging = Mathf.Lerp(colourChanging, 0.0f, timer);
        //        float colourChanged = colourChanging / 100.0f;
        //        healthColour.GetComponent<Image>().material.color = Color.HSVToRGB(colourChanged, 1, 1);
        //    }    
        //}
        WeaponList();       
        CapacityAmmo();
        BossFight();
    }

    //References from character_status and assign to these texts.
    void CapacityAmmo()
    {
        if(GameManager.enableEZmode == 1)
        {
            curCapacity.text = "EZ Mode enabled!";
            capacityClips.text = "Infinite capacity";
        }
        else
        {
            curCapacity.text = characterStats.curCapacity.ToString();
            capacityClips.text = characterStats.capacityClip.ToString();
        }
    }

    void WeaponList()
    {
        weapon.text = Projectiles.shootTypes.ToString();
    }

    void BossFight()
    {
        if(bossTrigger && targetBoss != null)
        {
            boss_HP.maxValue = maxBossHealth;
            curBossHealth = Mathf.Lerp(curBossHealth, targetBoss.curHealth, 2.5f * Time.deltaTime);
            boss_HP.value = curBossHealth;
            if(curBossHealth >= targetBoss.curHealth - 10.0f)
            {
                bossTrigger = false;
                stopLerp = true;
            }
        }

        if(!bossTrigger && stopLerp && targetBoss != null)
        {
            float bossHPValue = Mathf.Lerp(curBossHealth, targetBoss.curHealth, 1.0f * Time.deltaTime);
            boss_HP.value = bossHPValue;
        }
        else if(targetBoss == null)
        {
            HideBossUI();
        }
    }
    public float SetupBossHP(float starting_health, string bossName)
    {
        boss_Name.enabled = true;
        boss_Name.text = bossName;
        boss_HP.gameObject.SetActive(true);
        bossTrigger = true;
        return maxBossHealth = starting_health;
    }

    public void HideBossUI()
    {
        boss_Name.enabled = false;
        boss_HP.gameObject.SetActive(false);
    }

    public EnemyStatus BossAccessHP(EnemyStatus boss)
    {
        return targetBoss = boss;
    }
}
