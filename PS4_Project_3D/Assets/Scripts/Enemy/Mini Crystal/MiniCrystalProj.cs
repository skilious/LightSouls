using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCrystalProj : EnemyAI
{
    public void StartAttack()
    {
        InvokeRepeating("Attack", attackTimer, repeatTimer);
    }

    public void StopAttack()
    {
        CancelInvoke("Attack");
    }

    private void Attack()
    {
        GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("EnemyProjectile");
        obj.SetActive(true);
        obj.transform.position = transform.position + transform.forward;
        obj.transform.rotation = Quaternion.identity;
        Rigidbody cloneRB = obj.GetComponent<Rigidbody>();
        cloneRB.AddForce(transform.forward * 250.0f, ForceMode.Acceleration);
    }
}
