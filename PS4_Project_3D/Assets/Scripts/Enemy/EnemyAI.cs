using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script
public class EnemyAI : MonoBehaviour
{
    protected enum Attack_Types
    {
        Grenade,
        Boomerang,
        Basic,
        Tackling
    };

    protected float getDistance;
    private float time = 0.0f;

    Animator anim;
    private CapsuleCollider col;
    private GameObject player;
    private Vector3 getPlayerPos, getPos;

    [SerializeField]
    protected Attack_Types attack_types;

    [SerializeField]
    private float attackTimer = 0.0f, repeatTimer = 0.0f;
    protected float curDistance = 0;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
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
                    BoombladeAttack();
                    break;
                }
            case Attack_Types.Grenade:
                {
                    GrenadeAttack();
                    break;
                }
            case Attack_Types.Tackling:
                {
                    TacklingAttack();
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
        obj.SetActive(true);
        obj.transform.position = transform.position + transform.forward * 2.0f;
        obj.transform.rotation = transform.rotation;
        Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
        cloneRB.AddForce(transform.forward * 500.0f + transform.up * 100.0f, ForceMode.Acceleration);
    }

    private void BasicAttack()
    {
        GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("EnemyProjectile");
        obj.SetActive(true);
        obj.transform.position = transform.position + transform.forward * 1.0f;
        obj.transform.LookAt(player.transform.position);
        Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
        cloneRB.AddForce(cloneRB.transform.forward * 500.0f, ForceMode.Acceleration);
    }

    private void BoombladeAttack()
    {
        GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("Boomerang");
        obj.GetComponent<Projectile_Boomblade>().getSpawnPos = transform.position;
        obj.SetActive(true);
        obj.transform.position = transform.position;
        obj.transform.LookAt(player.transform.position);
        Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
        cloneRB.AddForce(cloneRB.transform.forward * 500.0f, ForceMode.Acceleration);
    }
    private void TacklingAttack()
    {
        time += Time.deltaTime * 1.2f;
        if(time < 3.0f)
        {
            getPos = transform.position;
            getPlayerPos = player.transform.position + transform.forward * 3.0f;
        }
        else if(time >= 3.0f)
        {
            col.isTrigger = true;
            transform.position = Vector3.MoveTowards(transform.position, getPlayerPos, 3.5f * Time.fixedDeltaTime);
            if(Vector3.Distance(transform.position, getPlayerPos) < 1.0f)
            {
                col.isTrigger = false;
                time = 0.0f;
            }
        }
    }
    void Update()
    {
        getDistance = Vector3.Distance(transform.position, player.transform.position);
        anim.SetFloat("Distance", getDistance);
    }
}
