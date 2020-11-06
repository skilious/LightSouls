using UnityEngine;

public class CreditsMenuWindow : MonoBehaviour
{
    private void Awake()
    {
        // Access SoundManager to play sound
        SoundManager.PlayTheme(SoundManager.Sound.CreditsMusic);

        transform.Find("quitBtn").GetComponent<CodeMonkey.Utils.Button_UI>().ClickFunc = () =>
        {
            Application.Quit();
        };
        // Call the AddButtonSounds extension method
        transform.Find("quitBtn").GetComponent<CodeMonkey.Utils.Button_UI>().AddButtonSounds();
    }
}
