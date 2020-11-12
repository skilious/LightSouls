// Author: Tarek Tabet

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;

    public static GameAssets GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    // Teleporter Transforms for Prefabs
    public Transform pfTeleporterRed;
    public Transform pfTeleporterGreen;
    public Transform pfTeleporterBlank;

    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
