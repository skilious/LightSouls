using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
    [SerializeField] int thisIndex;
	public Loader.Scene thisScene;

	void Start ()
    {
        GetScene();
    }

    void Update()
    {
		if(menuButtonController.index == thisIndex)
		{
			animator.SetBool ("selected", true);
			if (Input.GetAxis("Submit") == 1)
			{
				animator.SetBool("pressed", true);
			}
			else if (animator.GetBool("pressed"))
			{
				animator.SetBool("pressed", false);
                //animatorFunctions.disableOnce = true;
            }
		}
		else
		{
			animator.SetBool ("selected", false);
		}
    }

	public Loader.Scene GetScene()
	{
		switch (thisIndex)
		{
			case 0:
				return Loader.Scene.SoulGame;
			case 1:
				return Loader.Scene.ControlsScene;
			case 2:
				return Loader.Scene.CreditsScene;
			default:
				return Loader.Scene.MainMenu;
		}
	}
}
