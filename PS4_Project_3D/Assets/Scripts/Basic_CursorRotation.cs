using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_CursorRotation : MonoBehaviour
{
    public LayerMask clickMask;
    Ray cameraRay;
    RaycastHit cameraRayHit;

    public float adjustments;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float d;
        if (playerPlane.Raycast(cameraRay, out d))
        {
            Vector3 targetPos = cameraRay.GetPoint(d);
            Quaternion targetRot = Quaternion.LookRotation(targetPos - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, adjustments);
        }
    }
}
