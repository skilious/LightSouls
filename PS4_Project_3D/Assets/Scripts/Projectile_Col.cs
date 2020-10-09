using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Col : MonoBehaviour
{
    private Rigidbody rb;
    public float damage;
    public string tagName;
    private bool reflected = false;
    public bool grenadeSelected = false;
    float timer = 0;
    public GameObject projectiles;
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

    private void Update()
    {
        if(grenadeSelected)
        {
            timer += Time.deltaTime;
            if(timer >= 1.95f)
            {
                for (int i = 0; i < 10; i++)
                {
                    float rand = Random.Range(0.0f, 360.0f);
                    Quaternion rot = Quaternion.AngleAxis(rand, Vector3.up);
                    GameObject explosionClone = Instantiate(projectiles, transform.position, rot);
                    Rigidbody cloneRB = explosionClone.GetComponent<Rigidbody>();
                    cloneRB.AddForce(explosionClone.transform.forward * 500.0f, ForceMode.Acceleration);
                    Destroy(explosionClone, 2.0f);
                }
                timer = 0;
            }
        }
    }

    private void LateUpdate()
    {
        reflected = false;
    }
}
