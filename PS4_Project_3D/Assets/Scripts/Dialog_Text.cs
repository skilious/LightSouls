using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's version.
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
        base.OnTriggerEnter(collision);
    }

    protected override void SetDialogText()
    {
        textMeshProText.text = textDialogue;
    }
}
