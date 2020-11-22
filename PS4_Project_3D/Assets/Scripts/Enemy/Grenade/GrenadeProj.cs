using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProj : EnemyAI
{
    public void StartAttack()
    {
        InvokeRepeating("GrenadeAtk", attackTimer, repeatTimer);
    }
    public void StopAttack()
    {
        //Cancel attack once it reaches a certain distance.
        CancelInvoke("GrenadeAtk");
    }
    private void GrenadeAtk()
    {
        GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("Grenade");
        obj.SetActive(true);
        obj.transform.position = transform.position + transform.forward * 2.0f;
        obj.transform.rotation = transform.rotation;
        Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
        cloneRB.AddForce(transform.forward * 500.0f + transform.up * 100.0f, ForceMode.Acceleration);
    }
}
