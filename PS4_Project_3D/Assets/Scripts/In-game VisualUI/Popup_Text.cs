using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script learnt from tutorial.
public class Popup_Text : MonoBehaviour
{
    [SerializeField]
    protected GameObject objPrefab; //Prefab
    [SerializeField]
    protected Color color; //Change text colour.

    private Vector3 rot;
    protected void ShowFloatingText()
    {
        //Rot is what's being used to make the text facing towards the camera.
        rot = CharacterMovement.forward;
        GameObject prefab = Instantiate(objPrefab, transform.position, Quaternion.LookRotation(rot)); //Instantiates the objPrefab w/ rot as Quaternion.
    }
}
