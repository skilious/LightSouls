using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBossProj : EnemyAI
{
    /*TODO: 
    Create three projectiles at three different locations DONE
    Shoot in their own projectiles at their own locations DONE
    Aim directly towards the player's last location. DONE
    Adjustable firerates 

    OBJ_Pooling support - Reference three GameObjects as Projectiles. DONE

    Enumerator abilities
    */
    private enum Abilities
    {
        basic,
        worldspread,
        frontalspread
    };

    [SerializeField] private Vector3[] projectilePosition;
    [SerializeField] private Transform[] projectileSpawn;

    private int pattern = 0;
    private Abilities ability;
    private float timer = 0.0f;
    protected override void Start()
    {
        base.Start();
    }
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
        StartCoroutine(Projectile());
    }

    private IEnumerator Projectile()
    {
        WaitForSeconds wait = new WaitForSeconds(0.3f);
        if(timer >= 5.0f)
        {
            int abilitySwap = Random.Range(0, 3);
            ability = (Abilities)abilitySwap;
            timer = 0.0f;
        }
        switch (ability)
        {
            case Abilities.basic:
                {
                    for (int i = 0; i < projectileSpawn.Length; i++)
                    {
                        int randomThrow = Random.Range(0, 3);
                        projectilePosition[i] = projectileSpawn[randomThrow].position;
                        switch (randomThrow)
                        {
                            case 0:
                                {
                                    GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("EnemyProjectile");
                                    obj.SetActive(true);
                                    obj.transform.position = projectilePosition[i];
                                    obj.transform.LookAt(player.transform.position);
                                    Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
                                    cloneRB.AddForce(obj.transform.forward * 500.0f, ForceMode.Acceleration);
                                    break;
                                }
                            case 1:
                                {
                                    GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("Waterball");
                                    obj.SetActive(true);
                                    obj.transform.position = projectilePosition[i];
                                    obj.transform.LookAt(player.transform.position);
                                    Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
                                    cloneRB.AddForce(obj.transform.forward * 750.0f, ForceMode.Acceleration);
                                    break;
                                }
                            case 2:
                                {
                                    GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("EnemyProjectile");
                                    obj.SetActive(true);
                                    obj.transform.position = projectilePosition[i];
                                    obj.transform.LookAt(player.transform.position);
                                    Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
                                    cloneRB.AddForce(obj.transform.forward * 350.0f, ForceMode.Acceleration);
                                    break;
                                }
                        }
                        yield return wait;
                    }
                    yield return null;
                    break;
                }
            case Abilities.frontalspread:
                {
                    float spread = -90.0f;
                    for (int i = 0; i < 10; i++)
                    {
                        GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("WideLaser");
                        obj.SetActive(true);
                        obj.transform.position = transform.position + transform.up * 0.5f;
                        obj.transform.rotation = transform.rotation;
                        obj.transform.Rotate(0, spread, 0);
                        Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
                        cloneRB.AddForce(obj.transform.forward * 350.0f, ForceMode.Acceleration);
                        spread += 18.0f;
                    }
                    break;
                }
            case Abilities.worldspread:
                {
                    if(pattern == 0)
                    {
                        float spread = -360.0f;
                        for (int i = 0; i < 10; i++)
                        {
                            GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("EnemyProjectile");
                            obj.SetActive(true);
                            obj.transform.position = transform.position + transform.up * 0.5f; ;
                            obj.transform.rotation = Quaternion.identity;
                            obj.transform.Rotate(0, spread, 0);
                            Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
                            cloneRB.AddForce(obj.transform.forward * 350.0f, ForceMode.Acceleration);
                            spread += 36.0f;
                        }
                        pattern++;
                    }
                    else if(pattern == 1)
                    {
                        float spread = 15.0f;
                        for (int i = 0; i < 10; i++)
                        {
                            GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("EnemyProjectile");
                            obj.SetActive(true);
                            obj.transform.position = transform.position + transform.up * 0.5f; ;
                            obj.transform.rotation = Quaternion.identity;
                            obj.transform.Rotate(0, spread, 0);
                            Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
                            cloneRB.AddForce(obj.transform.forward * 350.0f, ForceMode.Acceleration);
                            spread += 36.0f;
                        }
                        pattern--;
                    }
                    break;
                }
        }
    }
}
