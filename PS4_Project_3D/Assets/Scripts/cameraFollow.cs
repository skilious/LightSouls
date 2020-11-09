using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script
public class cameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.one;

    private bool rotating = false;

    private int directions = 0;

    private void LateUpdate()
    {
        //This then, follows the Target (Player).
        FollowTransform(); //Using late update basically gets called out after every other types of Updates get called first.
    }

    private void Update()
    {
        if(!rotating && SimplePause.notPaused)
        {
            float dpadAxis = Input.GetAxis("Rotate"); //PS4 "Rotate" uses axis and it'll have to work differently.
            smoothSpeed = 0.0f;
            if (dpadAxis >= 1 || Input.GetKeyDown(KeyCode.E)) //Added GetKeyDown for PC functionality
            {
                directions--;
                StartCoroutine(rotationChange());
            }
            else if (dpadAxis <= -1 || Input.GetKey(KeyCode.Q))
            {
                directions++;
                StartCoroutine(rotationChange());
            }
        }
    }
    void FollowTransform()
    {
        Vector3 desiredPos = target.position + offset; //grab the Target's position and offset.
        Vector3 smoothedPos = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothSpeed); //Smoothdamp is basically like lerp but Vector3 support.
        transform.position = smoothedPos; //Set it to this Camera's position.

        transform.LookAt(target); //Look towards the target and focus. It will lock rotation as well until the values are changed within the offset.
    }

    //Deals with User input left and right on dpad
    //Using switch statements to deal with rotating and referencing it from an integer value.
    IEnumerator rotationChange()
    {
        smoothSpeed = 0.1f; //Set the smoothSpeed to 0.1f so, it'll move smoothly instead of snapping.
        if (rotating)
        {
            yield return null;
        }
        if (directions > 3) //If its more than 3, return back to 0.
        {
            directions = 0;
        }
        else if (directions < 0) //same but vice versa.
        {
            directions = 3;
        }
        rotating = true; //set it true and do the job.
        switch(directions)
        {
            case 0:
                {
                    Vector3 setOffset = new Vector3(-5, 5, -5);
                    offset = setOffset;
                    break;
                }
            case 1:
                {
                    Vector3 setOffset = new Vector3(-5, 5, 5);
                    offset = setOffset;
                    break;
                }
            case 2:
                {
                    Vector3 setOffset = new Vector3(5, 5, 5);
                    offset = setOffset;
                    break;
                }
            case 3:
                {
                    Vector3 setOffset = new Vector3(5, 5, -5);
                    offset = setOffset;
                    break;
                }
        }
        yield return new WaitForSeconds(0.25f);
        CharacterMovement.forward = Camera.main.transform.forward; //Apply the new Camera's transform.forward values.
        CharacterMovement.forward.y = 0; //Maintain the 0 on the y.
        CharacterMovement.right = Quaternion.Euler(new Vector3(0, 90, 0)) * CharacterMovement.forward; //Apply it w/ right and 90 degrees.
        rotating = false; //Then set it to false to prevent rotation problems.
    }
}
