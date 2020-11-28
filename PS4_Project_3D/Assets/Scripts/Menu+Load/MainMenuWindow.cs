using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuWindow : MonoBehaviour
{
    // Moved this call from Awake() to Start() due to random execution order.
    private void Start()
    {
        // Access SoundManager to play sound
        SoundManager.PlayTheme(SoundManager.Sound.MenuMusic);
    }

    private void Awake()
    {
        transform.Find("playBtn").GetComponent<CodeMonkey.Utils.Button_UI>().ClickFunc = () =>
        {
            StartCoroutine(Wait(1.5f));
            Loader.Load(Loader.Scene.SoulGame);
        };
        // Call the AddButtonSounds extension method
        transform.Find("playBtn").GetComponent<CodeMonkey.Utils.Button_UI>().AddButtonSounds();

        transform.Find("quitBtn").GetComponent<CodeMonkey.Utils.Button_UI>().ClickFunc = () =>
        {
            StartCoroutine(Wait(1.5f));
            Application.Quit();
        };
        // Call the AddButtonSounds extension method
        transform.Find("quitBtn").GetComponent<CodeMonkey.Utils.Button_UI>().AddButtonSounds();

        transform.Find("controlsBtn").GetComponent<CodeMonkey.Utils.Button_UI>().ClickFunc = () =>
        {
            StartCoroutine(Wait(1.5f));
            SceneManager.LoadScene(Loader.Scene.ControlsScene.ToString());
        };
        // Call the AddButtonSounds extension method
        transform.Find("controlsBtn").GetComponent<CodeMonkey.Utils.Button_UI>().AddButtonSounds();

        transform.Find("creditsBtn").GetComponent<CodeMonkey.Utils.Button_UI>().ClickFunc = () =>
        {
            StartCoroutine(Wait(1.5f));
            SceneManager.LoadScene(Loader.Scene.CreditsScene.ToString());
        };
        // Call the AddButtonSounds extension method
        transform.Find("creditsBtn").GetComponent<CodeMonkey.Utils.Button_UI>().AddButtonSounds();
    }
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
