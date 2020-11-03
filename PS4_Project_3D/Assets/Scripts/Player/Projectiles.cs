using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : Character_Status
{
    public GameObject obj;
    [SerializeField]
    private float BasicSpeed = 0, ShotgunSpeed = 0;

    [SerializeField]
    protected bool isReloading = false;
    [SerializeField]
    protected bool isLifestealing = false;
    public enum ShootTypes
    {
        Basic_Shots,
        Shotgun_Shots,
        Orb_Shots,
    };
    public ShootTypes shootTypes;

    private void Awake()
    {
        curCapacity = maxCapacity;
    }

    protected override void Update()
    {
        base.Update();
        if (curCapacity < 0)
        {
            curCapacity = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shootTypes = ShootTypes.Basic_Shots;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            shootTypes = ShootTypes.Shotgun_Shots;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            shootTypes = ShootTypes.Orb_Shots;
        }

        else if(Input.GetKeyDown(KeyCode.R) & !isReloading)
        {
            if (curCapacity <= maxCapacity - 1 && !isLifestealing && capacityClip > 0)
            {
                isReloading = true;
                InvokeRepeating("ReloadCapacity", 0.2f, 0.25f);
            }

            else if(isLifestealing && curCapacity <= maxCapacity - 1)
            {
                InvokeRepeating("ReloadLifesteal", 1.0f, 0.5f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && curCapacity > 0)
        {
            CancelInvoke("ReloadCapacity");
            CancelInvoke("ReloadLifesteal");
            isReloading = false;
            Vector3 getPos = transform.position + transform.forward * 2.0f;
            switch (shootTypes)
            {
                case ShootTypes.Basic_Shots:
                    {
                        curCapacity--;
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
                        if (curCapacity >= 3)
                        {
                            //Calculating the angle
                            for (int i = 0; i < 3; i++)
                            {
                                curCapacity--;
                                float spread = Random.Range(-30, 30);
                                GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("Projectile");
                                cloning.SetActive(true);
                                cloning.transform.position = getPos;
                                cloning.transform.rotation = transform.rotation;
                                cloning.transform.Rotate(0, spread, 0);
                                Rigidbody rb = cloning.gameObject.GetComponent<Rigidbody>();
                                rb.AddForce(cloning.transform.forward * ShotgunSpeed, ForceMode.Acceleration);
                            }

                        }
                        break;
                    }
                case ShootTypes.Orb_Shots:
                    {
                        GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("OrbPortal");
                        cloning.SetActive(true);
                        cloning.transform.position = transform.position + -transform.forward * 1.0f + transform.up * 2.5f;
                        cloning.transform.rotation = Quaternion.Euler(cloning.transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
                        break;
                    }
            }
        }
    }

    void ReloadCapacity()
    {
        if(curCapacity <= maxCapacity - 1 && capacityClip > 0)
        {
            //It literally just increments by 1.
            curCapacity++;
            capacityClip--;
        }
        else
        {
            isReloading = false;
            CancelInvoke("ReloadCapacity");
            print("Full capacity already!");
        }
    }

    protected void ReloadLifesteal()
    {
        if (curCapacity <= maxCapacity - 1 && curHealth >= 3.0f)
        {
            ReceiveDamage(3.0f);
            curCapacity++;
        }
        else
        {
            isReloading = false;
            CancelInvoke("ReloadLifesteal");
            print("Capacity is full or insufficient health!");
        }
    }
}
