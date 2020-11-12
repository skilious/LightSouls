using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterCheck : RayToGround
{
    private static TeleporterCheck instance;
    public static TeleporterCheck GetInstance()
    {
        return instance;
    }

    // Event Shoutout Funtions
    public event System.EventHandler OnTeleporter;
    public event System.EventHandler OffTeleporter;
    
    public LayerMask teleporterLayer;
    public bool onTeleporter;
    public bool offTeleporter;

    private void Awake()
    {
        onTeleporter = false;
        rayOrigin = transform.position;
        rayDirection = -transform.up;
    }

    private void FixedUpdate()
    {
        rayOrigin = transform.position;
        ray = new Ray(rayOrigin, rayDirection * distance);
        RaycastDown(teleporterLayer);
    }

    protected override void RaycastDown(LayerMask layerMask)
    {
        base.RaycastDown(layerMask);

        // Dray ray for debugging
        Debug.DrawRay(rayOrigin, rayDirection * distance, Color.red);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, distance, layerMask))
        {
            onTeleporter = true;
            Debug.Log("ON Teleporter");

            // Do Teleporter Stuff

            // Text Popup: "Press X to Enter Stage"
        }
        else
        {
            onTeleporter = false;
            Debug.Log("OFF Teleporter");
        }
    }

}
