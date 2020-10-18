using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour
{
    private float timer;

    public Transform playerPos;
    public List<Transform> waypoints;
    public int waypoint_count, randDestination;
    NavMeshAgent aiAgent;
    private void Awake()
    {
        aiAgent = gameObject.GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        for (int i = 0; i < waypoint_count; i++)
        {
            waypoints.Add(GameObject.Find("Waypoint_" + i).GetComponent<Transform>());
        }
        SetDestination(waypoints[0].position);
    }
    private void Update()
    {
        //Timer uses deltaTime to count down.
        timer -= Time.deltaTime;
        float distanceBetweenWayPoints = Vector3.Distance(waypoints[randDestination].position, transform.position);
        float distBetweenPlayer = Vector3.Distance(playerPos.position, transform.position);
        //IF statement condition - Timer hits below zero.
        if (timer < 0 && distanceBetweenWayPoints < 1.0f)
        {
            randDestination = Random.Range(0, 4);
            print("Next destination: " + waypoints[randDestination].position);
            SetDestination(waypoints[randDestination].position);
            //Randomises the timer between 1.0f and 5.0f.
            timer = Random.Range(10.0f, 15.0f);
        }

        if(distBetweenPlayer <= 10.0f)
        {
            transform.LookAt(playerPos.position);
        }
    }

    void SetDestination(Vector3 targetPos)
    {
        if(playerPos != null)
        {
            aiAgent.SetDestination(targetPos);
        }
    }
}
