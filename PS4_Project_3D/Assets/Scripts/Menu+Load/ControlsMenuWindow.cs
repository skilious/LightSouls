using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControlsMenuWindow : MonoBehaviour
{
    // Moved this call from Awake() to Start() due to random execution order.
    private void Start()
    {
        // Access SoundManager to play sound
        SoundManager.PlayTheme(SoundManager.Sound.ControlsMusic);
    }

    private void Awake()
    {
        transform.Find("menuBtn").GetComponent<CodeMonkey.Utils.Button_UI>().ClickFunc = () =>
        {
            Wait(1.5f);
            SceneManager.LoadScene(Loader.Scene.MainMenu.ToString());
        };
        // Call the AddButtonSounds extension method
        transform.Find("menuBtn").GetComponent<CodeMonkey.Utils.Button_UI>().AddButtonSounds();
    }
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
