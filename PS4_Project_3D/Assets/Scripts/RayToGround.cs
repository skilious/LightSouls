using System;
using UnityEngine;

public class RayToGround : MonoBehaviour
{
    [SerializeField]
    protected Color tintColor = Color.green;

    protected Vector3 rayOrigin;
    protected Vector3 rayDirection;
    protected Ray ray;
    protected float distance = 1f;

    protected virtual void RaycastForward(LayerMask layerMask)
    {
    }
}