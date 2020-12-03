using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_Text : DialogController
{
    [SerializeField] private string textDialogue;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            SetDialogText();
            animator.SetBool("showDialog", true);
        }
    }

    protected override void SetDialogText()
    {
        textMeshProText.text = textDialogue;
    }
}
