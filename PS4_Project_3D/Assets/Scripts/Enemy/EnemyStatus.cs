using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script
public class EnemyStatus : Popup_Text
{
    [SerializeField]
    private float duration = 0.0f;

    [SerializeField] private bool isAlive = true;
    [SerializeField] protected bool isInvincible = false;

    public float maxHealth;
    public float curHealth;

    private Vector3 spawnPos;

    [SerializeField] private GameObject soulEssence;

    [SerializeField] private Material dissolve;
    [SerializeField] private MeshRenderer rend;
    void Start()
    {
        spawnPos = transform.position;
    }

    void Update()
    {
        //print("Object:  " + gameObject.name + "'s health: " + curHealth);
        aliveStatus();
    }

    public float ReceiveDamage(float dmg)
    {
        //Damage receiver
        curHealth -= dmg; 
        //objPrefab is from Popup_Text.
        if (damageObjPrefab)
        {
            damageObjPrefab.GetComponent<TextMesh>().text = dmg.ToString(); //Grabs the variable from other class that its inheriting.
            damageObjPrefab.GetComponent<TextMesh>().color = color; //To set values such as this.
            ShowFloatingText(); //From inherited class that instantiates the text as prefab.
        }
        return curHealth;
    }

    void aliveStatus()
    {
        //Check if its alive and is not invincible.
        if (isAlive && !isInvincible)
        {
            //it'll proceed death normally.
            if (curHealth < 0)
            {
                Instantiate(soulEssence, transform.position, Quaternion.identity);
                StartCoroutine(Dissolve());
                isAlive = false;
            }
        }
        //Otherwise, if its invincible, it'll respawn indefinitely no matter how many times you kill it.
        else if (isInvincible)
        {
            if (curHealth < 0)
            {
                transform.position = spawnPos;
                curHealth = maxHealth;
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            //This disables the invincibility
            isInvincible = false;
            print("All enemies are no longer invincible");
        }
        //If its not alive anymore, destroy it.
        if (!isAlive)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        Wave_System.enemiesLeft--;
        Destroy(gameObject, 1.0f);
    }
    public void Invincibility()
    {
        isInvincible = true;
    }

    public void DisableInvincibility()
    {
        isInvincible = false;
    }

    IEnumerator Dissolve()
    {
        float dissolveValue = 0.0f;
        rend.material = dissolve;
        while(dissolveValue < 1.0f)
        {
            print(dissolveValue);
            dissolveValue += Time.deltaTime / duration;
            dissolveValue = Mathf.Clamp01(dissolveValue);
            dissolve.SetFloat("_Timer", dissolveValue);
            yield return null;
        }
    }
}
