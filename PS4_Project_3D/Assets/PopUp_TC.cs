using System;
using TMPro;
using UnityEngine;
public class PopUp_TC : TeleporterCheck
{
    //[SerializeField]
    //protected GameObject popUpText_TeleporterCheck;

    [SerializeField]
    private TMP_Text teleporterCheckText;

    private readonly int[] stageNumber = new int[] { 1, 2, 3, 4 };

    protected Vector3 rotationFaceCamera;

    protected Vector3 RotateToFaceCamera()
    {
        // Taken from Tai's Popup_Text Script
        rotationFaceCamera = CharacterMovement.forward;
        return rotationFaceCamera;
    }

    private void Start()
    {
        teleporterCheckText.text = "Press 'X' to Start Stage " + stageNumber[0];
        GetInstance().OnTeleporter += TeleporterCheck_OnTeleporter; //Removed TeleporterCheck as its already inherited from the "TeleporterCheck" class...
        GetInstance().OnGround += TeleporterCheck_OnGround;
        //Hide();
    }

    private void FixedUpdate()
    {
        RotateToFaceCamera();
        teleporterCheckText.transform.rotation = Quaternion.LookRotation(rotationFaceCamera);
    }

    private void TeleporterCheck_OnTeleporter(object sender, EventArgs e)
    {
        // Play OnTeleporter Sound
        // SoundManager.PlaySound(SoundManager.Sound.ButtonOver);
        Debug.Log("Do THings for ON tele text");
        animator.SetBool("onTeleporter" , true);
        if(Input.GetKeyDown(KeyCode.X))
        {
            print("Stage 1 loading");
            Loader.Load(Loader.Scene.Level_Skull);
        }
        //Show();
    }
    private void TeleporterCheck_OnGround(object sender, EventArgs e)
    {
        // Play OffTeleporter Sound
        // SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        animator.SetBool("onTeleporter", false);
        //if(!animator.isActiveAndEnabled)
        //    Hide();
    }
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
