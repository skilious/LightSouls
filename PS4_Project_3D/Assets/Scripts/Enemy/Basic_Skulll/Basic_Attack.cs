using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Attack : NPCBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        NPC.GetComponent<BasicProj>().StartAttack();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Staares at the target's position. (Player's position)
        //NPC.transform.LookAt(new Vector3(target.transform.position.x, NPC.transform.position.y, target.transform.position.z)); //LookAt extremely sharp rotation :[

        Quaternion lookTarget = Quaternion.LookRotation(target.transform.position - NPC.transform.position); //Grabs its location w/ LookRotation
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, lookTarget, rotSpeed * Time.deltaTime); //Slerp to smoothen rotation towards the Player from NPC.transform.position.
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC.GetComponent<BasicProj>().StopAttack();
        agent.isStopped = false;
    }
}
