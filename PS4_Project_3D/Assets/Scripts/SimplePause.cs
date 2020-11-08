﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Tai's simple pause script.
public class SimplePause : MonoBehaviour
{
    public static bool notPaused = true;
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (notPaused)
            {
                notPaused = false;
                Time.timeScale = 0;
                SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            }
            else if (!notPaused)
            {
                notPaused = true;
                Time.timeScale = 1;
                SceneManager.UnloadSceneAsync(gameObject.name);
            }
        }
    }
}
