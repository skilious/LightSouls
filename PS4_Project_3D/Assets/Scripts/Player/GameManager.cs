using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject player;

    public static GameManager GMInstance;
    private void Awake()
    {
        GMInstance = this;
        player = GameObject.Find("Player");
        DontDestroyOnLoad(player);
    }

    public Vector3 SetPosition(Vector3 playerPos)
    {
        return player.transform.position = playerPos;
    }
    public static GameObject GetPlayer()
    {
        return player;
    }
}
