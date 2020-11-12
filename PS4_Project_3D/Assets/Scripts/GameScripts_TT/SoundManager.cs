using UnityEngine;

[RequireComponent(typeof(GameAssets))]
public static class SoundManager
{
    public enum Sound
    {
        MenuMusic,
        ControlsMusic,
        CreditsMusic,

        // UNIMPLEMENTED... FOR NOW.
        OnTeleporter,
        ButtonOver,
        ButtonClick,
    }

    public static void PlaySound(Sound sound)
    {
        GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound), volumeScale: 0.05f);

        // Want to destroy to clean up game sounds... no such function in static void?

    }

    public static void PlayTheme(Sound sound)
    {
        GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound), volumeScale: 0.1f);
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        // Removed Tai's fix. Added a 'RequireComponent' at beginning of script to make sure it
        // finds the GameAssets component in the scene. Also, the issue was occurring due to 
        // random execution order of Awake() calls. See '...MenuWindow' Scripts for comment.
        // Basically, had to move a function call from Awake() to Start() for initial game sound.
        // GameAssets gameAssets = GameObject.Find("GameAssets").GetComponent<GameAssets>();

        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.GetInstance().soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

    // Extension Method of the Button_UI Class - The 'this' keyword means that this is an
    // extension method. AddButtonSounds() to the Button_UI class without affecting the 
    // Button_UI class.
    public static void AddButtonSounds(this CodeMonkey.Utils.Button_UI buttonUI)
    {
        buttonUI.MouseOverOnceFunc += () => PlaySound(Sound.ButtonOver);
        buttonUI.ClickFunc += () => PlaySound(Sound.ButtonClick);
    }
}
