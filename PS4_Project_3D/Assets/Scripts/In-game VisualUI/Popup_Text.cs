using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script learnt from tutorial.
public class Popup_Text : MonoBehaviour
{
    [SerializeField]
    protected GameObject damageObjPrefab; // Prefab
    [SerializeField]
    protected Color color; //Change text colour.

    protected Vector3 rotationFaceCamera;

    protected void ShowFloatingText()
    {
        //Rot is what's being used to make the text facing towards the camera.
        rotationFaceCamera = CharacterMovement.forward;
        GameObject prefab = Instantiate(damageObjPrefab, transform.position, Quaternion.LookRotation(rotationFaceCamera)); //Instantiates the objPrefab w/ rot as Quaternion.
    }
}
