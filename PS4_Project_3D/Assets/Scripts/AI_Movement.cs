using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movement : MonoBehaviour
{
    private Vector2 targetPosition;
    private float timer;

    [SerializeField]
    private float minRange, maxRange;

    private float axisX, axisY;
    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        //Timer uses deltaTime to count down.
        timer -= Time.deltaTime;

        //IF statement condition - Timer hits below zero.
        if (timer < 0)
        {

            //Randomises the timer between 1.0f and 5.0f.
            timer = Random.Range(1.0f, 5.0f);
        }
    }
}
