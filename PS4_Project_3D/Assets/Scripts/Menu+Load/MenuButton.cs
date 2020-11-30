using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    const Loader.Scene DEFAULT_SCENE = default;
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
			animator.SetBool("selected", true);
			if (Input.GetAxis("Submit") == 1)
			{
				animator.SetBool("pressed", true);
                _ = StartCoroutine(WaitThenLoad(0.5f));
			}
			else if (animator.GetBool("pressed"))
			{
				animator.SetBool("pressed", false);
                animatorFunctions.disableOnce = true;
            }
		}
		else
		{
			animator.SetBool ("selected", false);
		}
    }

	IEnumerator WaitThenLoad(float seconds)
	{
		yield return new WaitForSeconds(seconds);
        Loader.Load(GetScene());
	}
	
	public Loader.Scene GetScene()
	{
		switch (thisIndex)
		{
			case 0:
                return Loader.Scene.MainMenu;
			case 1:
                return Loader.Scene.SoulGame;
			case 2:
                return Loader.Scene.ControlsScene;
			case 3:
                return Loader.Scene.CreditsScene;
			case 4:
				return DEFAULT_SCENE;
			default:
				return DEFAULT_SCENE;
		}
	}
}
