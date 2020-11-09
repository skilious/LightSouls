using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script
public class Basic_CursorRotation : MonoBehaviour
{
    Ray cameraRay;
    private float adjustments = 3.0f;

    [SerializeField]
    private bool PCMode = false;
    private void Awake()
    {
        //Yeet this from CharacterMovement so it could be relative to the camera.
        //Isometric support too. That's what "right" is for.
        CharacterMovement.forward = Camera.main.transform.forward; //Check out CharacterMovement's script for the entire descrption of why this is needed.
        CharacterMovement.forward.y = 0;
        CharacterMovement.right = Quaternion.Euler(new Vector3(0, 90, 0)) * CharacterMovement.forward;
    }
    void Update()
    {
        //Grabs GetAxis from right analog w/ X (R_Horizontal) and Y (R_Vertical).
        float hAxis = Input.GetAxis("R_Horizontal");
        float vAxis = Input.GetAxis("R_Vertical");

        //Some other float variables to get axis from left analog.
        float hAxis2 = Input.GetAxis("Horizontal");
        float vAxis2 = Input.GetAxis("Vertical");

        //print("Horizontal: " + hAxis + " " + " Vertical" + vAxis); Checks if they are registering properly.

        //Hacky vector3s to check if they are properly rotating based on its rotation from the camera.
        Vector3 rightRotation = CharacterMovement.right * 10.0f * Time.deltaTime * hAxis;
        Vector3 upRotation = CharacterMovement.forward * 10.0f * Time.deltaTime * vAxis;

        Vector3 rightRotationH = CharacterMovement.right * 10.0f * Time.deltaTime * hAxis2;
        Vector3 upRotationV = CharacterMovement.forward * 10.0f * Time.deltaTime * vAxis2;
        if (SimplePause.notPaused)
        {
            //PC - Cursor purposes (Raycast)

            //Plane grabs current position of the player
            Plane playerPlane = new Plane(Vector3.up, transform.position);

            //Uses Ray relative to main camera and point to ray w / mouse position.
            cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //d = distance;
            if (playerPlane.Raycast(cameraRay, out float d))
            {
                Vector3 targetPos = cameraRay.GetPoint(d);
                Quaternion targetRot = Quaternion.LookRotation(targetPos - transform.position, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, adjustments * Time.fixedDeltaTime);
            }


            //Angle - Rotating character - Right analog stick focuses on rotating the character.
            if (hAxis != 0 || vAxis != 0 && !PCMode) //This piece of shit checks if hAxis/vAxis are not equal to 0. (This takes priority over the "else if" statement)
            {
                //Supports rotation and relative to the camera.
                Quaternion rotSmooth = Quaternion.LookRotation(rightRotation + upRotation);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotSmooth, 10.0f * Time.deltaTime);
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(upRotation), 10.0f * Time.deltaTime);
            }
            //Recently added to allow player to rotate the character with the other analog whilst moving.
            else if (hAxis2 != 0 && !PCMode || vAxis2 != 0 && !PCMode)
            {
                Quaternion rotSmooth = Quaternion.LookRotation(rightRotationH + upRotationV);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotSmooth, 10.0f * Time.deltaTime);
            }
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
