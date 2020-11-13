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

    [SerializeField] List<GameObject> _prefabsListTeleporters;
    // Teleporter Colliders
    public MeshCollider meshCollider_TeleportStage01;
    public MeshCollider meshCollider_TeleportStage02;
    public MeshCollider meshCollider_TeleportStage03;
    public MeshCollider meshCollider_TeleportStage04;

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
