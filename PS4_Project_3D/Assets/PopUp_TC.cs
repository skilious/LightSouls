using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUp_TC : Popup_Text
{
    [SerializeField]
    private TextMeshPro teleporterCheckText;
    private void Awake()
    {
        // teleporterCheckText = transform.Find("scoreText").GetComponent<TextMeshPro>();

        // Start Theme Music on play start
        SoundManager.PlaySound(SoundManager.Sound.ButtonOver);
    }

    private void Start()
    {
        teleporterCheckText.text = "Press 'X' to Start Stage";
        TeleporterCheck.GetInstance().OnTeleporter += TeleporterCheck_OnTeleporter;
        TeleporterCheck.GetInstance().OffTeleporter += TeleporterCheck_OffTeleporter;
        Hide();
    }
    private void TeleporterCheck_OnTeleporter(object sender, EventArgs e)
    {
        Show();
    }
    private void TeleporterCheck_OffTeleporter(object sender, EventArgs e)
    {
        Hide();
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
