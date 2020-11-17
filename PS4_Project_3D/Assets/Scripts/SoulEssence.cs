using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEssence : MonoBehaviour
{
    private Transform player;
    
    [SerializeField]
    private float speed = 0.0f;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 3.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Character_Status.soulEssence++;
            Destroy(gameObject);
        }
    }
}
