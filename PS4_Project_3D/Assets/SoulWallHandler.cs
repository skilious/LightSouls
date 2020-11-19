using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulWallHandler : MonoBehaviour
{
    private int soulsAbsorbedCount;

    [SerializeField]
    private List<GameObject> SoulFire;

    private BoxCollider soulWallBoxCollider;
    private ParticleSystem soulWallParticleSystem;

    private void Start()
    {
        soulWallBoxCollider = GetComponentInParent<BoxCollider>();
        soulWallParticleSystem = GetComponentInParent<ParticleSystem>();
        soulsAbsorbedCount = 0;
        SoulEssence.OnSoulAbsorbed += SoulWallHandler_OnSoulAbsorbed; //Removed TeleporterCheck as its already inherited from the "TeleporterCheck" class...
    }

    private void SoulWallHandler_OnSoulAbsorbed(SoulEssence soulEssence)
    {
        soulsAbsorbedCount++;
        Debug.Log(soulsAbsorbedCount);
        if (soulsAbsorbedCount == 1)
        {
            SoulFire[0].SetActive(true);
        }
        else if (soulsAbsorbedCount == 2)
        {
            SoulFire[1].SetActive(true);
        }
        else if (soulsAbsorbedCount == 3)
        {
            SoulFire[2].SetActive(true);
            soulWallBoxCollider.enabled = false;
            var psMain = soulWallParticleSystem.main;
            psMain.loop = false;
        }
    }

    // Do Teleporter Stuff
    // OnTeleporter.Invoke(this, System.EventArgs.Empty);
}
