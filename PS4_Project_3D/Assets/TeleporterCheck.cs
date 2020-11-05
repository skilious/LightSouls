using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterCheck : RayToGround
{
    public LayerMask teleporterLayer;

    private void Awake()
    {
        rayOrigin = transform.position;
        rayDirection = -transform.up;
        ray = new Ray(rayOrigin, rayDirection);
    }

    private void Update()
    {
        RaycastDown(teleporterLayer);
    }

    protected override void RaycastDown(LayerMask layerMask)
    {
        base.RaycastDown(layerMask);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, layerMask))
        {
            // Do Teleporter Stuff
            // Text Popup: "Press X to Enter Stage"
            raycastHit.collider.GetComponent<Renderer>().material.color = tintColor;
        }
    }
}
