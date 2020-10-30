using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Basic : ProjectileBase
{
    [SerializeField]
    private float damageModifier = 2;

    protected float damageModify()
    {
        damage = damageModifier;
        return damage;
    }
}
