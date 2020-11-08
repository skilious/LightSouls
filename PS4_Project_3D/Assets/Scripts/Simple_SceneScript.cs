using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Tai's simple script.
public class Simple_SceneScript : MonoBehaviour
{
    private bool isPaused;
    void Awake()
    {
        //Load this in the same scene. Additive adds the scene into the current scene.
        SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
    }
}
