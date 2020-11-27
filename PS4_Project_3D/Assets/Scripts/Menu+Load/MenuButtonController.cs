using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
	public int index;
	[SerializeField] bool keyDown;
	[SerializeField] int maxIndex;
	public AudioSource audioSource;
	private float inputValue;

	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
	}
	
	void FixedUpdate()
    {
		inputValue = Input.GetAxis("Vertical");
	}

	void Update ()
	{
		Debug.Log(inputValue);
		if(inputValue != 0)
		{
			if(!keyDown)
			{
				if (inputValue < 0) 
				{
					if(index < maxIndex)
					{
						index++;
					}
					else
					{
						index = 0;
					}
				} 
				else if(inputValue > 0)
				{
					if(index > 0)
					{
						index --; 
					}
					else
					{
						index = maxIndex;
					}
				}
				keyDown = true;
			}
		}
		else
		{
			keyDown = false;
		}
	}
}
