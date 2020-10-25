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

    [SerializeField]
    private Transform boomblade;

    private GameObject instantiateObj;
    private Boomerang_State ability_state;
    public Abilities ability;
    public static bool instantiated = false;

    public float timer = 0.0f;
    public float maxTimer;

    [SerializeField]
    private float maxDistance = 0.0f;
    void Update()
    { 
        if(ability != Abilities.boomerang) 
        timer += Time.deltaTime;

        if (ability == Abilities.boomerang)
        {
            Boomerang();
        }
    }

    private void FixedUpdate()
    {


        if (ability == Abilities.grenade)
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
                    boomblade.SetParent(null);
                    boomblade.position += transform.forward * Time.deltaTime * 10.0f;
                    float curDistance = Vector3.Distance(transform.position, boomblade.position);
                    print("Forward");
                    if (curDistance > maxDistance)
                    {
                        ability_state = Boomerang_State.returning;
                    }
                    break;
                }
            case Boomerang_State.returning:
                {
                    boomblade.position = Vector3.MoveTowards(boomblade.position, transform.position, Time.deltaTime * 10.0f);
                    float curDistance = Vector3.Distance(transform.position, boomblade.position);
                    print("Backward");
                    if (curDistance <= 0.75f)
                    {
                        boomblade.SetParent(transform);
                        ability_state = Boomerang_State.activate;
                    }
                    break;
                }
        }
    }
}