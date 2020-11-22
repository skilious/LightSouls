using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalProj : EnemyAI
{
    public void StartAttack()
    {
        InvokeRepeating("CrystalAttack", attackTimer, repeatTimer);
    }
    public void StopAttack()
    {
        //Cancel attack once it reaches a certain distance.
        CancelInvoke("CrystalAttack");
    }

    void CrystalAttack()
    {
        GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("EnemyLaser");
        cloning.SetActive(true);
        cloning.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * 0.5f;
        cloning.transform.rotation = transform.rotation;
    }
}
