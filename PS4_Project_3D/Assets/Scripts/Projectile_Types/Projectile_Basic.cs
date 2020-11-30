using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script
public class Projectile_Basic : ProjectileBase
{
    //Damage modifier is useless for now.
    [SerializeField]
    private float damageModifier = 2;
    [SerializeField]
    protected bool Walkthroughable; //Checks if its the other projectile that shoots a wider version of the original one.

    protected float damageModify()
    {
        damage = damageModifier;
        return damage;
    }

    //Overriding the other virtual func due to the new wider projectile.
    protected override void OnTriggerEnter(Collider collision)
    {
        //Compares tagName if its correct.
        if (collision.gameObject.CompareTag(tagName))
        {
            //If its an enemy and AOE projectile, use this statement.
            if (tagName == "Enemy" && Walkthroughable)
            {
                collision.gameObject.SendMessage("ReceiveDamage", damage);
            }
            //Otherwise, assume its the original projectile.
            else if (tagName == "Enemy" && !Walkthroughable)
            {
                collision.gameObject.SendMessage("ReceiveDamage", damage);
                gameObject.SetActive(false);
            }

            //This only gets called out if the enemy is shooting towards the player.
            if (tagName == "Player" && !Walkthroughable)
            {
                collision.gameObject.SendMessage("ReceiveDamage", damage);
                gameObject.SetActive(false);
            }
            else if (tagName == "Player" && Walkthroughable)
            {
                collision.gameObject.SendMessage("ReceiveDamage", damage);
            }

            GameObject onHit = Instantiate(onHitPrefab, transform.position, transform.rotation);
            Destroy(onHit, 0.25f);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            GameObject onHit = Instantiate(onHitPrefab, transform.position, transform.rotation);
            Destroy(onHit, 0.25f);
            gameObject.SetActive(false);
        }
    }
}
