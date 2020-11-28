using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSoundFunction : MonoBehaviour
{
	[SerializeField]
	private AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	void PlaySound(AudioClip whichSound)
	{
		audioSource.PlayOneShot(whichSound);
	}
}
