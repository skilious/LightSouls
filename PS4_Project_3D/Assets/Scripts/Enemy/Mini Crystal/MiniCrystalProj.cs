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
        GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("EnemyLaser");
        cloning.SetActive(true);
        cloning.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z) + transform.forward * 0.5f;
        cloning.transform.rotation = transform.rotation;
    }
}
