using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevelScene : MonoBehaviour
{
    [SerializeField] private Vector3 setSpawnPosition;

    [SerializeField] private string sceneName;

    [SerializeField] private bool levelComplete = false;
    //Simple load to scene
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Loader.Scene scene = (Loader.Scene)Enum.Parse(typeof(Loader.Scene), sceneName);
            Loader.Load(scene);
            GameManager.GMInstance.SetPosition(setSpawnPosition);
            GameManager.GMInstance.SavePosition(sceneName);
            if(levelComplete)
            {
                GameManager.GMInstance.SaveLevelCompletion(1);
            }
        }
    }
}
