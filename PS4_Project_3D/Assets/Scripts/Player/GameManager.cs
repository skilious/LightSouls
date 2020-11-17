using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private void Start()
    {
        DontDestroyOnLoad(player);
    }
}
