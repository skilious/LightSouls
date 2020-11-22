﻿using System;
using TMPro;
using UnityEngine;
public class PopUp_TC : TeleporterCheck
{
    //[SerializeField]
    //protected GameObject popUpText_TeleporterCheck;

    [SerializeField]
    protected TMP_Text teleporterCheckText;

    [SerializeField]
    protected GameObject teleporterComplete;
    [SerializeField]
    protected GameObject teleporterIncomplete;

    [SerializeField]
    private int stageNumber;

    protected Vector3 rotationFaceCamera;

    public bool stageComplete;

    protected Vector3 RotateToFaceCamera()
    {
        // Taken from Tai's Popup_Text Script
        rotationFaceCamera = CharacterMovement.forward;
        return rotationFaceCamera;
    }

    private void Start()
    {
        if(GetStageStatus())
        {
            teleporterComplete.SetActive(true);
            teleporterIncomplete.SetActive(false);
        }
        else
        {
            teleporterIncomplete.SetActive(true);
            teleporterComplete.SetActive(false);
        }
        teleporterCheckText.text = "Press X to Start Stage " + stageNumber;

        // UNUSED CODE
        //GetInstance().OnTeleporter += TeleporterCheck_OnTeleporter; //Removed TeleporterCheck as its already inherited from the "TeleporterCheck" class...
        //GetInstance().OnGround += TeleporterCheck_OnGround;
        //Hide();
    }

    private void FixedUpdate()
    {
        RotateToFaceCamera();
        teleporterCheckText.transform.rotation = Quaternion.LookRotation(rotationFaceCamera);
    }

    public bool GetStageStatus()
    {
        switch (stageNumber)
        {
            case 1:
                return stageComplete;
            case 2:
                return stageComplete;
            case 3:
                return stageComplete;
            case 4:
                return stageComplete;
            default:
                return stageComplete;
        }
    }

    public Loader.Scene GetScene()
    {
        switch (stageNumber)
        {
            case 1:
                return Loader.Scene.Stage_01;
            case 2:
                return Loader.Scene.Stage_02;
            case 3:
                return Loader.Scene.Stage_03;
            case 4:
                return Loader.Scene.Stage_04;
            default:
                return Loader.Scene.MainMenu;
        }
    }


    // UNUSED CODE
    /*
    private void TeleporterCheck_OnTeleporter(object sender, EventArgs e)
    {
        // Play OnTeleporter Sound
        // SoundManager.PlaySound(SoundManager.Sound.ButtonOver);
        Debug.Log(" TeleporterCheck_OnTeleporter event function - Do Things for for when ON Teleporter");
        animator.SetBool("onTeleporter" , true);
        
        //Show();
    }
    private void TeleporterCheck_OnGround(object sender, EventArgs e)
    {
        Debug.Log(" TeleporterCheck_OnGround event function - Do Things for when OFF Teleporter");

        // Play OffTeleporter Sound
        // SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        animator.SetBool("onTeleporter", false);
        //if(!animator.isActiveAndEnabled)
        //    Hide();
    }
    */

    //No longer in use - Animator replaced this code.
    //private void Hide()
    //{
    //    gameObject.SetActive(false);
    //}
    //private void Show()
    //{
    //    gameObject.SetActive(true);
    //}
}
