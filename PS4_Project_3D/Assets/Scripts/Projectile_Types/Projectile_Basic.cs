using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Basic : ProjectileBase
{
    [SerializeField]
    private float damageModifier = 2;
    [SerializeField]
    protected bool isAOEProjectile;
    protected float damageModify()
    {
        damage = damageModifier;
        return damage;
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(tagName))
        {
            if (tagName == "Enemy" && isAOEProjectile)
            {
                print(collision.gameObject.name);
                collision.gameObject.SendMessage("ReceiveDamage", damage);
            }
            
            if(tagName == "Player" && !isAOEProjectile)
            {
                print(collision.gameObject.name);
                collision.gameObject.SendMessage("ReceiveDamage", damage);
                gameObject.SetActive(false);
            }
        }
    }
}
