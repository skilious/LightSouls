using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script
public class Projectile_Grenade : ProjectileBase
{
    protected override void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            float rand = 0.0f;
            for (int i = 0; i < 10; i++) //This spits out projectiles around the GameObject randomly.
            {
                Quaternion rot = Quaternion.AngleAxis(rand, Vector3.up);
                GameObject explosionClone = Object_Pooling.SharedInstance.GetPooledObject("EnemyProjectile");
                if(explosionClone == null)
                {
                    print("Yeet not gonna exist mate");
                    gameObject.SetActive(false);
                }
                explosionClone.SetActive(true);
                explosionClone.transform.position = transform.position;
                explosionClone.transform.rotation = rot;
                Rigidbody cloneRB = explosionClone.GetComponent<Rigidbody>();
                cloneRB.AddForce(explosionClone.transform.forward * 500.0f, ForceMode.Acceleration);
                rand += 36.0f;
            }
            gameObject.SetActive(false);
        }
    }
}
