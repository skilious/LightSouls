﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToCredits : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Loader.Load(Loader.Scene.CreditsScene);
        }
    }
}
