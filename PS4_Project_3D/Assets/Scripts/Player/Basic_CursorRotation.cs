using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_CursorRotation : MonoBehaviour
{
    public LayerMask clickMask;
    Ray cameraRay;

    private Vector3 forward, right;
    public float adjustments;

    private void Awake()
    {
        //Yeet this from CharacterMovement so it could be relative to the camera.
        //Isometric support too. That's what "right" is for.
        CharacterMovement.forward = Camera.main.transform.forward;
        CharacterMovement.forward.y = 0;
        CharacterMovement.right = Quaternion.Euler(new Vector3(0, 90, 0)) * CharacterMovement.forward;
    }
    void Update()
    {
        //Grabs GetAxis from right analog w/ X (R_Horizontal) and Y (R_Vertical).
        float hAxis = Input.GetAxis("R_Horizontal");
        float vAxis = Input.GetAxis("R_Vertical");
        //print("Horizontal: " + hAxis + " " + " Vertical" + vAxis);

        Vector3 rightRotation = CharacterMovement.right * 10.0f * Time.deltaTime * hAxis;
        Vector3 upRotation = CharacterMovement.forward * 10.0f * Time.deltaTime * vAxis;
        //Angle - Rotating character
        //float angle = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
        if (hAxis != 0 || vAxis != 0)
        {
            Quaternion rotSmooth = Quaternion.LookRotation(rightRotation + upRotation);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotSmooth, 10.0f * Time.deltaTime);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(upRotation), 10.0f * Time.deltaTime);
        }
    }
}


//UNUSED CODE
/*PC - Cursor purposes (Raycast)

//Plane grabs current position of the player
//Plane playerPlane = new Plane(Vector3.up, transform.position);

//Uses Ray relative to main camera and point to ray w/ mouse position.
//cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
//d = distance.
//float d;
//if (playerPlane.Raycast(cameraRay, out d))
//{
//    Vector3 targetPos = cameraRay.GetPoint(d);
//    Quaternion targetRot = Quaternion.LookRotation(targetPos - transform.position, Vector3.up);
//    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, adjustments * Time.fixedDeltaTime);
}*/