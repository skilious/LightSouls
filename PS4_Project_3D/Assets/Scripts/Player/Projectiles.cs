using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's shitty code on projectiles before I even bothered to learn inheritance.
public class Projectiles : Character_Status
{
    //Speed of the projectiles coming out w/ addforce rigidbody.
    [SerializeField]
    private float BasicSpeed = 0, ShotgunSpeed = 0;

    //Two booleans represents either reloading from Ammo Capacity or lifestealing your health.
    [SerializeField]
    protected bool isReloading = false;
    [SerializeField]
    protected bool isLifestealing = false;

    //Firerate for each weapon.
    public static float fireRate = 0;

    //Selection of the weapons swapping
    private int selection = 0;

    //Prevents miss selecting the right weapon (Prevents holding due to the way how dpad is set to axis).
    private bool selected = false;
    protected float dpadAxis; //dpad uses axis and goes between -1 (left) to 1 (right).


    //All the types of projectiles that we are going to use.
    public enum ShootTypes
    {
        Basic_Shots,
        Shotgun_Shots,
        Orb_Shots,
        AOEShot,
        LaserShot
    };
    public static ShootTypes shootTypes;

    private void Awake()
    {
        curCapacity = maxCapacity;
    }

    protected override void Update()
    {
        if (SimplePause.notPaused)
        {
            dpadAxis = Input.GetAxis("SwitchWeapon_Dpad");
        }
        base.Update();
        //Ran out of ammo
        if (curCapacity < 0)
        {
            //Stays at 0.
            curCapacity = 0;
        }

        //fireRate less than 0, it'll end up at 0.
        if (fireRate < 0)
        {
            fireRate = 0;
        }
        //Otherwise if it is more, decrease fireRate timer.
        else if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
        }

        //Selections are equal to different types of shots.
        switch (selection)
        {
            case 0:
                {
                    shootTypes = ShootTypes.Basic_Shots;
                    break;
                }
            case 1:
                {
                    shootTypes = ShootTypes.Shotgun_Shots;
                    break;
                }
            case 2:
                {
                    shootTypes = ShootTypes.Orb_Shots;
                    break;
                }
            case 3:
                {
                    shootTypes = ShootTypes.AOEShot;
                    break;
                }
            case 4:
                {
                    shootTypes = ShootTypes.LaserShot;
                    break;
                }
        }
        //Checks if its exceeding the limit.
        if (selection > 4)
        {
            //sets it back to 0.
            selection = 0;
        }
        //Same with the opposite.
        else if (selection < 0)
        {
            //set it back up to 4.
            selection = 4;
        }

        // Added ( 'OR' KeyCode.R ) to restore PC functionality for prototyping. - Tarek
        if ((Input.GetButtonDown("Reload") || Input.GetKeyDown(KeyCode.R)) & !isReloading && SimplePause.notPaused)
        {
            if (curCapacity <= maxCapacity - 1 && !isLifestealing && capacityClip > 0)
            {
                isReloading = true;
                InvokeRepeating("ReloadCapacity", 0.1f, 0.15f);
            }

            else if (isLifestealing && curCapacity <= maxCapacity - 1)
            {
                InvokeRepeating("ReloadLifesteal", 0.15f, 0.20f);
            }
        }

        if (dpadAxis >= 1 && !selected || Input.GetKeyDown(KeyCode.UpArrow)) //Up Arrow is added for PC functionality
        {
            selected = true;
            selection++;

            print(shootTypes);
        }
        else if (dpadAxis <= -1 && !selected || Input.GetKeyDown(KeyCode.DownArrow)) // Same with the down arrow
        {
            selected = true;
            print(shootTypes);
            selection--;
        }
        else if (selected && dpadAxis == 0)
        {
            selected = false;
        }

        //Shoot is for PS4 only and checks if curCapacity is over than 0 and fireRate is equal to 0 or less.

        // Added 'OR' Conditional to make it work for both PS4 and PC while we are still in prototype phase. - Tarek

        if ((Input.GetButton("Shoot") || Input.GetButton("Fire1")) && curCapacity > 0 && fireRate <= 0 && SimplePause.notPaused)
        {
            CancelInvoke("ReloadCapacity"); //Cancels both reloading functions preventing them to continue on whilst shooting.
            CancelInvoke("ReloadLifesteal");
            isReloading = false; //Sets the boolean and allows the player to reload again.
            Vector3 getPos = transform.position + transform.forward * 1.2f; //Grab's the players position w/ offset.
            switch (shootTypes)
            {
                //Basic projectile that shoots wherever the player is facing.
                case ShootTypes.Basic_Shots:
                    {
                        curCapacity--;
                        GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("Projectile");
                        cloning.SetActive(true);
                        cloning.transform.position = getPos;
                        cloning.transform.rotation = transform.rotation;
                        Rigidbody rb = cloning.gameObject.GetComponent<Rigidbody>();
                        rb.AddForce(cloning.transform.forward * BasicSpeed, ForceMode.Acceleration);
                        fireRate = 0.25f;
                        break;
                    }
                case ShootTypes.Shotgun_Shots: //Shoots three projectiles split away from each other.
                    {
                        if (curCapacity >= 3)
                        {
                            float spread = -15.0f;
                            //Calculating the angle
                            for (int i = 0; i < 3; i++)
                            {
                                curCapacity--;
                                GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("Projectile");
                                cloning.SetActive(true);
                                cloning.transform.position = getPos;
                                cloning.transform.rotation = transform.rotation;
                                cloning.transform.Rotate(0, spread, 0); //This used to be randomized until its difficult to predict. Now its fixed to have normal spreading.
                                Rigidbody rb = cloning.gameObject.GetComponent<Rigidbody>();
                                rb.AddForce(cloning.transform.forward * ShotgunSpeed, ForceMode.Acceleration);
                                fireRate = 1.0f;
                                spread += 15.0f;
                            }
                        }
                        break;
                    }
                case ShootTypes.Orb_Shots: //This one summons a portal above and behind the player. Shoots fire orbs and automatically aims nearest enemy.
                    {
                        curCapacity--;
                        GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("OrbPortal");
                        cloning.SetActive(true);
                        cloning.transform.position = transform.position + -transform.forward * 1.0f + transform.up * 2.5f;
                        cloning.transform.rotation = Quaternion.Euler(cloning.transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
                        fireRate = 5.0f;
                        break;
                    }
                case ShootTypes.AOEShot: //This is wider comparing to the original and does the same except it goes through enemies.
                    {
                        curCapacity--;
                        GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("AOEProjectile");
                        cloning.SetActive(true);
                        cloning.transform.position = getPos;
                        cloning.transform.rotation = transform.rotation;
                        Rigidbody rb = cloning.gameObject.GetComponent<Rigidbody>();
                        rb.AddForce(cloning.transform.forward * 500.0f, ForceMode.Acceleration);
                        fireRate = 1.5f;
                        break;
                    }
                case ShootTypes.LaserShot:
                    {
                        curCapacity--;
                        GameObject cloning = Object_Pooling.SharedInstance.GetPooledObject("Laser");
                        cloning.SetActive(true);
                        cloning.transform.position = getPos;
                        cloning.transform.rotation = transform.rotation;
                        fireRate = 3.0f;
                    }

                    break;
            }
        }
    }


    protected void ReloadCapacity()
    {
        if (curCapacity <= maxCapacity - 1 && capacityClip > 0)
        {
            //It literally just increments by 1.
            curCapacity++;
            //Grabs ammo capacity clip in return.
            capacityClip--;
        }
        else
        {
            //No longer reloading and stops the function.
            isReloading = false;
            CancelInvoke("ReloadCapacity");
            print("Full capacity already!"); //Yeets a print telling you that your weapon is ready to fire again!
        }
    }

    //Lifesteal method.
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

/*UNUSED CODE HERE
 * Raycast reference for aimming and height adjustments. However, its PC only with the mouse.
 *          RaycastHit hit;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (Physics.Raycast(ray, out hit, 1000, ground))
            //{
            //targetPos = new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z);
            //}
*/
