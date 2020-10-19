using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Projectile : MonoBehaviour
{
    public enum Boomerang_State
    {
        activate,
        returning
    };

    public enum Abilities
    {
        boomerang,
        grenade
    };
    GameObject instantiateObj;
    private float curDistance;
    private Boomerang_State ability_state;
    public Abilities ability;
    public static bool instantiated = false;

    public float timer = 0.0f;
    public float maxTimer;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > maxTimer && timer <= maxTimer + 0.1f && ability_state != Boomerang_State.activate)
        {
            ability_state = Boomerang_State.activate;
        }
        if(ability == Abilities.boomerang)
        {
            Boomerang();
        }
        
        if(ability == Abilities.grenade)
        {
            if (timer > maxTimer)
            {
                timer = 0;
                GameObject cloneGrenade = Object_Pooling.SharedInstance.GetPooledObject("Grenade");
                Projectile_Col projScript = cloneGrenade.GetComponent<Projectile_Col>();
                projScript.damage = 10.0f;
                cloneGrenade.SetActive(true);
                cloneGrenade.transform.position = transform.position + transform.forward * 2.0f;
                cloneGrenade.transform.rotation = transform.rotation;
                Rigidbody cloneRB = cloneGrenade.GetComponent<Rigidbody>();
                cloneRB.AddForce(transform.forward * 500.0f + transform.up * 100.0f, ForceMode.Acceleration);
            }
        }
    }

    void Boomerang()
    {
        switch (ability_state)
        {
            case Boomerang_State.activate:
                {
                    if (!instantiated)
                    {
                        instantiateObj = Object_Pooling.SharedInstance.GetPooledObject("Projectile"); 
                        Projectile_Col projScript = instantiateObj.GetComponent<Projectile_Col>();
                        projScript.damage = 20.0f;
                        instantiateObj.SetActive(true);
                        instantiateObj.transform.position = transform.position;
                        instantiateObj.transform.rotation = transform.rotation;
                        instantiated = true;
                    }
                    Rigidbody cloneRb = instantiateObj.GetComponent<Rigidbody>();
                    cloneRb.AddForce(instantiateObj.transform.forward * 0.5f);
                    curDistance = Vector3.Distance(transform.position, instantiateObj.transform.position);
                    if (timer >= maxTimer + 1.0f)
                    {
                        timer = 0;
                        instantiated = false;
                        ability_state = Boomerang_State.returning;
                    }
                    break;
                }
            case Boomerang_State.returning:
                {
                    Vector3 dirRot = instantiateObj.transform.position - transform.position;
                    Rigidbody cloneRb = instantiateObj.GetComponent<Rigidbody>();
                    cloneRb.AddForce(-instantiateObj.transform.forward * 1.0f, ForceMode.Acceleration);
                    instantiateObj.transform.rotation = Quaternion.LookRotation(dirRot);
                    curDistance = Vector3.Distance(transform.position, instantiateObj.transform.position);
                    break;
                }
        }
    }
}