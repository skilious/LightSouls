using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	public bool disableOnce;



	void PlaySound(AudioClip whichSound)
	{
		// COMMENTED CODE IS REMOVED BECAUSE I DON"T CARE THAT IT PLAYS THE BUTTON SELECTED SOUND ON START.
		// It was annoying to see the animation without the sound actually...
		//if(!disableOnce)
		//{
			menuButtonController.audioSource.PlayOneShot(whichSound);
		//}
		//else
		//{
		//	disableOnce = false;
		//}
	}
}	
