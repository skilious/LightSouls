using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chase : NPCBase
{
    protected bool throwback = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        throwback = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 direction = target.transform.position - NPC.transform.position;
        direction.y = 0;
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        NPC.transform.Translate(0, 0, Time.deltaTime * speed); 
        if (throwback && NPC.GetComponent<EnemyAI>().boomerang != null)
        {
            NPC.GetComponent<EnemyAI>().boomerang.position = Vector3.MoveTowards(NPC.GetComponent<EnemyAI>().boomerang.position, NPC.transform.position, Time.deltaTime * 10.0f);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        throwback = false;
    }
}
