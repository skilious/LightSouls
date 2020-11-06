using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Popup_Text : MonoBehaviour
{
    [SerializeField]
    protected GameObject objPrefab;
    [SerializeField]
    protected Color color;

    private Vector3 rot;
    protected GameObject prefab;
    protected void ShowFloatingText()
    {
        rot = CharacterMovement.forward;
        GameObject prefab = Instantiate(objPrefab, transform.position, Quaternion.LookRotation(rot));
    }
}
