using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBoss_Proj : EnemyAI
{
    private bool finalStage = false;
    public void StartAttack()
    {
        InvokeRepeating("SkullAttack", attackTimer, repeatTimer);
    }
    public void StopAttack()
    {
        //Cancel attack once it reaches a certain distance.
        CancelInvoke("SkullAttack");
    }

    protected override void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= 15.0f && status.isAlive)
        {
            UI_Manager.instance.SetupBossHP(status.maxHealth, "Skully the spinner");
            UI_Manager.instance.BossAccessHP(status);
        }
        else if (Vector3.Distance(player.transform.position, transform.position) >= 20.0f || !status.isAlive)
        {
            UI_Manager.instance.HideBossUI();
        }
        base.Update();

    }
    private void SkullAttack()
    {
        if (PercentageHealth() >= 75.0f)
        {
            GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("EnemyBasic");
            cloning.SetActive(true);
            cloning.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) - transform.forward * 0.1f;
            cloning.transform.rotation = transform.rotation;
            Rigidbody rb = cloning.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(cloning.transform.forward * 350.0f, ForceMode.Acceleration);
            print("Start off easy."); //Testing purposes.
        }
        else if (PercentageHealth() >= 30.0f && PercentageHealth() < 75.0f)
        {
            float spread = -120.0f;
            for (int i = 0; i < 2; i++)
            {
                GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("EnemyBasic");
                cloning.SetActive(true);
                cloning.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) - transform.forward * 0.1f;
                cloning.transform.rotation = transform.rotation;
                cloning.transform.Rotate(0, spread, 0); //This used to be randomized until its difficult to predict. Now its fixed to have normal spreading.
                Rigidbody rb = cloning.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(cloning.transform.forward * 500.0f, ForceMode.Acceleration);
                spread += 240.0f;
            }
            print("Second attack pattern change, one more to go.");

        }
        else if (PercentageHealth() >= 0.0f && PercentageHealth() < 30.0f)
        {
            if (!finalStage)
            {
                finalStage = true;
                print("Difficulty change");
                GameObject clone = Instantiate(gameObject, transform.position + transform.right * 1.0f, transform.rotation);
                SkullBoss_Proj setClone = clone.GetComponent<SkullBoss_Proj>();
                setClone.status = setClone.GetComponent<EnemyStatus>();
                setClone.finalStage = true;
                setClone.status.curHealth = status.curHealth;
            }
            float spread = -120.0f;
            for (int i = 0; i < 3; i++)
            {
                GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("EnemyBasic");
                cloning.SetActive(true);
                cloning.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) - transform.forward * 0.1f;
                cloning.transform.rotation = transform.rotation;
                cloning.transform.Rotate(0, spread, 0); //This used to be randomized until its difficult to predict. Now its fixed to have normal spreading.
                Rigidbody rb = cloning.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(cloning.transform.forward * 500.0f, ForceMode.Acceleration);
                spread += 120.0f;
            }
            print("Set off the hardest");
        }
    }
}
