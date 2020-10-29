using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    protected enum Attack_Types
    {
        Grenade,
        Boomerang,
        Basic
    };

    Animator anim;
    
    private GameObject player;

    [SerializeField]
    protected Attack_Types attack_types;

    public Transform boomerang;

    [SerializeField]
    private float attackTimer = 0.0f, repeatTimer = 0.0f;
    private float curDistance = 0;

    [SerializeField]
    private bool boomerangRetrieve;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        if (boomerang == null) return;
    }
    protected void Attack()
    {
        switch (attack_types)
        {
            case Attack_Types.Basic:
                {
                    BasicAttack();
                    break;
                }
            case Attack_Types.Boomerang:
                {
                    curDistance = Vector3.Distance(transform.position, boomerang.position);
                    if (!boomerangRetrieve)
                    {
                        BoomerangThrow();
                    }

                    else if (boomerangRetrieve)
                    {
                        BoomerangRetrieve();
                    }
                    break;
                }
            case Attack_Types.Grenade:
                {
                    GrenadeAttack();
                    break;
                }
        }
    }

    public void StopAttack()
    {
        //Cancel attack once it reaches a certain distance.
        CancelInvoke("Attack");
    }

    public void StartAttack()
    {
        //Repeats the function in time seconds and then, repeat every repeatRate seconds.
        InvokeRepeating("Attack", attackTimer, repeatTimer);
    }
    public GameObject GetPlayer()
    {
        return player;
    }
    private void GrenadeAttack()
    {
        GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("Grenade");
        Projectile_Col projScript = obj.GetComponent<Projectile_Col>();
        projScript.damage = 10.0f;
        obj.SetActive(true);
        obj.transform.position = transform.position + transform.forward * 2.0f;
        obj.transform.rotation = transform.rotation;
        Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
        cloneRB.AddForce(transform.forward * 500.0f + transform.up * 100.0f, ForceMode.Acceleration);
    }

    private void BasicAttack()
    {
        GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("EnemyProjectile");
        Projectile_Col projScript = obj.GetComponent<Projectile_Col>();
        projScript.damage = 10.0f;
        obj.SetActive(true);
        obj.transform.position = transform.position + transform.forward * 2.0f;
        obj.transform.rotation = transform.rotation;
        Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
        cloneRB.AddForce(transform.forward * 500.0f + transform.up * 100.0f, ForceMode.Acceleration);
    }

    private void BoomerangThrow()
    {
        boomerang.position += transform.forward * Time.deltaTime * 3.0f;
        // print("Forward");
        if (curDistance > 7.5f)
        {
            boomerang.SetParent(transform);
            boomerangRetrieve = true;
        }
    }

    private void BoomerangRetrieve()
    {
        boomerang.position = Vector3.MoveTowards(boomerang.position, transform.position, Time.deltaTime * 2.0f);
        // print("Backward");
        if (curDistance <= 0.0f)
        {
            boomerang.SetParent(null);
            boomerangRetrieve = false;
        }

    }

    void Update()
    {
        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));
    }
}
