using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputToContinue : MonoBehaviour
{
    private Animator comicAnimator;
    void Awake()
    {
        comicAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (comicAnimator.GetBool("ShowComic"))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                comicAnimator.SetBool("ShowComic", false);
            }
        }
    }
}
