using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script
public class Projectile_Boomblade : ProjectileBase
{
    [SerializeField]
    public Vector3 getSpawnPos;
    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(tagName))
        {
            if (tagName == "Player")
            {
                collision.gameObject.SendMessage("ReceiveDamage", damage);
            }
        }
    }

    protected void FixedUpdate()
    {
        base.Update();
        if(timer < 3.0f)
        {
            //Does not use rigidbody because, its literally just moving towards the target and back after it meets the target.
            transform.position = Vector3.MoveTowards(transform.position, getSpawnPos, Time.fixedDeltaTime * 20.0f); 
            if (Vector3.Distance(getSpawnPos, transform.position) < 1.0f)
            {
                gameObject.SetActive(false);
            }
        }

    }
}
