using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Tai's script learnt from tutorial
public class NPCBase : StateMachineBehaviour
{
    public GameObject NPC; //This GameObject
    public GameObject target; //Player's GameObject
    //protected float speed = 3.0f; //Movement speed
    protected float rotSpeed = 10.0f; //Rotation speed (Slerp)
    protected float maxDistance = 3.0f; //Distance comparison w/ waypoints

    public NavMeshAgent agent; //NPC's NavMeshAgent.
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //All of this will be referencing from the GameObject that will be inherited from other classes.
        //Enemy_Chase, Enemy_Attack and Enemy_Control uses all this.
        NPC = animator.gameObject;
        agent = NPC.GetComponent<NavMeshAgent>();

        //Singleton script - GameManager
        target = GameManager.GetPlayer(); //GameManager.GetPlayer() is a static function where it gets the player from the GameManager script.
    }
}

//Source to tutorial: https://www.youtube.com/watch?v=NEvdyefORBo < This basically introduced me to Finite State Machines.
