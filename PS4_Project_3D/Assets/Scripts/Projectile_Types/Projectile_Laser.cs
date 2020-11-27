using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Laser : ProjectileBase
{
    private GameObject player;
    private Vector3 startScale;
    private float scaling = 0.0f;
    private bool noLongerHold = false;
    private void Start()
    {
        player = GameObject.Find("Player");
        startScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
    protected override void Update()
    {
        base.Update();
        bool holding = Input.GetButton("Fire1");
        bool holdingPS4 = Input.GetButton("Shoot");
        if(holding && !noLongerHold|| holdingPS4 && !noLongerHold)
        {
            transform.position = player.transform.position + player.transform.forward * 1.5f;
            transform.rotation = player.transform.rotation;
            if(timer < 1.0f)
            {
                noLongerHold = true;
            }
        }
        else if(!holding || !holdingPS4)
        {
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f * scaling);
            rb.AddForce(transform.forward * 20.0f, ForceMode.Acceleration);
            noLongerHold = true;
        }
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
        ////Compares tagName if its correct.
        //if (collision.gameObject.CompareTag(tagName))
        //{
        //    //If its an enemy and AOE projectile, use this statement.
        //    if (tagName == "Enemy")
        //    {
        //        GameObject onHit = Instantiate(onHitPrefab, transform.position, transform.rotation);
        //        Destroy(onHit, 0.25f);
        //        collision.gameObject.SendMessage("ReceiveDamage", damage);
        //    }
        //}
        //else if (collision.CompareTag("Wall"))
        //{
        //    GameObject onHit = Instantiate(onHitPrefab, transform.position, transform.rotation);
        //    Destroy(onHit, 0.25f);
        //    gameObject.SetActive(false);
        //}
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        noLongerHold = false;
        scaling = 0.0f;
        transform.localScale = startScale;
    }
}
