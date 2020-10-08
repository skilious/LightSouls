using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Col : MonoBehaviour
{
    private Rigidbody rb;
    public float damage;
    public string tagName;
    private bool reflected = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == tagName)
        {
            Player_Health.curHealth -= damage;
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Barrier" && !reflected)
        {
            rb.velocity = Vector3.Reflect(rb.velocity, transform.forward);
            rb.rotation = Quaternion.LookRotation(rb.velocity);
            reflected = true;
        }
    }

    private void LateUpdate()
    {
        reflected = false;
    }
}
