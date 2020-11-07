using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abandoned script by Tai. For now.
public class Barrier : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.position;
    }
}
