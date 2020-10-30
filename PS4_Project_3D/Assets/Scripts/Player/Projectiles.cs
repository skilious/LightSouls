using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public GameObject obj;
    [SerializeField]
    private float BasicSpeed = 0, ShotgunSpeed = 0;
    public enum ShootTypes
    {
        Basic_Shots,
        Shotgun_Shots,
        Laser_Shots,
    };
    public ShootTypes shootTypes;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shootTypes = ShootTypes.Basic_Shots;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            shootTypes = ShootTypes.Shotgun_Shots;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 getPos = transform.position + transform.forward * 2.0f;
            switch (shootTypes)
            {
                case ShootTypes.Basic_Shots:
                    {
                        GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("Projectile");
                        cloning.SetActive(true);
                        cloning.transform.position = getPos;
                        cloning.transform.rotation = transform.rotation;
                        Rigidbody rb = cloning.gameObject.GetComponent<Rigidbody>();
                        rb.AddForce(transform.forward * BasicSpeed, ForceMode.Acceleration);
                        break;
                    }
                case ShootTypes.Shotgun_Shots:
                    {
                        //Calculating the angle
                        for (int i = 0; i < 3; i++)
                        {
                            float spread = Random.Range(-30, 30);
                            GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("Projectile");
                            cloning.SetActive(true);
                            cloning.transform.position = getPos;
                            cloning.transform.rotation = transform.rotation;
                            cloning.transform.Rotate(0, spread, 0);
                            Rigidbody rb = cloning.gameObject.GetComponent<Rigidbody>();
                            rb.AddForce(cloning.transform.forward * ShotgunSpeed, ForceMode.Acceleration);
                        }
                        break;
                    }
            }
        }
    }
}
