using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalProj : EnemyAI
{
    private enum Ability
    {
        Ability01,
        Ability02,
        Ability03
    };

    private int abilityTarget;
    private float timer = 0.0f;
    private Ability ability;
    public void StartAttack()
    {
        InvokeRepeating("CrystalAttack", attackTimer, repeatTimer);
    }
    public void StopAttack()
    {
        //Cancel attack once it reaches a certain distance.
        CancelInvoke("CrystalAttack");
    }
    protected override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
    }
    void CrystalAttack()
    {
        if (timer >= Random.Range(5.0f, 10.0f))
        {
            abilityTarget = Random.Range(0, 3);
            ability = (Ability)abilityTarget;
            switch (ability)
            {
                case Ability.Ability01:
                    {
                        print("Ability 01");
                        GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("EnemyLaser");
                        cloning.SetActive(true);
                        cloning.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z) + transform.forward * 0.5f;
                        cloning.transform.rotation = transform.rotation;
                        break;
                    }
                case Ability.Ability02:
                    {
                        float spread = -120.0f;
                        for (int i = 0; i < 3; i++)
                        {
                            GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("EnemyLaser");
                            cloning.SetActive(true);
                            cloning.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z) + transform.forward * 0.5f;
                            cloning.transform.rotation = transform.rotation;
                            cloning.transform.Rotate(0, spread, 0);
                            spread += 120.0f;
                        }
                        print("Ability 02");
                        break;
                    }
                case Ability.Ability03:
                    {
                        print("Ability 03! Mother fucker!");
                        float spread = -360.0f;
                        for (int i = 0; i < 6; i++)
                        {
                            GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("EnemyLaser");
                            cloning.SetActive(true);
                            cloning.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z) + transform.forward * 0.5f;
                            cloning.transform.rotation = transform.rotation;
                            cloning.transform.Rotate(0, spread, 0);
                            spread += 60.0f;
                        }
                        break;
                    }
            }
            timer = 0.0f;
        }
        GameObject clone = Object_Pooling.SharedInstance.GetPooledObject("EnemyLaser");
        clone.SetActive(true);
        clone.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * 0.5f;
        clone.transform.rotation = transform.rotation;
    }
}
