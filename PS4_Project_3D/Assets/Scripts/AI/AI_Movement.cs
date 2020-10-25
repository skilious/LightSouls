using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour
{
    protected float timer = 0;

    private Transform playerPos;
    private List<Transform> waypoints;

    [SerializeField]
    private int waypoint_count;

    [SerializeField]
    private int rangeMin, rangeMax;

    protected int randDestination;
    NavMeshAgent aiAgent;

    private void Awake()
    {
        aiAgent = gameObject.GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        waypoints = new List<Transform>();
        for (int i = 0; i < waypoint_count; i++)
        {
            waypoints.Add(GameObject.Find("Waypoint_" + i).GetComponent<Transform>());
        }
        SetDestination(waypoints[0].position);
    }
    private void Update()
    {
        float distBetweenPlayer = Vector3.Distance(playerPos.position, transform.position);
        if (distBetweenPlayer <= 10.0f)
        {
            transform.LookAt(new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z));
        }
    }

    void SetDestination(Vector3 targetPos)
    {
        if (playerPos != null)
        {
            aiAgent.SetDestination(targetPos);
        }
    }

    protected void RandomPatrol()
    {
        //Timer uses deltaTime to count down.
        timer -= Time.deltaTime;
        float distanceBetweenWayPoints = Vector3.Distance(waypoints[randDestination].position, transform.position);
        //IF statement condition - Timer hits below zero.
        if (timer < 0 && distanceBetweenWayPoints < 1.0f)
        {
            randDestination = Random.Range(rangeMin, rangeMax);
            print("Next destination: " + waypoints[randDestination].position);
            SetDestination(waypoints[randDestination].position);
            //Randomises the timer between 1.0f and 5.0f.
            timer = Random.Range(10.0f, 15.0f);
        }
    }
}
