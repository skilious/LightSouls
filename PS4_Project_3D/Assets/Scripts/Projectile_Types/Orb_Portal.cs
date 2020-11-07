using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script
public class Orb_Portal : ProjectileBase
{
    [SerializeField]
    private Transform target = null;

    private Vector3 getPos = Vector3.zero;

    [SerializeField]
    private float attackTime, repeatRate; //Determine how fast the function is being called out.
    protected override void Update()
    {
        base.Update(); //Uses ProjectileBase's update function and include with the rest of this whole crap.
        FindNearestEnemy(10.0f);
        if (!gameObject.activeInHierarchy)
        {
            CancelInvoke("OrbAttack"); //I dont know but incase, just cancel this function if OnDisable doesn't work.
        }

        if (target != null)
        {
            Vector3 rot = Quaternion.LookRotation(target.position - transform.position).eulerAngles;
            rot.x = 90.0f; rot.z = 0.0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rot), 2.0f * Time.deltaTime); //Slowly rotate w/ slerp towards nearest enemy.
        }
    }

    protected void OrbAttack()
    {
        if(target != null) //If it exists, use the crap below.
        {
            getPos = target.position - transform.position;  //Gets the distance between each other.
            GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("Orb");
            cloning.SetActive(true); //obviously set it active as its coming from Object_Pooling script and already instantiated.
            cloning.transform.position = transform.position;
            cloning.transform.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f); //Rotating the orb but its kinda pointless.
            Rigidbody rb = cloning.GetComponent<Rigidbody>(); //Reference rigidbody from the prefab.
            rb.AddForce(getPos * 100.0f, ForceMode.Acceleration); //Yeet it to the enemy.
        }
    }

    private Transform FindNearestEnemy(float radius)
    {
        float minDist = Mathf.Infinity; //Minimum distance radius
        Transform nearest = null; //Sets Transform nearest to null at first to prevent errors throwing at your face.
        Collider[] cols = Physics.OverlapSphere(transform.position, radius); //An array of colliders that are within your radius.

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
        //Once spawned, it starts the function "OrbAttack" with attackTime and repeatRate.
        InvokeRepeating("OrbAttack", attackTime, repeatRate);
    }

    private void OnDisable()
    {
        //Makes sure it doesnt yeet across the scene quickly out of nowhere.
        rb.velocity = Vector3.zero;
        CancelInvoke("OrbAttack"); //Stops the function from continuing.
    }
}
