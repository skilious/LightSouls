using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    private Canvas dialogCanvas;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private TMP_Text text;

    // Start is called before the first frame update
    void Awake()
    {
        animator = dialogCanvas.GetComponentInChildren<Animator>();
        text = dialogCanvas.GetComponentInChildren<TMP_Text>();
        animator.SetBool("showDialog", false);
    }

    private void Update()
    {
        if(animator.GetBool("showDialog"))
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                animator.SetBool("showDialog", false);
            }
        }
    }

    protected void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("showDialog", true);
        }
    }
    protected void OnTriggerExit(Collider collision)
    {
        animator.SetBool("showDialog", false);
    }

    protected virtual void SetDialogText()
    {

    }
}
