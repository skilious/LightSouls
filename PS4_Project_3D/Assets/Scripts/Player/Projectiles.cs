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

    private float timer = 0;

    private int selection = 0;
    private bool selected = false;
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
        float dpadAxis = Input.GetAxis("SwitchWeapon_Dpad");
        base.Update();
        //Ran out of ammo
        if (curCapacity < 0)
        {
            //Stays at 0.
            curCapacity = 0;
        }
        //Timer less than 0, it'll end up at 0.
        if (timer < 0)
        {
            timer = 0;
        }
        //Otherwise if it is more, decrease timer.
        else if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        //Selections are equal to different types of shots.
        if (selection == 0)
        {
            shootTypes = ShootTypes.Basic_Shots;
        }
        else if (selection == 1)
        {
            shootTypes = ShootTypes.Shotgun_Shots;
        }
        else if (selection == 2)
        {
            shootTypes = ShootTypes.Orb_Shots;
        }
        //Checks if its exceeding the limit.
        if(selection > 2)
        {
            //sets it back to 0.
            selection = 0;
        }
        //Same with the opposite.
        else if(selection < 0)
        {
            //set it back up to 2.
            selection = 2;
        }

        if (Input.GetButtonDown("Reload") & !isReloading)
        {
            if (curCapacity <= maxCapacity - 1 && !isLifestealing && capacityClip > 0)
            {
                isReloading = true;
                InvokeRepeating("ReloadCapacity", 0.2f, 0.25f);
            }

            else if (isLifestealing && curCapacity <= maxCapacity - 1)
            {
                InvokeRepeating("ReloadLifesteal", 1.0f, 0.5f);
            }
        }

        if(dpadAxis >= 1 && !selected)
        {
            selected = true;
            print(shootTypes);
            selection++;
        }
        else if(dpadAxis <= -1 && !selected)
        {
            selected = true;
            print(shootTypes);
            selection--;
        }
        else if(selected && dpadAxis == 0)
        {
            selected = false;
        }

        if (Input.GetButton("Shoot") && curCapacity > 0 && timer <= 0)
        {
            //RaycastHit hit;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (Physics.Raycast(ray, out hit, 1000, ground))
            //{
            //targetPos = new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z);
            //}
            CancelInvoke("ReloadCapacity");
            CancelInvoke("ReloadLifesteal");
            isReloading = false;
            Vector3 getPos = transform.position + transform.forward * 1.5f;
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
                        rb.AddForce(cloning.transform.forward * BasicSpeed, ForceMode.Acceleration);
                        timer = 0.25f;

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
                                timer = 1.0f;
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
                        timer = 5.0f;
                        break;
                    }
            }
        }
    }

    protected void ReloadCapacity()
    {
        if (curCapacity <= maxCapacity - 1 && capacityClip > 0)
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
