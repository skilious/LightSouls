using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        DontDestroyOnLoad(player);
    }

    public static GameObject GetPlayer()
    {
        return player;
    }
}
