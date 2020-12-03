using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Tarek
public class SoulWallHandler : MonoBehaviour
{
    //private static SoulWallHandler instance;
    //public static SoulWallHandler GetInstance()
    //{
    //    return instance;
    //}

    private int soulsAbsorbedCount;

    [SerializeField]
    private List<GameObject> SoulFire;

    private BoxCollider soulWallBoxCollider;
    private ParticleSystem soulWallParticleSystem;

    [SerializeField] private GameObject[] EntrySoulWalls;
    private void Start()
    {
        soulWallBoxCollider = GetComponentInParent<BoxCollider>();
        soulWallParticleSystem = GetComponentInParent<ParticleSystem>();
        soulsAbsorbedCount = 0;
        SoulEssence.OnSoulAbsorbed += SoulWallHandler_OnSoulAbsorbed;
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
            for(int i = 0; i < EntrySoulWalls.Length; i++)
            {
                var soulParticles = EntrySoulWalls[i].GetComponent<ParticleSystem>().main;
                soulParticles.loop = false;
                EntrySoulWalls[i].GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    // Do Teleporter Stuff
    // OnTeleporter.Invoke(this, System.EventArgs.Empty);
}
