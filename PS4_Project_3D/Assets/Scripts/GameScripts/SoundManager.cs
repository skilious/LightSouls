using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        ThemeMusic,

        // UNIMPLEMENTED... FOR NOW.
        ButtonOver,
        ButtonClick
    }

    public static void PlaySound(Sound sound)
    {
        GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));


        // Want to destroy to clean up game sounds... no such function in static void?


    }

    public static void PlayTheme(Sound sound)
    {
        GameObject soundStorage = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = soundStorage.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound), volumeScale: 0.2f);
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        GameAssets gameAssets = GameObject.Find("GameAssets").GetComponent<GameAssets>();
        foreach (GameAssets.SoundAudioClip soundAudioClip in gameAssets.soundAudioClipArray)
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
