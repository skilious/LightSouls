using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script learnt from tutorial.
public class Enemy_Attack : NPCBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Reference base as NPCBase's version of OnStateEnter.
        base.OnStateEnter(animator, stateInfo, layerIndex);
        NPC.GetComponent<EnemyAI>().StartAttack(); //Grab NPC's EnemyAI to use function "StartAttack".
        agent.isStopped = true; //Stops the ai from setting destination back.
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Staares at the target's position. (Player's position)
        NPC.transform.LookAt(new Vector3(target.transform.position.x, NPC.transform.position.y, target.transform.position.z));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Stops attacking once it gets out of this state
        NPC.GetComponent<EnemyAI>().StopAttack();
        agent.isStopped = false; //Resumes the ai after attacking.
    }
}
