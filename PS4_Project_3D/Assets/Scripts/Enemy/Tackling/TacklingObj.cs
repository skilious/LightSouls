using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacklingObj : EnemyAI
{
    private CapsuleCollider col;
    private float time = 0.0f;
    private Vector3 getPlayerPos;

    private void Start()
    {
        col = GetComponent<CapsuleCollider>();
    }
    public void StartAttack()
    {
        InvokeRepeating("TacklingAttack", attackTimer, repeatTimer);
    }
    public void StopAttack()
    {
        //Cancel attack once it reaches a certain distance.
        CancelInvoke("TacklingAttack");
    }
    private void TacklingAttack()
    {
        float maxDistance = 0.55f;
        GameObject obj = gameObject.transform.GetChild(0).gameObject;
        time += Time.deltaTime * 1.2f;
        if (time < 3.0f)
        {
            getPlayerPos = player.transform.position + transform.forward * 2.5f;
        }
        else if (time >= 3.0f)
        {
            obj.SetActive(true);
            col.isTrigger = true;
            transform.position = Vector3.MoveTowards(transform.position, getPlayerPos, 7.0f * Time.fixedDeltaTime);
            //I hate this part the most of this whole script.
            //Calling two Raycasts in one if statement for both back and forth of the GameObject.
            if (Vector3.Distance(transform.position, getPlayerPos) < 1.5f || Physics.Raycast(transform.position, transform.forward, maxDistance) || Physics.Raycast(transform.position, -transform.forward, maxDistance))
            {
                obj.SetActive(false);
                col.isTrigger = false;
                time = 0.0f;
            }
        }
    }
}
