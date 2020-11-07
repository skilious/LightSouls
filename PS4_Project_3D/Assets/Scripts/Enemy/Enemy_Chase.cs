using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script learnt from tutorial.
public class Enemy_Chase : NPCBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Reference base from NPCBase to call out the same function as this.
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Set the GameObject's destination to target's position.
        agent.SetDestination(target.transform.position);
    }
}

//UNUSED CODE
//Vector3 direction = target.transform.position - NPC.transform.position;
//direction.y = 0;
//NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
//NPC.transform.Translate(0, 0, Time.deltaTime * speed); 
