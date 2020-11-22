﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomblade_Attack : NPCBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        NPC.GetComponent<BoombladeProj>().StartAttack();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //NPC.transform.LookAt(new Vector3(target.transform.position.x, NPC.transform.position.y, target.transform.position.z)); 
        Quaternion lookTarget = Quaternion.LookRotation(target.transform.position - NPC.transform.position);
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, lookTarget, rotSpeed * Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC.GetComponent<BoombladeProj>().StopAttack();
        agent.isStopped = false;
    }
}
