using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Col : MonoBehaviour
{
    private Rigidbody rb;
    public float damage;
    public string tagName;
    private bool reflected = false;
    public bool grenadeSelected = false, canReflect = false;
    private float timer = 0;
    public GameObject projectiles;

    private bool enableTimer = false;

    protected private Vector3 curPos = Vector3.zero;
    public float timerToDisable = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == tagName)
        {
            Player_Health.curHealth -= damage;
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Barrier" && !reflected && canReflect)
        {
            rb.velocity = Vector3.Reflect(rb.velocity, transform.forward);
            rb.rotation = Quaternion.LookRotation(rb.velocity);
            reflected = true;
        }
    }

    private void Update()
    {
        timerToDisable += Time.deltaTime;
        if (timerToDisable >= 3.0f)
        {
            gameObject.SetActive(false);
        }
        if (grenadeSelected)
        {
            timer += Time.deltaTime;
            if(timer >= 1.95f)
            {
                for (int i = 0; i < 10; i++)
                {
                    float rand = Random.Range(0.0f, 360.0f);
                    Quaternion rot = Quaternion.AngleAxis(rand, Vector3.up);
                    GameObject explosionClone = Object_Pooling.SharedInstance.GetPooledObject("Projectile");
                    explosionClone.SetActive(true);
                    explosionClone.transform.position = curPos;
                    explosionClone.transform.rotation = rot;
                    Rigidbody cloneRB = explosionClone.GetComponent<Rigidbody>();
                    cloneRB.AddForce(explosionClone.transform.forward * 500.0f, ForceMode.Acceleration);
                    if(timerToDisable >= 4.0f)
                    {
                        cloneRB.gameObject.SetActive(false);
                    }
                }
                gameObject.SetActive(false);
                timer = 0;
            }
        }
    }

    private void LateUpdate()
    {
        curPos = transform.position;
        reflected = false;
    }

    private void OnEnable()
    {
        print("Enabled object!");
        enableTimer = true;
    }

    private void OnDisable()
    {
        print("Disabled object!");
        timerToDisable = 0;
        rb.velocity = Vector3.zero;
        enableTimer = false;
    }
}
