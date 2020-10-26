using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : StateMachineBehaviour
{
    public GameObject NPC;
    public GameObject target;
    protected float speed = 2.0f;
    protected float rotSpeed = 1.0f;
    protected float maxDistance = 3.0f;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        target = NPC.GetComponent<EnemyAI>().GetPlayer();
    }
}
