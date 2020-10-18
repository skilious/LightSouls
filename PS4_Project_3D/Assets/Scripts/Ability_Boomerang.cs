﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Boomerang : MonoBehaviour
{
    public enum Ability_State
    {
        activate,
        returning
    };
    public float maxDistance;
    public GameObject obj;
    GameObject instantiateObj;
    private float curDistance;
    public static Ability_State ability_state;

    public static bool instantiated = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && ability_state != Ability_State.activate)
        {
            ability_state = Ability_State.activate;
        }
        switch(ability_state)
        {
            case Ability_State.activate:
                {
                    if (!instantiated)
                    {
                        instantiateObj = Object_Pooling.SharedInstance.GetPooledObject("Projectile");
                        instantiateObj.SetActive(true);
                        instantiateObj.transform.position = transform.position;
                        instantiateObj.transform.rotation = transform.rotation;
                        instantiated = true;
                    }
                    Rigidbody cloneRb = instantiateObj.GetComponent<Rigidbody>();
                    cloneRb.AddForce(instantiateObj.transform.forward * 2.5f, ForceMode.Acceleration);
                    curDistance = Vector3.Distance(transform.position, instantiateObj.transform.position);
                    if (curDistance > maxDistance)
                    {
                        ability_state = Ability_State.returning;
                    }
                    break;
                }
            case Ability_State.returning:
                {
                    Vector3 dirRot = instantiateObj.transform.position - transform.position;
                    Rigidbody cloneRb = instantiateObj.GetComponent<Rigidbody>();
                    cloneRb.AddForce(-instantiateObj.transform.forward * 15.0f, ForceMode.Acceleration);
                    instantiateObj.transform.rotation = Quaternion.LookRotation(dirRot);
                    curDistance = Vector3.Distance(transform.position, instantiateObj.transform.position);
                    break;
                }
        }

    }
}
