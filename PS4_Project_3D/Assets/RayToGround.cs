using System;
using UnityEngine;

public class RayToGround : MonoBehaviour
{
    [SerializeField]
    private Color tintColor = Color.green;

    private void Update()
    {
        RaycastSingle();
    }

    private void RaycastSingle()
    {
        Vector3 origin = transform.position;
        Vector3 direction = -transform.up;

        Debug.DrawRay(origin, direction * 5f, Color.red);
        Ray ray = new Ray(origin, direction);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            raycastHit.collider.GetComponent<Renderer>().material.color = tintColor;
        }
    }
}
