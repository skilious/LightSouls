using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackling : ProjectileBase
{

    protected override void OnTriggerEnter(Collider collision)
    {
        //Compares tagName if its correct.
        if (collision.gameObject.CompareTag(tagName))
        {
            //This only gets called out if the enemy is shooting towards the player.
            if (tagName == "Player")
            {
                GameObject onHit = Instantiate(onHitPrefab, transform.position, transform.rotation);
                Destroy(onHit, 0.25f);
                collision.gameObject.SendMessage("ReceiveDamage", damage);
            }
        }
    }
}
