using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the base of the projectile.
//Literally handles all similaries to all projectiles no matter what.
public class ProjectileBase : MonoBehaviour
{
    //Grab rigidbody from Projectile themselves.
    protected Rigidbody rb;
    [SerializeField]
    protected string tagName;

    protected Vector3 curPos = Vector3.zero;

    //Handles sending damage to opposing opponent.
    [SerializeField]
    protected float damage = 0.0f;

    [SerializeField]
    protected float maxTimer = 0.0f;

    [SerializeField]
    protected float timer = 0.0f;

    protected void Awake()
    {
        timer = maxTimer;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = maxTimer;
            gameObject.SetActive(false);
        }
    }
    protected virtual void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(tagName))
        {
            if (tagName == "Player")
            {
                collision.gameObject.SendMessage("ReceiveDamage", damage);
            }
            else if (tagName == "Enemy")
            {
                print(collision.gameObject.name);
                collision.gameObject.SendMessage("ReceiveDamage", damage);
            }
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        timer = maxTimer;
    }

    private void OnDisable()
    {
        timer = maxTimer;
        rb.velocity = Vector3.zero;
    }
}
