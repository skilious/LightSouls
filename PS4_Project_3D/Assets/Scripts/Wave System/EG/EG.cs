﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EG : MonoBehaviour
{























































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































        
    public AudioSource yeet;
    public GameObject otherMusic;
    public GameObject goaway;
    private void Awake()
    {
        yeet = gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            goaway.SetActive(true);
            goaway.GetComponent<Text>().text = "Hidden Easteregg by Tai and no one from the group knows this except me and you.";
            if(otherMusic == null)
            {
                otherMusic = GameObject.Find("Sound");
            }
            Destroy(otherMusic);
            yeet.Play();
            DontDestroyOnLoad(yeet);
        }
    }
}
