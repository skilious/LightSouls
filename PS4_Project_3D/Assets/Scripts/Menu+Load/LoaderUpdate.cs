using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderUpdate : MonoBehaviour
{
    private const float DELAY_BEFORE_LOAD = 1f;

    private float timeElapsed;

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > DELAY_BEFORE_LOAD)
        {
            Loader.LoadTargetScene();
        }
    }
}
