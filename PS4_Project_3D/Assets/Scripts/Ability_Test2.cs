using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Test2 : MonoBehaviour
{
    public GameObject grenade;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameObject cloneGrenade = Object_Pooling.SharedInstance.GetPooledObject("Grenade");
            cloneGrenade.SetActive(true);
            cloneGrenade.transform.position = transform.position + transform.forward * 2.0f;
            cloneGrenade.transform.rotation = Quaternion.identity;
            Rigidbody cloneRB = cloneGrenade.GetComponent<Rigidbody>();
            cloneRB.AddForce(transform.forward * 500.0f + transform.up * 100.0f, ForceMode.Acceleration);
        }
    }
}
