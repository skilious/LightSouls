using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCBase : StateMachineBehaviour
{
    public GameObject NPC;
    public GameObject target;
    protected float speed = 3.0f;
    protected float rotSpeed = 0.25f;
    protected float maxDistance = 3.0f;

    public NavMeshAgent agent; 
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        target = NPC.GetComponent<EnemyAI>().GetPlayer();
        agent = NPC.GetComponent<NavMeshAgent>();
    }
}
