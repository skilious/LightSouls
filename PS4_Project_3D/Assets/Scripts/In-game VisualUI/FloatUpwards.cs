using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script learnt from tutorial.
public class FloatUpwards : MonoBehaviour
{
    [SerializeField]
    private float timeToDestroy;

    private Vector3 offset = new Vector3(0.0f, 2.0f, 0.0f);

    private Vector3 Intensity = new Vector3(0.5f, 0.5f, 0.0f);
    void Start()
    {
        Destroy(gameObject, timeToDestroy);

        transform.localPosition += offset;
        //Randomising its positioning after the offset anywhere between -0.5 and 0.5f with each axis.
        transform.localPosition += new Vector3(Random.Range(-Intensity.x, Intensity.x), Random.Range(-Intensity.y, Intensity.y), Random.Range(-Intensity.z, Intensity.z));
    }
}
