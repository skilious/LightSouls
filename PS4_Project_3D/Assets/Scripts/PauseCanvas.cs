using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Tai's Canvas Pause script
public class PauseCanvas : MonoBehaviour
{
    [SerializeField]
    private Button quitBtn;

    private void Start()
    {
        quitBtn.onClick.AddListener(QuitGame);
    }
    private void QuitGame()
    {
        Application.Quit();
    }
}
