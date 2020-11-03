using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    private void Awake()
    {
        // Start Theme Music on play start
        SoundManager.PlayTheme(SoundManager.Sound.ThemeMusic);
    }
}
