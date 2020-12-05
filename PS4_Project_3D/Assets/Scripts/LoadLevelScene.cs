using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevelScene : MonoBehaviour
{
    [SerializeField] protected Vector3 setSpawnPosition;

    [SerializeField] protected string sceneName;

    //Simple load to scene
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Loader.Scene scene = (Loader.Scene)Enum.Parse(typeof(Loader.Scene), sceneName);
            Loader.Load(scene);
            GameManager.GMInstance.SetPosition(setSpawnPosition);
            GameManager.GMInstance.SavePosition(sceneName);
        }
    }
}
