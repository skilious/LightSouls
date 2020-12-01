using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevelScene : MonoBehaviour
{
    [SerializeField] private Vector3 setSpawnPosition;

    [SerializeField] private string sceneName;

    //Simple load to scene
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            SceneManager.LoadScene(sceneName);
            GameManager.GMInstance.SetPosition(setSpawnPosition);
            GameManager.GMInstance.SavePosition(sceneName);
        }
    }
}
