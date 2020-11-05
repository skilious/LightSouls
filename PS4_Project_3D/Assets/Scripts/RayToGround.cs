using System;
using UnityEngine;

public class RayToGround : MonoBehaviour
{
    [SerializeField]
    protected Color tintColor = Color.green;

    protected Vector3 rayOrigin;
    protected Vector3 rayDirection;
    protected Ray ray;
    protected float distance = 5f;

    protected virtual void RaycastDown(LayerMask layerMask)
    {
        Debug.DrawRay(rayOrigin, rayDirection * distance, Color.red);
    }

    protected virtual void RaycastForward(LayerMask layerMask)
    {
        Debug.DrawRay(rayOrigin, rayDirection * distance, Color.blue);
    }
}