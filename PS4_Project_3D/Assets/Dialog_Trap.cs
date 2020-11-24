using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_Trap : DialogController
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        SetDialogText();
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("showDialog", true);
        }
    }

    protected override void SetDialogText()
    {
        textMeshProText.text = "Do not step on that panel! I know you're thinking; " +
            "'Whatever, mate, You're a Text box, not my mother!' ... Well, my conscience" +
            " is clear... You do you, Champ...";
    }
}
