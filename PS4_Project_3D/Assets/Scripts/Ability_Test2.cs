using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Test2 : MonoBehaviour
{
    public GameObject grenade;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameObject cloneGrenade = Instantiate(grenade, transform.position + transform.forward * 2.0f, Quaternion.identity);
            Rigidbody cloneRB = cloneGrenade.GetComponent<Rigidbody>();
            cloneRB.AddForce(transform.forward * 500.0f + transform.up * 100.0f, ForceMode.Acceleration);

            Destroy(cloneGrenade, 2.0f);
        }
    }
}
