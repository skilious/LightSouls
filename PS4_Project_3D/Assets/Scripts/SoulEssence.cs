using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEssence : MonoBehaviour
{
    // Event Shoutouts
    public static event Action<SoulEssence> OnSoulAbsorbed = delegate { };

    [SerializeField]
    private BoxCollider boxColliderSoulWall;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, soulDoor.transform.position, Time.deltaTime * speed);
    }
    private GameObject soulDoor;
    
    [SerializeField]
    private float speed = 0.0f;

    void Start()
    {
        soulDoor = GameObject.Find("SoulAbsorbPoint");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Changed to Compare tag because if the soulessence hits anything without a BoxCollider it returns an error.
        //if (other.GetComponent<BoxCollider>().name == "SoulWall")
        if (other.CompareTag("SoulWall"))
        {
            Debug.Log("Soul Absorbed");

            // Invoke OnSoulAbsorbed Event
            OnSoulAbsorbed(this);

            Character_Status.soulEssence++; //Increments soulEssence.
            Destroy(gameObject); //Destroy this gameObject.
        }
    }
}
