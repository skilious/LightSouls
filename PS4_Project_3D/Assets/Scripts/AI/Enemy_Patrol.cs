using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : NPCBase
{
    private GameObject[] waypoints; //An array of waypoints.
    private int curWaypoints; //Current waypoint relating to above.

    private void Awake()
    {
        //Call every GameObject with the tag "Waypoint".
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        //Start at current Waypoint (0).
        curWaypoints = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //No existing waypoints will return null.
        if(waypoints.Length == 0)
        {
            return;
        }

        //Checks distance between current waypoint's position and NPC position if its less than 3.0f.
        if(Vector3.Distance(waypoints[curWaypoints].transform.position, NPC.transform.position) < maxDistance)
        {
            //Increments to next waypoint.
            curWaypoints += Random.Range(0, 2);
            
            if(curWaypoints >= waypoints.Length) //If current waypoint exceeds over waypoints array length, it'll reset back to 0.
            {
                curWaypoints = 0;
            }
        }
        //Rotating towards whatever current waypoint is located.
        Vector3 direction = waypoints[curWaypoints].transform.position - NPC.transform.position;
        direction.y = 0;
        //Slerp assists in smooth rotation and clamps between 0 and 1 in range.
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        NPC.transform.Translate(0, 0, Time.deltaTime * speed); //Translate to current waypoint.
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
