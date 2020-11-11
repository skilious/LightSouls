﻿using System.Collections;
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
                collision.gameObject.SendMessage("ReceiveDamage", damage);
            }
        }
    }
}
