using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProj : EnemyAI
{
    public void StartAttack()
    {
        InvokeRepeating("BasicAttack", attackTimer, repeatTimer);
    }
    public void StopAttack()
    {
        //Cancel attack once it reaches a certain distance.
        CancelInvoke("BasicAttack");
    }

    private void BasicAttack()
    {
        GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("EnemyProjectile");
        obj.SetActive(true);
        obj.transform.position = transform.position + transform.forward * 1.0f;
        //obj.transform.LookAt(player.transform.position);
        Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
        cloneRB.AddForce(transform.forward * 350.0f, ForceMode.Acceleration);
    }
   
}
