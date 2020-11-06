using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CapacityVisual : MonoBehaviour
{
    private Image capacityCircle;

    private void Awake()
    {
        capacityCircle = GetComponent<Image>();
    }
    private void Update()
    {
        float amount = Character_Status.curCapacity / (float)Character_Status.maxCapacity * 360.0f / 360.0f;
        capacityCircle.fillAmount = amount;
    }
}
