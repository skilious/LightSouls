using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EZMode : MonoBehaviour
{
    [SerializeField] private GameObject main_pause;
    [SerializeField] private GameObject EZModePanel;

    private int pressed = 0;
    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            main_pause.SetActive(false);
            EZModePanel.SetActive(true);
            pressed++;
        }
        
        if(pressed >= 2)
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("Pause_Scene");
            EZModePanel.SetActive(false);
            main_pause.SetActive(false);
            SimplePause.notPaused = true;
            PlayerPrefs.SetInt("EZMode", 1);
            GameManager.enableEZmode = 1;
        }
    }

    private void OnDisable()
    {
        pressed = 0;
    }
}
