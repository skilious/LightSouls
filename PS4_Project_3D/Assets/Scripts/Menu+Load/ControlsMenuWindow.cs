using UnityEngine;

public class ControlsMenuWindow : MonoBehaviour
{
    private void Awake()
    {
        // Access SoundManager to play sound
        SoundManager.PlayTheme(SoundManager.Sound.ControlsMusic);

        transform.Find("menuBtn").GetComponent<CodeMonkey.Utils.Button_UI>().ClickFunc = () =>
        {
            Loader.Load(Loader.Scene.MainMenu);
        };
        // Call the AddButtonSounds extension method
        transform.Find("menuBtn").GetComponent<CodeMonkey.Utils.Button_UI>().AddButtonSounds();
    }
}
