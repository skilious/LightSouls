using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkiesLoader : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoading = 10f;

    private float timeElapsed;

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > delayBeforeLoading)
        {
            Debug.Log("Application.Quit");
            Application.Quit();
        }
    }
}
