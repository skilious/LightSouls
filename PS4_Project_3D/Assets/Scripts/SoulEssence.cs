using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEssence : MonoBehaviour
{
    public GameObject soulDoor;
    
    [SerializeField]
    private float speed = 0.0f;


    //void Start()
    //{
    //    soulDoor = GetComponent<SoulWallHandler>().transform;
    //}

    void Update()
    {
        transform.position = Vector3.MoveTowards(current: transform.position, soulDoor.transform.position, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BoxCollider>())
        {
            Debug.Log("Soul Absorbed");
            //Character_Status.soulEssence++; //Increments soulEssence.
            Destroy(gameObject); //Destroy this gameObject.
        }
    }
}
