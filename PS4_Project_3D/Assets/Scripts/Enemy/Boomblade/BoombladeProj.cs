using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoombladeProj : EnemyAI
{
    public void StartAttack()
    {
        InvokeRepeating("BoombladeAttack", attackTimer, repeatTimer);
    }
    public void StopAttack()
    {
        //Cancel attack once it reaches a certain distance.
        CancelInvoke("BoombladeAttack");
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
}
