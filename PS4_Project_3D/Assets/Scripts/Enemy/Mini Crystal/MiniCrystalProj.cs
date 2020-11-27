using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCrystalProj : EnemyAI
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
        InvokeRepeating("Attack", attackTimer, repeatTimer);
    }

    public void StopAttack()
    {
        CancelInvoke("Attack");
    }

    protected override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
    }
    private void Attack()
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
        GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("EnemyProjectile");
        obj.SetActive(true);
        obj.transform.position = transform.position + transform.forward;
        obj.transform.rotation = Quaternion.identity;
        Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
        cloneRB.AddForce(transform.forward * 250.0f, ForceMode.Acceleration);
    }
}
