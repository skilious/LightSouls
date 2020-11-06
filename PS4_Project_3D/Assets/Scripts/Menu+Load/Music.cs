using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    void Start()
    {
        // Access SoundManager to play sound
        SoundManager.PlayTheme(SoundManager.Sound.ThemeMusic);
    }
}
