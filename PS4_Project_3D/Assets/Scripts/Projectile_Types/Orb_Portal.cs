using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb_Portal : ProjectileBase
{
    [SerializeField]
    private Transform target = null;

    [SerializeField]
    private float attackTime, repeatRate;
    protected override void Update()
    {
        base.Update();
        FindNearestEnemy(10.0f);
        if (!gameObject.activeInHierarchy)
        {
            CancelInvoke("OrbAttack");
        }
        if (target != null)
        {
            Vector3 rot = Quaternion.LookRotation(target.position - transform.position).eulerAngles;
            rot.x = 90.0f; rot.z = 0.0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rot), 2.0f * Time.deltaTime);
        }
    }

    protected void OrbAttack()
    {
        Vector3 getPos = target.position - transform.position;
        GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("Projectile");
        cloning.SetActive(true);
        cloning.transform.position = transform.position;
        cloning.transform.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
        Rigidbody rb = cloning.GetComponent<Rigidbody>();
        rb.AddForce(getPos * 100.0f, ForceMode.Acceleration);
    }

    private Transform FindNearestEnemy(float radius)
    {
        float minDist = Mathf.Infinity;
        Transform nearest = null;
        Collider[] cols = Physics.OverlapSphere(transform.position, radius);

        //For each collider, call out!
        foreach (Collider hit in cols)
        {
            //Checks distance between this position and collider's position.
            float dist = Vector3.Distance(transform.position, hit.transform.position);
            //If it meets the requirements below, grab its distance and set this to "Nearest" transform.
            if(dist < minDist && hit.gameObject.CompareTag("Enemy"))
            {
                print("Found you bitch! " + "GameObject: " + hit.gameObject.name);
                minDist = dist;
                nearest = hit.transform;
            }
        }

        //Returns your closest enemy!
        return target = nearest;
    }
    private void OnEnable()
    {
        InvokeRepeating("OrbAttack", attackTime, repeatRate);
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
        CancelInvoke("OrbAttack");
    }
}
