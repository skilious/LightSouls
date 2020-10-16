using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Boomerang : MonoBehaviour
{
    public enum Ability_State
    {
        deactivate,
        activate,
        returning
    };
    public float maxDistance;
    private GameObject instantiateObj;
    public GameObject obj;
    private float curDistance;
    private Ability_State ability_state;

    protected private bool instantiated = false;

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
                    if(!instantiated)
                    {
                        instantiateObj = Instantiate(obj, transform.position, transform.rotation);
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
            case Ability_State.deactivate:
                {
                    Destroy(instantiateObj);
                    break;
                }
            case Ability_State.returning:
                {
                    Vector3 dirRot = instantiateObj.transform.position - transform.position;
                    instantiated = false;
                    Rigidbody cloneRb = instantiateObj.GetComponent<Rigidbody>();
                    cloneRb.AddForce(-instantiateObj.transform.forward * 10.0f, ForceMode.Acceleration);
                    instantiateObj.transform.rotation = Quaternion.LookRotation(dirRot);
                    curDistance = Vector3.Distance(transform.position, instantiateObj.transform.position);
                    if (curDistance < 2.0f)
                    {
                        ability_state = Ability_State.deactivate;
                    }
                    break;
                }
        }

    }
}
