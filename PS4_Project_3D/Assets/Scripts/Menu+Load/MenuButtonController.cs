using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
	public int index;
	[SerializeField] bool keyDown;
	[SerializeField] int maxIndex;
	public AudioSource audioSource;
	private float inputValueVertical;
	private float inputValueHorizontal;

	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
	}
	
	void FixedUpdate()
    {
		inputValueVertical = Input.GetAxis("Vertical");
		inputValueHorizontal = Input.GetAxis("Horizontal");
	}

	void Update ()
	{
		if (maxIndex == 3)
		{
			Debug.Log(inputValueVertical);
			if (inputValueVertical != 0)
			{
				if (!keyDown)
				{
					if (inputValueVertical < 0)
					{
						if (index < maxIndex)
						{
							index++;
						}
						else
						{
							index = 1;
						}
					}
					else if (inputValueVertical > 0)
					{
						if (index > 1)
						{
							index--;
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
        else if (maxIndex == 1)
		{
			Debug.Log(inputValueHorizontal);
			if (inputValueHorizontal != 0)
			{
				if (!keyDown)
				{
					if (index == 0)
					{
						index = 1;
					}
					else if (index == 1)
					{
						index = 0;
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
}
