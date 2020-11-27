using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script
//This is the base of the projectile.
//Literally handles all similaries to all projectiles no matter what.
public class ProjectileBase : MonoBehaviour
{

    [SerializeField] protected GameObject onHitPrefab;
    //Grab rigidbody from Projectile themselves.
    protected Rigidbody rb;
    [SerializeField] protected string tagName;

    protected Vector3 curPos = Vector3.zero;

    //Handles sending damage to opposing opponent.
    [SerializeField] protected float damage = 0.0f;

    [SerializeField] protected float maxTimer = 0.0f;

    [SerializeField] protected float timer = 0.0f;

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
    //Opens out to the classes that inherit this class and overrides this function to do differently comparing to this.
    //However, if its not being overriden, this function will be called out instead.
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
                collision.gameObject.SendMessage("ReceiveDamage", damage);
            }
            GameObject onHit = Instantiate(onHitPrefab, transform.position, transform.rotation);
            Destroy(onHit, 0.25f);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            GameObject onHit = Instantiate(onHitPrefab, transform.position, transform.rotation);
            Destroy(onHit, 0.25f);
            gameObject.SetActive(false);
        }
    }
    //Automatically sets timer to maxTimer once the GameObject is active.
    // TT - Converted to virtual to fix warning in editor - See Orb_Portal (line 69) for the reference that raised the warning.
    protected virtual void OnEnable()
    {
        timer = maxTimer;
    }
    
    //Set timer to maxTimer as backup and set rigidbody's velocity to zero to prevent any unnecessary force from previous shot.
    protected virtual void OnDisable()
    {
        timer = maxTimer;
        rb.velocity = Vector3.zero;
    }
}
