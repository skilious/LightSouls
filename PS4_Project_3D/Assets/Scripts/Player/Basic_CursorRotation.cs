using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_CursorRotation : MonoBehaviour
{
    public LayerMask clickMask;
    Ray cameraRay;

    public float adjustments;
    void FixedUpdate()
    {
        float hAxis = Input.GetAxis("R_Horizontal");
        float vAxis = Input.GetAxis("R_Vertical");
        print("Horizontal: " + hAxis + " " + " Vertical" + vAxis);
        //Plane playerPlane = new Plane(Vector3.up, transform.position);

        //cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //float d;
        //if (playerPlane.Raycast(cameraRay, out d))
        //{
        //    Vector3 targetPos = cameraRay.GetPoint(d);
        //    Quaternion targetRot = Quaternion.LookRotation(targetPos - transform.position, Vector3.up);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, adjustments * Time.fixedDeltaTime);
        //}
        float angle = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
        if (hAxis != 0 || vAxis != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), 10.0f * Time.deltaTime);
        }
    }
}
