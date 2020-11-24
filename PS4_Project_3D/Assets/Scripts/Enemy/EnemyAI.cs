using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script
public class EnemyAI : MonoBehaviour
{
    //Reference EnemyStatus script
    protected EnemyStatus status;

    protected float getDistance;
    protected float curDistance = 0;
    Animator anim;
    protected GameObject player;

    [SerializeField] protected float attackTimer = 0.0f, repeatTimer = 0.0f;

    protected virtual void Start()
    {
        status = GetComponent<EnemyStatus>();
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        getDistance = Vector3.Distance(transform.position, player.transform.position);
        anim.SetFloat("Distance", getDistance);
    }

    protected float PercentageHealth()
    {
        float healthLeft = (status.curHealth / status.maxHealth) * 100.0f; //Basically 100% health with this formula.
        return healthLeft;
    }
}

//UNUSED CODES
//protected enum Attack_Types
//{
//    Grenade,
//    Boomerang,
//    Basic,
//    Tackling,
//    Shotgun,
//    Laser
//};

// [SerializedField] protected Attack_Types attack_types;

//protected void Attack()
//{
//    switch (attack_types)
//    {
//        case Attack_Types.Basic:
//            {
//                BasicAttack();
//                break;
//            }
//        case Attack_Types.Boomerang:
//            {
//                BoombladeAttack();
//                break;
//            }
//        case Attack_Types.Grenade:
//            {
//                GrenadeAttack();
//                break;
//            }
//        case Attack_Types.Tackling:
//            {
//                TacklingAttack();
//                break;
//            }
//        case Attack_Types.Shotgun:
//            {
//                ShotgunAttack();
//                break;
//            }
//        case Attack_Types.Laser:
//            {
//                AttackLaser();
//                break;
//            }
//    }
//}

//private void GrenadeAttack()
//{
//    GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("Grenade");
//    obj.SetActive(true);
//    obj.transform.position = transform.position + transform.forward * 2.0f;
//    obj.transform.rotation = transform.rotation;
//    Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
//    cloneRB.AddForce(transform.forward * 500.0f + transform.up * 100.0f, ForceMode.Acceleration);
//}

//private void BasicAttack()
//{
//    GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("EnemyProjectile");
//    obj.SetActive(true);
//    obj.transform.position = transform.position + transform.forward * 1.0f;
//    obj.transform.LookAt(player.transform.position);
//    Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
//    cloneRB.AddForce(cloneRB.transform.forward * 350.0f, ForceMode.Acceleration);
//}

//private void BoombladeAttack()
//{
//    GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("Boomerang");
//    obj.GetComponent<Projectile_Boomblade>().getSpawnPos = transform.position;
//    obj.SetActive(true);
//    obj.transform.position = transform.position;
//    obj.transform.LookAt(player.transform.position);
//    Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
//    cloneRB.AddForce(cloneRB.transform.forward * 500.0f, ForceMode.Acceleration);
//}
//private void TacklingAttack()
//{
//    float maxDistance = 0.55f;
//    GameObject obj = gameObject.transform.GetChild(0).gameObject;
//    time += Time.deltaTime * 1.2f;
//    if (time < 3.0f)
//    {
//        getPlayerPos = player.transform.position + transform.forward * 2.5f;
//    }
//    else if (time >= 3.0f)
//    {
//        obj.SetActive(true);
//        col.isTrigger = true;
//        transform.position = Vector3.MoveTowards(transform.position, getPlayerPos, 7.0f * Time.fixedDeltaTime);
//        //I hate this part the most of this whole script.
//        //Calling two Raycasts in one if statement for both back and forth of the GameObject.
//        if (Vector3.Distance(transform.position, getPlayerPos) < 1.5f || Physics.Raycast(transform.position, transform.forward, maxDistance) || Physics.Raycast(transform.position, -transform.forward, maxDistance))
//        {
//            obj.SetActive(false);
//            col.isTrigger = false;
//            time = 0.0f;
//        }
//    }
//}

//private void ShotgunAttack()
//{
//    if(PercentageHealth() >= 75.0f)
//    {
//        GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("EnemyBasic");
//        cloning.SetActive(true);
//        cloning.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) - transform.forward * 0.1f;
//        cloning.transform.rotation = transform.rotation;
//        Rigidbody rb = cloning.gameObject.GetComponent<Rigidbody>();
//        rb.AddForce(cloning.transform.forward * 350.0f, ForceMode.Acceleration);
//        print("Start off easy."); //Testing purposes.
//    }
//    else if(PercentageHealth() >= 30.0f && PercentageHealth() < 75.0f)
//    {
//        float spread = -120.0f;
//        for (int i = 0; i < 2; i++)
//        {
//            GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("EnemyBasic");
//            cloning.SetActive(true);
//            cloning.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) - transform.forward * 0.1f;
//            cloning.transform.rotation = transform.rotation;
//            cloning.transform.Rotate(0, spread, 0); //This used to be randomized until its difficult to predict. Now its fixed to have normal spreading.
//            Rigidbody rb = cloning.gameObject.GetComponent<Rigidbody>();
//            rb.AddForce(cloning.transform.forward * 500.0f, ForceMode.Acceleration);
//            spread += 240.0f;
//        }
//        print("Second attack pattern change, one more to go.");

//    }
//    else if(PercentageHealth() >= 0.0f && PercentageHealth() < 30.0f)
//    {
//        if (!finalStage)
//        {
//            finalStage = true;
//            print("Difficulty change");
//            GameObject clone = Instantiate(gameObject, transform.position + transform.right * 1.0f, transform.rotation);
//            EnemyAI setClone = clone.GetComponent<EnemyAI>();
//            setClone.status = setClone.GetComponent<EnemyStatus>();
//            setClone.finalStage = true;
//            setClone.status.curHealth = status.curHealth;
//        }
//        float spread = -120.0f;
//        for (int i = 0; i < 3; i++)
//        {
//            GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("EnemyBasic");
//            cloning.SetActive(true);
//            cloning.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) - transform.forward * 0.1f;
//            cloning.transform.rotation = transform.rotation;
//            cloning.transform.Rotate(0, spread, 0); //This used to be randomized until its difficult to predict. Now its fixed to have normal spreading.
//            Rigidbody rb = cloning.gameObject.GetComponent<Rigidbody>();
//            rb.AddForce(cloning.transform.forward * 500.0f, ForceMode.Acceleration);
//            spread += 120.0f;
//        }
//        print("Set off the hardest");
//    }
//}

//void AttackLaser()
//{
//    GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("EnemyLaser");
//    cloning.SetActive(true);
//    cloning.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * 0.5f;
//    cloning.transform.rotation = transform.rotation;
//}
