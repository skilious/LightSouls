using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    protected Canvas dialogCanvas;

    [SerializeField]
    protected Animator animator;

    [SerializeField]
    protected TMP_Text textMeshProText;

    //[SerializeField]
    //private string dialogText;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        animator = dialogCanvas.GetComponentInChildren<Animator>();
        textMeshProText = dialogCanvas.GetComponentInChildren<TMP_Text>();
        animator.SetBool("showDialog", false);
    }

    protected virtual void Update()
    {
        if(animator.GetBool("showDialog"))
        {
            if (Input.GetButtonDown("Submit"))
            {
                animator.SetBool("showDialog", false);
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Temp"))
        {
            SetDialogText();
            animator.SetBool("showDialog", true);
        }
    }
    protected void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Temp"))
        {
            animator.SetBool("showDialog", false);
        }
    }

    // UNUSED - Not Sure if this is more efficient but at least the other way I only have on 
    //script and edit the text I want in the editor... Feedback Appreciated though...
    protected virtual void SetDialogText() //Just use SerializeField private String. Its not that bad at all.
    {
        textMeshProText.text = "Welcome to the Game. Are You Looking To Save Someone? " +
            "Better to save your Own soul If you ask me... But you didn't ask, I Just " +
            "popped Up... So, I Guess, you do you, Champ!";
    }
}

//The textmeshpro will automatically clamp it down the line anyways as it has an invisible yellow border.
//that handles the limitation of how far the sentence can go until it goes down the paragraph.
