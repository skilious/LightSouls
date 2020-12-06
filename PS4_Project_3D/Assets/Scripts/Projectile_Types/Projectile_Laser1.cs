﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Laser1 : ProjectileBase
{
    [SerializeField]
    private float laserRange = 0.0f;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        float smoothness = Mathf.Lerp(0.0f, laserRange, Time.deltaTime * 5.0f);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, smoothness);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameObject onHit = Instantiate(onHitPrefab, transform.position, transform.rotation);
            Destroy(onHit, 0.25f);
            other.gameObject.SendMessage("ReceiveDamage", damage);
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject onHit = Instantiate(onHitPrefab, transform.position, transform.rotation);
            Destroy(onHit, 0.25f);
            other.gameObject.SendMessage("ReceiveDamage", damage);
        }
    }
    protected new void OnEnable()
    {
        base.OnEnable();
        transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
    }
}
