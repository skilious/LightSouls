using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameObject player;
    public static GameManager GMInstance;
    private void Awake()
    {
        GMInstance = this;
        player = GameObject.Find("Player");
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerCheckpointX") && !PlayerPrefs.HasKey("PlayerCheckpointY")) //If it doesn't exist, setup one.
        {
            PlayerPrefs.SetFloat("PlayerCheckpointX", player.transform.position.x); //Saves X and Y axis from the player's starting position.
            PlayerPrefs.SetFloat("PlayerCheckpointY", player.transform.position.z);
            print("Saved!");
            print("Saved: " + PlayerPrefs.GetFloat("PlayerCheckpointX") + " X axis");
            print("Saved: " + PlayerPrefs.GetFloat("PlayerCheckpointY") + " Z axis");
        }
        else
        LoadPosition();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("Resetting your fucking playerprefs. knob");
            PlayerPrefs.DeleteAll();
        }
    }
    private Vector3 LoadPosition()
    {
        float playerPosX = PlayerPrefs.GetFloat("PlayerCheckpointX");
        float playerPosY = PlayerPrefs.GetFloat("PlayerCheckpointY");
        Vector3 playerPosition = new Vector3(playerPosX, player.transform.position.y, playerPosY);
        return player.transform.position = playerPosition;
    }

    public Vector3 SetPosition(Vector3 playerPos)
    {
        PlayerPrefs.SetFloat("PlayerCheckpointX", player.transform.position.x); //Saves X and Y axis from the player's starting position.
        PlayerPrefs.SetFloat("PlayerCheckpointY", player.transform.position.z); //Saves on top of the trigger box of the teleporter.
        return player.transform.position = playerPos;
    }
    public static GameObject GetPlayer()
    {
        return player;
    }


}
