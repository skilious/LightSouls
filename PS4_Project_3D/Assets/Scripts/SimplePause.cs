using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Tai's simple pause script.
public class SimplePause : MonoBehaviour
{
    private bool isPaused = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                isPaused = false;
                Time.timeScale = 0;
                SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            }
            else if (!isPaused)
            {
                isPaused = true;
                Time.timeScale = 1;
                SceneManager.UnloadSceneAsync(gameObject.name);
            }
        }
    }
}
