using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : AI_Movement
{
    private float testfloat = 1.0f;

    void Update()
    {
        
        RandomPatrol();
    }
}
