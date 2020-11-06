using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using CodeMonkey.Utils;

public class MainMenuWindow : MonoBehaviour
{
    private void Awake()
    {
        // Access SoundManager to play sound
        SoundManager.PlayTheme(SoundManager.Sound.ThemeMusic);

        transform.Find("playBtn").GetComponent<CodeMonkey.Utils.Button_UI>().ClickFunc = () =>
        {
            Loader.Load(Loader.Scene.SoulGame);
        };
        // Call the AddButtonSounds extension method
        transform.Find("playBtn").GetComponent<CodeMonkey.Utils.Button_UI>().AddButtonSounds();

        transform.Find("quitBtn").GetComponent<CodeMonkey.Utils.Button_UI>().ClickFunc = () =>
        {
            Loader.Load(Loader.Scene.CreditsScene);
        };
        // Call the AddButtonSounds extension method
        transform.Find("quitBtn").GetComponent<CodeMonkey.Utils.Button_UI>().AddButtonSounds();

        transform.Find("controlsBtn").GetComponent<CodeMonkey.Utils.Button_UI>().ClickFunc = () =>
        {
            Loader.Load(Loader.Scene.ControlsScene);
        };
        // Call the AddButtonSounds extension method
        transform.Find("controlsBtn").GetComponent<CodeMonkey.Utils.Button_UI>().AddButtonSounds();
    }
}
