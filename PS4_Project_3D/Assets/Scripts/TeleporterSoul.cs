using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TeleporterSoul : LoadLevelScene
{
    private bool allowTeleport = false; 
    private int soulsAbsorbed = 0;
    [SerializeField] private int soulsRequired = 0;
    [SerializeField] private int levelCompleteN = 0;
    [SerializeField] private GameObject bonfireLit;
    [SerializeField] private bool isBossStage = false;

    private void Awake()
    {
        bonfireLit.SetActive(false);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Soul"))
        {
            Destroy(other.gameObject);
            soulsAbsorbed++;
            if (soulsAbsorbed >= soulsRequired)
            {
                bonfireLit.SetActive(true);
                allowTeleport = true;
            }
        }

        if (other.gameObject.CompareTag("Player") && allowTeleport)
        {
            Loader.Scene scene = (Loader.Scene)Enum.Parse(typeof(Loader.Scene), sceneName);
            Loader.Load(scene);
            GameManager.GMInstance.SetPosition(setSpawnPosition);
            GameManager.GMInstance.SavePosition(sceneName);

            if(isBossStage)
            GameManager.GMInstance.SaveLevelCompletion(levelCompleteN);
        }
    }
}
