using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEssence : MonoBehaviour
{
    // Event Shoutouts
    public static event Action<SoulEssence> OnSoulAbsorbed = delegate { };

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
        if(other.GetComponent<BoxCollider>())
        {
            Debug.Log("Soul Absorbed");

            // Invoke OnSoulAbsorbed Event
            OnSoulAbsorbed(this);

            Character_Status.soulEssence++; //Increments soulEssence.
            Destroy(gameObject); //Destroy this gameObject.
        }
    }
}
